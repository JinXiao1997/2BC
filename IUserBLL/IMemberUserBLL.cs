using System;
using System.Threading.Tasks;

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
        public  Task<bool> Login(string name,string pwd);
    }
}
