using IUserBLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
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
   

    public class UserInfoController : ControllerBase
    {

        IMemberUserBLL db;
        public UserInfoController(IMemberUserBLL db)
        {
            this.db = db;
        }

        [HttpPost("login")]
        public async Task<TData> Login(LoginDto login)
        {
            return await db.Login(login.name, login.pwd);
        }
        [HttpPost("reg")]
        public async Task<TData> Register(RegDto login)
        {
            return await db.Login(login.name, login.pwd);
        }


    }
}
