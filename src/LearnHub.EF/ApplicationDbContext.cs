using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LearnHub.Core;
using LearnHub.Core.Models;

namespace LearnHub.EF
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options ): base(options)
        {
            
        }


        public DbSet<Course> courses { get; set; }
        public DbSet<Enrollment> enrollment { get; set; }
        public DbSet<Grade> grades { get; set; }
        public DbSet<Module> modules { get; set; }
        public DbSet<Lesson> lessons { get; set; }
        public DbSet<Content> content { get; set; }


     
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configure the relationship between Grade and User (Student)
        builder.Entity<Grade>()
            .HasOne(g => g.Student)
            .WithMany()
            .HasForeignKey(g => g.StudentId)
            .OnDelete(DeleteBehavior.NoAction);
        

        // Configure the relationship between Grade and Course
        builder.Entity<Grade>()
            .HasOne(g => g.Course)
            .WithMany()
            .HasForeignKey(g => g.CourseId)
            .OnDelete(DeleteBehavior.NoAction);
    }

    }
}