using IUserBLL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserDAL;
namespace UserBLL
{
    public class MemberUserBLL : IMemberUserBLL
    {
        private IUserDAL.IUserDAL userSvc;
        public MemberUserBLL(IUserDAL.IUserDAL userSvc)
        {
            this.userSvc = userSvc;
        }
        public async Task<bool> Login(string name, string pwd)
        {
            UserModel.MemberUser model = await userSvc.FindAsync(p => p.UserName.Equals(name) && p.PassWord.Equals(pwd));
            return model == null;
        }

     
    }
}
