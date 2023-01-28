using Microsoft.EntityFrameworkCore;
using RIArchitecture.Core.Entities;

namespace RIArchitecture.Infrastructure
{
    public static class RIArchitectureModelBuilderExtenstion
    {
        public static void RIArchitectureEntitiesConfigurationExtenstion(this ModelBuilder builder)
        {
            builder.Entity<OtpDetail>(entity =>
            {
                entity.ToTable(name: "OtpDetails");
            });

        }
    }
}
