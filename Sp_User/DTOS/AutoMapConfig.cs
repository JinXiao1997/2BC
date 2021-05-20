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
                    opt=>opt.MapFrom(src=>DateTime.Now.ToString("yyyy-MM-dd : HH :mm:ss"))
                );
        }
    }
}
