using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RIArchitecture.Application.Contracts.Administration.DTOs;
using RIArchitecture.Application.Contracts.Utility;
using RIArchitecture.Application.Contracts.Utility.Otp;
using RIArchitecture.Core;
using RIArchitecture.Core.Entities;

namespace RIArchitecture.Application
{
    public class RIArchitectureAutoMapperProfile : Profile
    {
        public RIArchitectureAutoMapperProfile()
        {
            CreateMap<Item,ItemDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CreationTime, opt => opt.MapFrom(src => src.CreationTime))
                .ForMember(dest => dest.CreatorId, opt => opt.MapFrom(src => src.CreatorId))
                .ForMember(dest => dest.LastModificationTime, opt => opt.MapFrom(src => src.LastModificationTime))
                .ForMember(dest => dest.LastModifierId, opt => opt.MapFrom(src => src.LastModifierId))
                .ReverseMap()
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<AppUser,UserDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ReverseMap()
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<IdentityRole, RoleDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ReverseMap()
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<AppUser, GetUserOutputDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ReverseMap()
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<OtpDetail, OtpDetailDto>().ReverseMap();
        }
    }
}
