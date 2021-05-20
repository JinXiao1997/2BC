using System;
using System.Threading.Tasks;
using UserModel;

namespace IUserBLL
{
    public interface IMemberUserBLL
    {
        /// <summary>
        ///  用户登录
        /// </summary>
        /// <param name="name">账号</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public  Task<TData> Login(string name,string pwd);

        public Task<TData> Register(MemberUser model);
    }
}
