using Flagging.Taspen.Web.DBContext;
using Flagging.Taspen.Web.Models;
using Flagging.Taspen.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Flagging.Taspen.Web.Controllers
{
    [Authorize]
    public class PesertaController : Controller
    {
        private readonly AppDataContext _context;

        public PesertaController(AppDataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetPesertaData()
        {
            var data = _context.Peserta
                .Select(p => new
                {
                    p.Id,
                    p.NIP,
                    p.Notas,
                    p.NoKPE,
                    p.Nama,
                    p.Instansi,
                    p.NIK,
                    p.NoTel,
                    TanggalLahir = p.TanggalLahir.ToString("yyyy-MM-dd")
                })
                .ToList();

            return Json(new { data });
        }

        private PesertaCreateEditViewModel BuildSelectLists(PesertaCreateEditViewModel model)
        {
            model.ProvinsiList = new SelectList(_context.Provinsi.OrderBy(m => m.Nama).ToList(), "Id", "Nama", model.IdProvinsi);
            model.KotaList = new SelectList(_context.Kota.OrderBy(m => m.Nama).ToList(), "Id", "Nama", model.IdKota);
            model.KecamatanList = new SelectList(_context.Kecamatan.OrderBy(m => m.Nama).ToList(), "Id", "Nama", model.IdKecamatan);
            model.KelurahanList = new SelectList(_context.Kelurahan.OrderBy(m => m.Nama).ToList(), "Id", "Nama", model.IdKelurahan);
            return model;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = BuildSelectLists(new PesertaCreateEditViewModel());
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PesertaCreateEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, message = string.Join(" ", errors) });
            }

            var peserta = new Peserta
            {
                Id = Guid.NewGuid().ToString(),
                NIP = model.NIP,
                Notas = model.Notas,
                NoKPE = model.NoKPE ?? "",
                Nama = model.Nama,
                TanggalLahir = model.TanggalLahir,
                Instansi = model.Instansi,
                IdProvinsi = model.IdProvinsi,
                Provinsi = _context.Provinsi.FirstOrDefault(p => p.Id.ToString() == model.IdProvinsi)?.Nama ?? "",
                IdKota = model.IdKota,
                Kota = _context.Kota.FirstOrDefault(k => k.Id.ToString() == model.IdKota)?.Nama ?? "",
                IdKecamatan = model.IdKecamatan,
                Kecamatan = _context.Kecamatan.FirstOrDefault(k => k.Id.ToString() == model.IdKecamatan)?.Nama ?? "",
                IdKelurahan = model.IdKelurahan,
                Kelurahan = _context.Kelurahan.FirstOrDefault(k => k.Id.ToString() == model.IdKelurahan)?.Nama ?? "",
                Alamat = model.Alamat,
                RekKredit = model.RekKredit ?? "",
                RekTabungan = model.RekTabungan ?? "",
                NIK = model.NIK ?? "",
                Surat = model.Surat ?? "",
                TMTKredit = model.TMTKredit ?? default,
                TATKredit = model.TATKredit ?? default,
                NoTel = model.NoTel ?? "",
                CreatedDate = DateTime.Now,
                CreatedBy = User.Identity?.Name
            };

            if (model.SuratPernyataan != null && model.SuratPernyataan.Length > 0)
            {
                if (model.SuratPernyataan.Length > 1024 * 1024) // 1 MB
                {
                    return Json(new { success = false, message = "File terlalu besar. Maksimal 1 MB." });
                }

                var uploads = Path.Combine("wwwroot", "uploads");
                if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.SuratPernyataan.FileName);
                var filePath = Path.Combine(uploads, fileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    await model.SuratPernyataan.CopyToAsync(stream);
                }
                peserta.Surat = fileName;
            }

            _context.Peserta.Add(peserta);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Peserta berhasil ditambahkan", redirectUrl = Url.Action("Index", "Peserta") });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var peserta = await _context.Peserta.FirstOrDefaultAsync(m => m.Id == id);
            if (peserta == null)
                return RedirectToAction("Index");

            var model = new PesertaCreateEditViewModel
            {
                ID = peserta.Id,
                NIP = peserta.NIP,
                Notas = peserta.Notas,
                NoKPE = peserta.NoKPE,
                Nama = peserta.Nama,
                TanggalLahir = peserta.TanggalLahir,
                Instansi = peserta.Instansi,
                IdProvinsi = peserta.IdProvinsi,
                Provinsi = peserta.Provinsi,
                IdKota = peserta.IdKota,
                Kota = peserta.Kota,
                IdKecamatan = peserta.IdKecamatan,
                Kecamatan = peserta.Kecamatan,
                IdKelurahan = peserta.IdKelurahan,
                Kelurahan = peserta.Kelurahan,
                Alamat = peserta.Alamat,
                RekKredit = peserta.RekKredit,
                RekTabungan = peserta.RekTabungan,
                NIK = peserta.NIK,
                Surat = peserta.Surat,
                TMTKredit = peserta.TMTKredit,
                TATKredit = peserta.TATKredit,
                NoTel = peserta.NoTel
            };

            return View(BuildSelectLists(model));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PesertaCreateEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, message = string.Join(" ", errors) });
            }

            var peserta = await _context.Peserta.FirstOrDefaultAsync(p => p.Id == model.ID);
            if (peserta == null)
            {
                return Json(new { success = false, message = "Peserta tidak ditemukan" });
            }

            peserta.NIP = model.NIP;
            peserta.Notas = model.Notas;
            peserta.NoKPE = model.NoKPE ?? "";
            peserta.Nama = model.Nama;
            peserta.TanggalLahir = model.TanggalLahir;
            peserta.Instansi = model.Instansi;
            peserta.IdProvinsi = model.IdProvinsi;
            peserta.Provinsi = _context.Provinsi.FirstOrDefault(p => p.Id.ToString() == model.IdProvinsi)?.Nama ?? "";
            peserta.IdKota = model.IdKota;
            peserta.Kota = _context.Kota.FirstOrDefault(k => k.Id.ToString() == model.IdKota)?.Nama ?? "";
            peserta.IdKecamatan = model.IdKecamatan;
            peserta.Kecamatan = _context.Kecamatan.FirstOrDefault(k => k.Id.ToString() == model.IdKecamatan)?.Nama ?? "";
            peserta.IdKelurahan = model.IdKelurahan;
            peserta.Kelurahan = _context.Kelurahan.FirstOrDefault(k => k.Id.ToString() == model.IdKelurahan)?.Nama ?? "";
            peserta.Alamat = model.Alamat;
            peserta.RekKredit = model.RekKredit ?? "";
            peserta.RekTabungan = model.RekTabungan ?? "";
            peserta.NIK = model.NIK ?? "";
            peserta.TMTKredit = model.TMTKredit ?? default;
            peserta.TATKredit = model.TATKredit ?? default;
            peserta.NoTel = model.NoTel ?? "";
            peserta.UpdatedDate = DateTime.Now;
            peserta.UpdatedBy = User.Identity?.Name;

            if (model.SuratPernyataan != null && model.SuratPernyataan.Length > 0)
            {
                if (model.SuratPernyataan.Length > 1024 * 1024) // 1 MB
                {
                    return Json(new { success = false, message = "File terlalu besar. Maksimal 1 MB." });
                }

                var uploads = Path.Combine("wwwroot", "uploads");
                if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.SuratPernyataan.FileName);
                var filePath = Path.Combine(uploads, fileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    await model.SuratPernyataan.CopyToAsync(stream);
                }
                peserta.Surat = fileName;
            }

            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Peserta berhasil diperbarui", redirectUrl = Url.Action("Index", "Peserta") });
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
    }
}
