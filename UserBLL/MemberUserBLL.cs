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


        public async Task<TData> Login(string name, string pwd)
        {
            TData<object> data = new TData<object>();
            UserModel.MemberUser model = await userSvc.FindAsync(p => p.UserName.Equals(name));
            if (model == null)
            {
                throw new Exception("用户不存在");
                //data.Tag = 0;
                //data.Message = "用户不存在";
            }
            else if (!model.PassWord.Equals(pwd))
            {
                throw new Exception("密码错误");
                //data.Tag = 0;
                //data.Message = "密码错误";
            }
            else
            {
                try
                {
                    data.Tag = 1;
                    data.Message = "登录成功";
                    var obj = new { Token = JwtTools.GetToken(name) };
                    data.Result = obj;
                }
                catch (Exception ex)
                {
                    throw new Exception("登录失败" + ex.Message);
                }
            }
            return data;
        }

        public async Task<TData> Register(MemberUser model)
        {
            TData data = new TData();
          
            if (await userSvc.FindAsync(p => p.Mobile.Equals(model.Mobile) || p.Email.Equals(model.Email)||p.UserName.Equals(model.UserName))!=null )
            {
                throw new Exception("该用户已注册");
            }
            int i = await userSvc.AddEntityAsync(model);
            if (i > 0)
            {
                data.Tag = 1;
                data.Message = "注册成功";
            }
            else
            {
                throw new Exception("注册失败");
            }
            return data;
        }
    }
}
