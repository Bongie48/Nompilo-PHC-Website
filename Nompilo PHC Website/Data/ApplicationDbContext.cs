using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nompilo_PHC_Website.Models;

namespace Nompilo_PHC_Website.Data
{
    public class ApplicationDbContext : IdentityDbContext<DataGeeksUser>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("Identity");
            builder.Entity<DataGeeksUser>(
                entity => entity.ToTable(name: "AspNetUsers")
                );

        }

        
        public DbSet<InstrumentRequest> InstrumentRequests { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<TestMethod> TestMethods { get; set; }
        public DbSet<EverithingPeriodsF> EverithingPeriodsF { get; set; }
        public DbSet<ContraceptiveRecordN> ContraceptiveRecordN { get; set; }
        public DbSet<ContraRegister> ContraRegister { get; set; }
        public DbSet<FPregnancy> FPregnancy { get; set; }
        public DbSet<FpregRefers> FpregRefers { get; set; }
        public DbSet<EmeContra> EmeContra { get; set; }
        public DbSet<RegisterFPatients> RegisterFPatients { get; set; }
        public DbSet<LogSyms> LogSyms { get; set; }
        public DbSet<CheckType> CheckType { get; set; }
        public DbSet<SympClass> SympClass { get; set; }
        public DbSet<RepOption> RepOption { get; set; }
        //public DbSet<AvailContra> AvailContra { get; set; }
        public DbSet<TestResult> TestResults { get; set; }
        public DbSet<Test> Tests { get; set; }

        public DbSet<Instrument> Instruments { get; set; }

        public DbSet<Reminder> Reminder { get; set; }
        public DbSet<Rvalues> Rvalues { get; set; }
        public DbSet<PrenatalPatients> PrenatalPatients{ get; set; }
        public DbSet<MigTest> MigTests { get; set; }
        public DbSet<Chronicpatients> Chronicpatients { get; set; }

        //added doctors class 
        public DbSet<Doctors> doctors { get; set; }

        public DbSet<Report_Wlk> rprt { get; set; }

        //Prenatal Care
        public DbSet<Prescription> Prescriptions { get; set; }

        public DbSet<Checkup> Checkups { get; set; }
    }
}