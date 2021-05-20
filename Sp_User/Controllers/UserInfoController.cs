using AutoMapper;
using IUserBLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Sp_User.AOP;
using Sp_User.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserModel;

namespace Sp_User.Controllers
{

    [ApiController]
    [Route("UserInfo")]

    [UserFilter]
    public class UserInfoController : ControllerBase
    {

       private readonly IMemberUserBLL db;
        private readonly IMapper mapper;
        public UserInfoController(IMemberUserBLL db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<TData> Login(LoginDto login)
        {
            return await db.Login(login.name, login.pwd);
        }
        [HttpPost("Register")]
     
        public async Task<TData>  Register(RegDto login)
        {
            var model = mapper.Map<MemberUser>(login);
            return await db.Register(model);

        }


    }
}
