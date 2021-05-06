
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using UserModel;
using Microsoft.EntityFrameworkCore;

namespace UserDAL
{
    public class DBContex : DbContext
    {
        public DBContex(DbContextOptions<DBContex> options)
               : base(options)
        {
        }
 
        public DbSet<MemberUser> Member { get; set; }
    }
}
