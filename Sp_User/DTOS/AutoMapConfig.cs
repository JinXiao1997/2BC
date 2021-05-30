using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserModel;

namespace Sp_User.DTOS
{
    public class AutoMapConfig:Profile
    {
        public AutoMapConfig() {
            CreateMap<RegDto, MemberUser>().ForMember(
                    desc => desc.CreateTime,
                    opt => opt.MapFrom(src => DateTime.Now.ToString("yyyy-MM-dd HH:mm"))
                ).ForMember(
                     desc => desc.Birth,
                    opt => opt.MapFrom(src => DateTime.Now.ToString("yyyy-MM-dd"))
                ).ForMember(
                     desc => desc.Status,
                    opt => opt.MapFrom(src => 0)
                ).ForMember(
                     desc => desc.NickName,
                    opt => opt.MapFrom(src => src.Mobile)
                ).ForMember(
                     desc => desc.Email,
                    opt => opt.MapFrom(src =>"")
                ).ForMember(
                     desc => desc.Sign,
                    opt => opt.MapFrom(src => "")
                ).ForMember(
                     desc => desc.Job,
                    opt => opt.MapFrom(src => "")
                ).ForMember(
                     desc => desc.Header,
                    opt => opt.MapFrom(src => "")
                ).ForMember(
                     desc => desc.City,
                    opt => opt.MapFrom(src => "")
                );
        }
    }
}
