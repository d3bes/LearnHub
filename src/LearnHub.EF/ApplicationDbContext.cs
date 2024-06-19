using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LearnHub.EF
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options ): base(options)
        {
            
        }
        //   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //   {
        // // Specify the migrations assembly
        // optionsBuilder.UseSqlServer(
        //     "workstation id=LearnHub.mssql.somee.com;packet size=4096;user id=abdelrahman95856_SQLLogin_1;pwd=nfdplhaa8t;data source=LearnHub.mssql.somee.com;persist security info=False;initial catalog=LearnHub;TrustServerCertificate=True", 
        //            options => options.MigrationsAssembly("LearnHub.EF"));
        //    }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //    optionsBuilder.UseSqlServer( "workstation id=LearnHub.mssql.somee.com;packet size=4096;user id=abdelrahman95856_SQLLogin_1;pwd=nfdplhaa8t;data source=LearnHub.mssql.somee.com;persist security info=False;initial catalog=LearnHub;TrustServerCertificate=True"
        //    ,    options => options.MigrationsAssembly("LearnHub.Api"));

        // }

    }
}