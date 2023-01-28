using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using RIArchitecture.Core;
using RIArchitecture.Core.Entities;
using RIArchitecture.Core.RIArchitectureCoreBase.Interface;
using RIArchitecture.Infrastructure.RIArchitectureDomainService;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace RIArchitecture.Infrastructure
{
    public class RIArchitectureDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        private readonly ICurrentUser _currentUser;
        private readonly IHttpContextAccessor _context;

        //Mason app entities
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<OtpDetail> OtpDetails { get; set; }

        public RIArchitectureDbContext(
            DbContextOptions options,
            ICurrentUser currentUser,
            IHttpContextAccessor context) : base(options)
        {
            _currentUser = currentUser;
            _context = context;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            IsSoftDeleteConfiguration(builder);

            IdentityCustomizationConfiguration(builder);

            builder.RIArchitectureEntitiesConfigurationExtenstion();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ProcessSave();
            return base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Below code should not be changed without having full understanding of the code -- Lalit
        /// </summary>
        #region Configuration
        private void ProcessSave()
        {
            var currentTime = DateTime.UtcNow;
            var addedEntities = ChangeTracker.Entries().Where(x => x.State == EntityState.Added && x.Entity is IFullAuditedEntity);
            var sourceValue = _context?.HttpContext?.Request?.Headers["app-source"];
            foreach (var item in addedEntities)
            {
                if (item.Entity is IFullAuditedEntity entity)
                {
                    entity.Source = sourceValue;
                    entity.CreationTime = currentTime;
                    entity.CreatorId = _currentUser.Id;
                }
            }

            var modifiedEntities = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified && x.Entity is IFullAuditedEntity);

            foreach (var item in modifiedEntities)
            {
                if (item.Entity is IFullAuditedEntity entity)
                {
                    entity.LastModificationTime = currentTime;
                    entity.LastModifierId = _currentUser.Id;
                }
            }


            var markedAsDeleted = ChangeTracker.Entries().Where(x => x.State == EntityState.Deleted && x.Entity is ISoftDelete);

            foreach (var item in markedAsDeleted)
            {
                if (item.Entity is ISoftDelete entity)
                {
                    // Set the entity to unchanged (if we mark the whole entity as Modified, every field gets sent to Db as an update)
                    item.State = EntityState.Unchanged;
                    // Only update the IsDeleted flag - only this will get sent to the Db
                    entity.IsDeleted = true;
                    entity.DeletedDateTime = currentTime;
                    entity.DeletorId = _currentUser.Id;
                }
            }
        }

        private static void IdentityCustomizationConfiguration(ModelBuilder builder)
        {
            builder.Entity<AppUser>(entity =>
            {
                entity.ToTable(name: "Users");
            });
            builder.Entity<IdentityRole<Guid>>(entity =>
            {
                entity.ToTable(name: "Roles");
            });
            builder.Entity<IdentityUserRole<Guid>>(entity =>
            {
                entity.ToTable("UserRoles");
            });
            builder.Entity<IdentityUserClaim<Guid>>(entity =>
            {
                entity.ToTable("UserClaims");
            });
            builder.Entity<IdentityUserLogin<Guid>>(entity =>
            {
                entity.ToTable("UserLogins");
            });
            builder.Entity<IdentityRoleClaim<Guid>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });
            builder.Entity<IdentityUserToken<Guid>>(entity =>
            {
                entity.ToTable("UserTokens");
            });
        }

        private static void IsSoftDeleteConfiguration(ModelBuilder builder)
        {
            Expression<Func<ISoftDelete, bool>> filterExpr = bm => !bm.IsDeleted;
            foreach (var mutableEntityType in builder.Model.GetEntityTypes())
            {
                // check if current entity type is child of BaseModel
                if (mutableEntityType.ClrType.IsAssignableTo(typeof(ISoftDelete)))
                {
                    // modify expression to handle correct child type
                    var parameter = Expression.Parameter(mutableEntityType.ClrType);
                    var body = ReplacingExpressionVisitor.Replace(filterExpr.Parameters.First(), parameter, filterExpr.Body);
                    var lambdaExpression = Expression.Lambda(body, parameter);

                    // set filter
                    mutableEntityType.SetQueryFilter(lambdaExpression);
                }
            }
        }
        #endregion

    }
}