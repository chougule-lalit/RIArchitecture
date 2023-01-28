using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RIArchitecture.Application.Contracts.Administration.DTOs;
using RIArchitecture.Application.Contracts.Administration.Interfaces;
using RIArchitecture.Application.Contracts.Utility;
using RIArchitecture.Core;
using RIArchitecture.Core.RIArchitectureCoreBase.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RIArchitecture.Application.Administration.Services
{
    public class UserAppService : RIArchitectureAppService, IUserAppService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly ILogger<UserAppService> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public UserAppService(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            ILogger<UserAppService> logger,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public virtual async Task<PagedResultDto<GetUserOutputDto>> GetPagedResultAsync(GetUserInputDto input)
        {
            var usersList = new List<AppUser>();
            if (input?.RoleNames.Count > 0)
            {
                foreach (var role in input.RoleNames)
                {
                    usersList.AddRange(await _userManager.GetUsersInRoleAsync(role));
                }
            }
            else
            {
                usersList = await _userManager.Users.ToListAsync();
            }

            var count = usersList.Count;
            var finalUsers = usersList.Skip(input.SkipCount * input.MaxResultCount).Take(input.MaxResultCount).ToList();

            var outputList = new List<GetUserOutputDto>();
            foreach (var user in finalUsers)
            {
                var outputData = ObjectMapper.Map<AppUser, GetUserOutputDto>(user);
                var roles = await _userManager.GetRolesAsync(user);
                outputData.RoleList = roles.ToList();
                outputData.Roles = roles.Count > 0 ? string.Join(',', roles) : string.Empty;

                outputList.Add(outputData);
            }

            return new PagedResultDto<GetUserOutputDto>
            {
                Items = outputList,
                TotalCount = count
            };
        }

        public virtual async Task<Guid> CreateAsync(UserDto input)
        {
            var user = await _userManager.Users.Where(x => x.PhoneNumber == input.PhoneNumber || x.Email == input.Email).FirstOrDefaultAsync();

            if (user == null)
            {
                var newUser = new AppUser()
                {
                    Email = input.Email,
                    UserName = input.UserName,
                    PhoneNumber = input.PhoneNumber,
                    Name = input.Name
                };

                var usersWithSameUserName = await _userManager.Users.Where(x =>  x.UserName.Contains(newUser.UserName)).ToListAsync();

                if(usersWithSameUserName.Count > 0)
                {
                    if(usersWithSameUserName.Any(x=> x.UserName == newUser.UserName && x.Email == newUser.Email))
                        return Guid.Empty;

                    var count = usersWithSameUserName.Count;
                    newUser.UserName = $"{newUser.UserName}_{count}";
                }

                var isCreated = await _userManager.CreateAsync(newUser, input.Password);

                if (isCreated.Succeeded)
                {
                    foreach (var role in input.Roles)
                    {
                        if (await _roleManager.RoleExistsAsync(role))
                            await _userManager.AddToRoleAsync(newUser, role);
                    }

                    await _unitOfWork.Complete();
                    return newUser.Id;
                }
                else
                {
                    _logger.LogError($"Error occured while user creation");
                    return Guid.Empty;
                }
            }
            else
            {
                _logger.LogError($"User already exist!!!");
                return Guid.Empty;
            }
        }

        public virtual async Task<bool> UpdateAsync(Guid id, UserDto input)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                user.UserName = input.UserName;
                user.Email = input.Email;
                user.PhoneNumber = input.PhoneNumber;

                var rolesToRemove = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, rolesToRemove);

                foreach (var role in input.Roles)
                {
                    if (await _roleManager.RoleExistsAsync(role))
                        await _userManager.AddToRoleAsync(user, role);
                }

                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    _logger.LogError($"Error occured while updating user with id : {id}");
                    return false;
                }
                else
                    return true;

            }
            else
            {
                _logger.LogError($"User not found with Id : {id}");
                return false;
            }
        }

        public virtual async Task<UserDto> GetAsync(Guid id)
        {
            var userDto = new UserDto();
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user != null)
            {
                userDto = ObjectMapper.Map<AppUser, UserDto>(user);
                var roles = await _userManager.GetRolesAsync(user);
                userDto.Roles = roles.ToList();
            }
            else
            {
                _logger.LogError($"User not found with id : {id}");
            }

            return userDto;
        }

        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                return result.Succeeded;
            }
            else
            {
                _logger.LogError($"User not found with id : {id}");
                return false;
            }
        }
    }
}
