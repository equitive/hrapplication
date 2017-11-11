using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HR_Management.Models;

namespace HR_Management.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<HR_Management.Models.HRModels.Complaints> Complaints { get; set; }
        public DbSet<HR_Management.Models.HRModels.Employee> Employee { get; set; }
        public DbSet<HR_Management.Models.HRModels.PositionInfo> PositionInfo { get; set; }
        public DbSet<HR_Management.Models.HRModels.Reviews> Reviews { get; set; }
        public DbSet<HR_Management.Models.HRModels.TimeOff> TimeOff { get; set; }
		public DbSet<HR_Management.Models.HRModels.Messages> Messages { get; set; }

	}
}
