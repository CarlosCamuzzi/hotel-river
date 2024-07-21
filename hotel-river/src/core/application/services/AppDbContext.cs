using hotel_hill.Models;
using hotel_river.src.infrastructure.database.models;
using Microsoft.EntityFrameworkCore;

namespace hotel_river.src.core.application.services
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        //public DbSet<AdditionalRoomItem> AdditionalRoomItems { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Employee> Employees { get; set; } = default!;
        public DbSet<Client> Clients { get; set; } = default!;
        public DbSet<Hotel> Hotels { get; set; }
        //public DbSet<Payment> Payments { get; set; }
        // public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Heritage with TPT (Table per type)
            modelBuilder.Entity<Client>().ToTable("Clients");
            modelBuilder.Entity<Employee>().ToTable("Employees");

            // Heritage
            modelBuilder.Entity<Employee>().HasBaseType<Person>();
            modelBuilder.Entity<Client>().HasBaseType<Person>();

            // Relationship Person : Address (1:1)
            modelBuilder.Entity<Person>()
               .HasOne(h => h.Address)
               .WithOne()
               .HasForeignKey<Person>(h => h.AddressId);

            // Relationship Hotel : Employee (1:N)
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Hotel)
                .WithMany(e => e.Employees)
                .HasForeignKey(e => e.HotelId);

            // Relationship Hotel : Client (1:N)
            modelBuilder.Entity<Client>()
                .HasOne(c => c.Hotel)
                .WithMany(c => c.Clients)
                .HasForeignKey(e => e.HotelId);

            // Relationship Hotel : Address (1:1)
            modelBuilder.Entity<Hotel>()
               .HasOne(h => h.Address)
               .WithOne()
               .HasForeignKey<Hotel>(h => h.AddressId);

            // Relationship Hotel : Rooms (1:N)
            modelBuilder.Entity<Room>()
                .HasOne(r => r.Hotel)
                .WithMany(r => r.Rooms)
                .HasForeignKey(r => r.HotelId);
        }
    }
}
