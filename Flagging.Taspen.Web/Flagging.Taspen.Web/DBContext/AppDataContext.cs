using Flagging.Taspen.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Flagging.Taspen.Web.DBContext
{
    public class AppDataContext(DbContextOptions<AppDataContext> options) : IdentityDbContext<AppUser>(options)
    {
        public DbSet<Peserta> Peserta { get; set; }
        public DbSet<Provinsi> Provinsi { get; set; }
        public DbSet<Kota> Kota { get; set; }
        public DbSet<Kecamatan> Kecamatan { get; set; }
        public DbSet<Kelurahan> Kelurahan { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kota>()
            .HasOne(k => k.Provinsi)
            .WithMany(p => p.Kota)
            .HasForeignKey(k => k.ProvinsiId);

            modelBuilder.Entity<Kecamatan>()
                .HasOne(kec => kec.Kota)
                .WithMany(k => k.Kecamatan)
                .HasForeignKey(kec => kec.KotaId);

            modelBuilder.Entity<Kelurahan>()
                .HasOne(kel => kel.Kecamatan)
                .WithMany(kec => kec.Kelurahan)
                .HasForeignKey(kel => kel.KecamatanId);

            modelBuilder.Entity<Kota>()
                .Property(k => k.Tipe).HasConversion<string>();
            modelBuilder.Entity<Kelurahan>()
                .Property(k => k.Tipe).HasConversion<string>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
