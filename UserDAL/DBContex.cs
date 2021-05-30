
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using UserModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace UserDAL
{
    public class DBContex : DbContext
    {
        public DBContex(DbContextOptions<DBContex> options)
               : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new EFLoggerProvider());
            optionsBuilder.UseLoggerFactory(loggerFactory);
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<MemberUser> Member { get; set; }
    }
}
