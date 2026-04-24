using Flagging.Taspen.Web.DBContext;
using Flagging.Taspen.Web.Models;
using Flagging.Taspen.Web.Utils;
using Microsoft.AspNetCore.Identity;

namespace Flagging.Taspen.Web.Helpers
{
    public static class SeedHelper
    {
        public static async Task<bool> SeedUserAndRoles(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, AppDataContext appDataContext)
        {
            var existingRole = roleManager.Roles.FirstOrDefault(m => m.Name == Constants.RoleAdministrator);
            if (existingRole == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Constants.RoleAdministrator));
            }

            existingRole = roleManager.Roles.FirstOrDefault(m => m.Name == Constants.RoleUser);
            if (existingRole == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Constants.RoleUser));
            }

            var existingUser = userManager.Users.FirstOrDefault(m => m.Email == Constants.EmailUserDefault);
            if (existingUser == null)
            {
                AppUser user = new AppUser
                {
                    Name = Constants.NameUserDefault,
                    Email = Constants.EmailUserDefault,
                    UserName = Constants.EmailUserDefault
                };

                await userManager.CreateAsync(user, Constants.PasswordUserDefault);
                await userManager.AddToRoleAsync(user, Constants.RoleAdministrator);
                await userManager.AddClaimAsync(user, new System.Security.Claims.Claim("Name", Constants.NameUserDefault));
            }

            var existingPeserta = appDataContext.Peserta.Any();
            if (!existingPeserta)
            {
                await appDataContext.Peserta.AddRangeAsync(new List<Peserta>
                {
                    new() {
                        Id = Guid.NewGuid().ToString(),
                        IsActive = true,
                        CreatedBy = "SeedSystem",
                        UpdatedBy = "SeedSystem",
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        NIP = "198765432012031001",
                        Notas = "N0001",
                        NoKPE = "KPE0001",
                        Nama = "Dewi Fajar",
                        TanggalLahir = DateTime.Now,
                        Instansi = "Dinas Pemerintah",
                        Provinsi = "Sumatera Utara",
                        Kota = "Kota A",
                        Kecamatan  = "Kecamatan A",
                        Kelurahan  = "Kelurahan A",
                        Alamat = "Alamat A",
                        RekKredit = "900001",
                        RekTabungan = "800001",
                        NIK = "3200000000000001",
                        Surat = "sp1.pdf",
                        TMTKredit = DateTime.Now,
                        TATKredit = DateTime.Now
                    },
                });
                await appDataContext.SaveChangesAsync();
            }

            return true;
        }

        public static async Task<bool> SeedProvinsiKota(AppDataContext context)
        {
            if (context.Provinsi.Any()) return true;

            // ── Provinsi ─────────────────────────────────────────────
            var jabar = new Provinsi { Id = 1, Kode = "32", Nama = "Jawa Barat" };
            var jateng = new Provinsi { Id = 2, Kode = "33", Nama = "Jawa Tengah" };
            var dki = new Provinsi { Id = 3, Kode = "31", Nama = "DKI Jakarta" };

            context.Provinsi.AddRange(jabar, jateng, dki);

            // ── Kota ─────────────────────────────────────────────────
            var kotaBandung = new Kota
            {
                Id = 1,
                Kode = "3273",
                Nama = "Kota Bandung",
                Tipe = KotaTipe.Kota,
                ProvinsiId = 1
            };
            var kabBandung = new Kota
            {
                Id = 2,
                Kode = "3204",
                Nama = "Kabupaten Bandung",
                Tipe = KotaTipe.Kabupaten,
                ProvinsiId = 1
            };
            var kotaSemarang = new Kota
            {
                Id = 3,
                Kode = "3374",
                Nama = "Kota Semarang",
                Tipe = KotaTipe.Kota,
                ProvinsiId = 2
            };
            var kotaJakartaPusat = new Kota
            {
                Id = 4,
                Kode = "3171",
                Nama = "Kota Jakarta Pusat",
                Tipe = KotaTipe.Kota,
                ProvinsiId = 3
            };

            context.Kota.AddRange(kotaBandung, kabBandung, kotaSemarang, kotaJakartaPusat);

            // ── Kecamatan ─────────────────────────────────────────────
            var kecCoblong = new Kecamatan
            {
                Id = 1,
                Kode = "327307",
                Nama = "Coblong",
                KotaId = 1
            };
            var kecBandungWetan = new Kecamatan
            {
                Id = 2,
                Kode = "327302",
                Nama = "Bandung Wetan",
                KotaId = 1
            };
            var kecCiwidey = new Kecamatan
            {
                Id = 3,
                Kode = "320401",
                Nama = "Ciwidey",
                KotaId = 2
            };
            var kecSemarangTengah = new Kecamatan
            {
                Id = 4,
                Kode = "337402",
                Nama = "Semarang Tengah",
                KotaId = 3
            };
            var kecGambir = new Kecamatan
            {
                Id = 5,
                Kode = "317101",
                Nama = "Gambir",
                KotaId = 4
            };

            context.Kecamatan.AddRange(kecCoblong, kecBandungWetan, kecCiwidey, kecSemarangTengah, kecGambir);

            // ── Kelurahan ─────────────────────────────────────────────
            var kelurahan = new List<Kelurahan>
            {
                // Coblong - Bandung
                new() { Id = 1, Kode = "3273071001", Nama = "Dago",
                        KodePos = "40135", Tipe = KelurahanTipe.Kelurahan, KecamatanId = 1 },
                new() { Id = 2, Kode = "3273071002", Nama = "Lebak Gede",
                        KodePos = "40132", Tipe = KelurahanTipe.Kelurahan, KecamatanId = 1 },
                new() { Id = 3, Kode = "3273071003", Nama = "Cipaganti",
                        KodePos = "40131", Tipe = KelurahanTipe.Kelurahan, KecamatanId = 1 },

                // Bandung Wetan
                new() { Id = 4, Kode = "3273021001", Nama = "Tamansari",
                        KodePos = "40116", Tipe = KelurahanTipe.Kelurahan, KecamatanId = 2 },
                new() { Id = 5, Kode = "3273021002", Nama = "Citarum",
                        KodePos = "40115", Tipe = KelurahanTipe.Kelurahan, KecamatanId = 2 },

                // Ciwidey - Kab. Bandung (Desa)
                new() { Id = 6, Kode = "3204011001", Nama = "Ciwidey",
                        KodePos = "40973", Tipe = KelurahanTipe.Desa, KecamatanId = 3 },
                new() { Id = 7, Kode = "3204011002", Nama = "Lebakmuncang",
                        KodePos = "40973", Tipe = KelurahanTipe.Desa, KecamatanId = 3 },

                // Semarang Tengah
                new() { Id = 8, Kode = "3374021001", Nama = "Miroto",
                        KodePos = "50134", Tipe = KelurahanTipe.Kelurahan, KecamatanId = 4 },
                new() { Id = 9, Kode = "3374021002", Nama = "Brumbungan",
                        KodePos = "50138", Tipe = KelurahanTipe.Kelurahan, KecamatanId = 4 },

                // Gambir - Jakarta Pusat
                new() { Id = 10, Kode = "3171011001", Nama = "Gambir",
                        KodePos = "10110", Tipe = KelurahanTipe.Kelurahan, KecamatanId = 5 },
                new() { Id = 11, Kode = "3171011002", Nama = "Cideng",
                        KodePos = "10150", Tipe = KelurahanTipe.Kelurahan, KecamatanId = 5 },
            };

            context.Kelurahan.AddRange(kelurahan);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
