using Data.Lib.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Lib
{
    public class DataDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Member> Members { get; set; }
        public DbSet<Deposit> Deposits { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Loan> Loan { get; set; }
        public DbSet<LoanCollection> LoanCollection { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<AppUser> AppUser { get; set; }
        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    if (!optionsBuilder.IsConfigured)
        //        optionsBuilder.UseInMemoryDatabase("Db");
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<Member>()
                .HasIndex(e => e.MemberNo).IsUnique();
            modelBuilder.Entity<Member>()
                .HasIndex(e => e.NationalNo).IsUnique();
            base.OnModelCreating(modelBuilder);
        }
    }
}
