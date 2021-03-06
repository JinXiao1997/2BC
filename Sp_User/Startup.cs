
using IUserBLL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using UserBLL;
using UserDAL;
using Sp_User.DTOS;
using Wei.RedisHelper;

namespace Sp_User
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<DBContex>(options => options.UseMySQL(Configuration.GetConnectionString("MySqlConnection")));
            services.AddTransient<IUserDAL.IUserDAL, UserDAL.UserDAL>();
            services.AddTransient<IMemberUserBLL, MemberUserBLL>();
            services.AddAutoMapper(typeof(AutoMapConfig));
            services.AddRedisHelper(ops => {
                ops.RedisConnectionString = Configuration.GetConnectionString("RedisConfig");
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
