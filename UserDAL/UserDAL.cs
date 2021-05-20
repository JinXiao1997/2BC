using System;
using System.Collections.Generic;
using System.Text;
using UserModel;
namespace UserDAL
{
   public class UserDAL:BaseDAL<MemberUser>, IUserDAL.IUserDAL
    {
        private DBContex DB;
        public UserDAL(DBContex DB) : base(DB)
        {
            this.DB = DB;
        }
    }
}
