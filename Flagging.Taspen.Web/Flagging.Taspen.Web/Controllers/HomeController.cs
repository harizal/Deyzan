using Flagging.Taspen.Web.DBContext;
using Flagging.Taspen.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Flagging.Taspen.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly AppDataContext _context;
        public HomeController(AppDataContext appDataContext)
        {
            _context = appDataContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SearchData(string query)
        {
            try
            {
                var peserta = await _context.Peserta.FirstOrDefaultAsync(m => m.NIP.Contains(query) || m.Notas.Contains(query));
                if (peserta == null)
                {
                    return Json(new BaseViewModel<PesertaViewModel>
                    {
                        IsSuccess = false,
                        Message = "Data tidak ditemukan"
                    });
                }

                if (peserta.IsBooking && peserta.IsBookingDate.HasValue && DateTime.Now - peserta.IsBookingDate.Value >= TimeSpan.FromHours(24))
                {
                    peserta.IsBooking = false;
                    peserta.IsBookingDate = null;
                    await _context.SaveChangesAsync();
                }

                var pesertaViewModel = new PesertaViewModel
                {
                    ID = peserta.Id,
                    Nama = Helpers.Utils.MaskName(peserta.Nama),
                    TanggalLahir = peserta.TanggalLahir.ToString("yyyy-MM"),
                    TanggalBUP = "",
                    Instansi = peserta.Instansi,
                    Status = peserta.IsBooking ? $"Booking aktif sejak {peserta.IsBookingDate:yyyy-MM-dd HH:mm}" : "Belum booking",
                    IsBooking = peserta.IsBooking
                };

                return Json(new BaseViewModel<PesertaViewModel>
                {
                    IsSuccess = true,
                    Message = "Data found",
                    Data = pesertaViewModel
                });
            }
            catch (Exception)
            {
                throw new Exception("Terjadi kesalahan saat mencari data");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Booking(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return Json(new BaseViewModel<object>
                {
                    IsSuccess = false,
                    Message = "Peserta tidak ditemukan"
                });
            }

            var peserta = await _context.Peserta.FirstOrDefaultAsync(m => m.Id == id);
            if (peserta == null)
            {
                return Json(new BaseViewModel<object>
                {
                    IsSuccess = false,
                    Message = "Peserta tidak ditemukan"
                });
            }

            peserta.IsBooking = true;
            peserta.IsBookingDate = DateTime.Now;
            await _context.SaveChangesAsync();

            return Json(new BaseViewModel<object>
            {
                IsSuccess = true,
                Message = "Booking berhasil disimpan"
            });
        }

        [HttpPost]
        public async Task<IActionResult> FlagingFlag(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return Json(new BaseViewModel<object>
                {
                    IsSuccess = false,
                    Message = "Peserta tidak ditemukan"
                });
            }

            var peserta = await _context.Peserta.FirstOrDefaultAsync(m => m.Id == id);
            if (peserta == null)
            {
                return Json(new BaseViewModel<object>
                {
                    IsSuccess = false,
                    Message = "Peserta tidak ditemukan"
                });
            }   

            peserta.IsFlaging = true;
            peserta.IsFlagingDate = DateTime.Now;
            await _context.SaveChangesAsync();

            return Json(new BaseViewModel<object>
            {
                IsSuccess = true,
                Message = "Flaging berhasil disimpan"
            });
        }

        public IActionResult Flaging(string idPeserta)
        {
            var peserta = _context.Peserta.FirstOrDefault(m => m.Id == idPeserta);
            if (peserta == null)
                return RedirectToAction("Index");
            var model = new FlagingPensiunViewModel
            {
                ID = peserta.Id,
                Nama = peserta.Nama,
                NIP = peserta.NIP,
                Notas = peserta.Notas,
                NoKPE = peserta.NoKPE,
                TanggalLahir = peserta.TanggalLahir,
                Instansi = peserta.Instansi,
                Provinsi = peserta.Provinsi,
                Kota = peserta.Kota,
                Kecamatan = peserta.Kecamatan,
                Kelurahan = peserta.Kelurahan,
                Alamat = peserta.Alamat,
                NoRekeningKredit = peserta.RekKredit,
                NoRekeningTabungan = peserta.RekTabungan,
                NIK = peserta.NIK,
                TMTKredit = peserta.TMTKredit,
                TATKredit = peserta.TATKredit,

                ProvinsiList = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Provinsi.OrderBy(m => m.Nama).ToList(), "Id", "Nama", peserta.Provinsi),
                KotaList = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Kota.OrderBy(m => m.Nama).ToList(), "Id", "Nama", peserta.Kota),
                KecamatanList = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Kecamatan.OrderBy(m => m.Nama).ToList(), "Id", "Nama", peserta.Kecamatan),
                KelurahanList = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Kelurahan.OrderBy(m => m.Nama).ToList(), "Id", "Nama", peserta.Kelurahan)

            };

            return View(model);
        }

        [HttpGet]
        public IActionResult GetKota(int provinsiId)
        {
            var list = _context.Kota
                .Where(k => k.ProvinsiId == provinsiId)
                .OrderBy(k => k.Nama)
                .Select(k => new { id = k.Id, name = k.Nama })
                .ToList();

            return Json(list);
        }

        [HttpGet]
        public IActionResult GetKecamatan(int kotaId)
        {
            var list = _context.Kecamatan
                .Where(k => k.KotaId == kotaId)
                .OrderBy(k => k.Nama)
                .Select(k => new { id = k.Id, name = k.Nama })
                .ToList();

            return Json(list);
        }

        [HttpGet]
        public IActionResult GetKelurahan(int kecamatanId)
        {
            var list = _context.Kelurahan
                .Where(k => k.KecamatanId == kecamatanId)
                .OrderBy(k => k.Nama)
                .Select(k => new { id = k.Id, name = k.Nama })
                .ToList();

            return Json(list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Flaging(FlagingPensiunViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Rebuild select lists
                model.ProvinsiList = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Provinsi.OrderBy(m => m.Nama).ToList(), "Id", "Nama", model.Provinsi);
                model.KotaList = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Kota.OrderBy(m => m.Nama).ToList(), "Id", "Nama", model.Kota);
                model.KecamatanList = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Kecamatan.OrderBy(m => m.Nama).ToList(), "Id", "Nama", model.Kecamatan);
                model.KelurahanList = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Kelurahan.OrderBy(m => m.Nama).ToList(), "Id", "Nama", model.Kelurahan);
                return View(model);
            }

            // Handle file upload
            if (model.SuratPernyataan != null && model.SuratPernyataan.Length > 0)
            {
                var uploads = Path.Combine("wwwroot", "uploads");
                if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);

                var fileName = Path.GetRandomFileName() + Path.GetExtension(model.SuratPernyataan.FileName);
                var filePath = Path.Combine(uploads, fileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    await model.SuratPernyataan.CopyToAsync(stream);
                }
            }

            // TODO: map model back to Peserta or store Flaging record. For now just redirect with success message.
            TempData["FlashMessage"] = "Flaging berhasil disimpan.";
            return RedirectToAction("Index");
        }
    }
}
