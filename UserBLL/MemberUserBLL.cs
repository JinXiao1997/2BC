using IUserBLL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserDAL;
using UserModel;
using Tools;
using Wei.RedisHelper;

namespace UserBLL
{
    public class MemberUserBLL : IMemberUserBLL
    {
        private IUserDAL.IUserDAL userSvc;
        private readonly RedisClient redisClient;
        public MemberUserBLL(IUserDAL.IUserDAL userSvc, RedisClient client)
        {
            this.userSvc = userSvc;
            redisClient = client;
        }

        //
        public async Task<TData> Login(string name, string pwd)
        {
            TData<object> data = new TData<object>();
            UserModel.MemberUser model =  redisClient.StringGet<UserModel.MemberUser>("User"+name);
            if (model == null)
            {
                //防止缓存穿透
                if (!redisClient.KeyExists("User" + name)) 
                {
                    model = await userSvc.FindAsync(p => p.UserName.Equals(name));
                    if (model == null)
                    {
                        redisClient.StringSet<UserModel.MemberUser>("User" + name, null, TimeSpan.FromHours(new Random().Next(10, 99)));
                        throw new Exception("用户不存在");
                    }
                }
                else
                {
                    throw new Exception("用户不存在");
                }
            }
            if (!Tools.Base64.DecodeBase64(Encoding.UTF8, model.PassWord).Equals(pwd))
            {
                throw new Exception("密码错误");
            }
            else
            {
               bool isOK=  redisClient.StringSet<UserModel.MemberUser>("User" + name, model, TimeSpan.FromHours(new Random().Next(10, 99)));
                data.Tag = 1;
                data.Message = "登录成功";
                var obj = new { Token = JwtTools.GetToken(name) };
                data.Result = obj;
            }
            return data;
        }

        public async Task<TData> Register(MemberUser model)
        {
            TData data = new TData();
            var db = userSvc.BeginTran();
            try
            {
                if (await userSvc.FindAsync(p => p.Mobile.Equals(model.Mobile) || p.UserName.Equals(model.UserName)) != null)
                {
                    throw new Exception("该用户已注册");
                }
                model.PassWord=Tools.Base64.EncodeBase64(Encoding.UTF8,model.PassWord);
                int i = await userSvc.AddEntityAsync(model);
                //int i = await userSvc.ExecuteSql("insert into ums_member(id,level_id,username,password,nickname,mobile,email,header,gender, birth, job, sign, source_type, integration,growth,status,create_time)VALUES(" + model.Id + ", " + model.LevelId + ", '" + model.UserName + "', '" + model.PassWord + "', '" + model.NickName + "', '" + model.Mobile + "', '" + model.Email + "', '" + model.Header + "', " + model.Gender + ", '" + model.Birth + "', '" + model.Job + "', '" + model.Sign + "', " + model.SourceType + ", " + model.Integration + ", " + model.Growth + ", " + model.Status + ", '" + model.CreateTime + "')");
                if (i > 0)
                {
                    data.Tag = 1;
                    data.Message = "注册成功";
                    redisClient.StringSet<UserModel.MemberUser>("User" + model.UserName, model, TimeSpan.FromHours(new Random().Next(10, 99)));
                }
                else
                {
                    throw new Exception("注册失败");
                }
                db.Commit();
                return data;

            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        
        }
    }
}
