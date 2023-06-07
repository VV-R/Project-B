namespace Db {
    using Entities;
    using System.Net.Mail;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    public class ApplicationDbContext : DbContext {
        private string _connectionString;

        public ApplicationDbContext(string connectionString = "Data Source=./airport.db;") {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Flight>().HasKey(c => c.FlightNumber);
            modelBuilder.Entity<User>().HasKey(c => c.IdUser);
            modelBuilder.Entity<UserInfo>().HasKey(c => c.Id);
            modelBuilder.Entity<UserInfo>().HasIndex(c => c.Email).IsUnique();
            modelBuilder.Entity<Ticket>().HasKey(c => c.Id);
            modelBuilder.Entity<Ticket>().HasOne(e => e.TheFlight).WithMany().HasForeignKey(e => e.FlightId).IsRequired();
            modelBuilder.Entity<Ticket>().HasOne(e => e.TheUserInfo).WithMany().HasForeignKey(e => e.UserId).IsRequired();

            modelBuilder.Entity<UserInfo>().Property(e => e.Email).HasConversion(v => v.ToString(), v => new MailAddress(v));

            modelBuilder.Entity<User>().HasOne(e => e.UserInfo).WithOne().HasForeignKey<User>(e => e.UserInfoId);
        }

        public DbSet<Flight> Flights { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserInfo> UserInfo { get; set; }

        public DbSet<Ticket> Tickets { get; set; }
    }
}
