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
    }
}
