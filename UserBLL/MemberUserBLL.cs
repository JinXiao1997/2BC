using IUserBLL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserDAL;
using UserModel;
using Tools;

namespace UserBLL
{
    public class MemberUserBLL : IMemberUserBLL
    {
        private IUserDAL.IUserDAL userSvc;
        public MemberUserBLL(IUserDAL.IUserDAL userSvc)
        {
            this.userSvc = userSvc;
        }
       

       public async Task<TData>  Login(string name, string pwd)
        {
            TData<object> data = new TData<object>();
            UserModel.MemberUser model = await userSvc.FindAsync(p => p.UserName.Equals(name));
            if (model==null) 
            {
                data.Tag = 0;
                data.Message = "用户不存在";
            }
            else if(!model.PassWord.Equals(pwd))
            {
                data.Tag = 0;
                data.Message = "密码错误";
            }
            else
            {
                data.Tag = 1;
                data.Message = "登录成功";
                var obj = new { Token = JwtTools.GetToken(name) };
                data.Result = obj;
            }
            return data;
        }
    }
}
