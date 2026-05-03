using Flagging.Taspen.Web.DBContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Flagging.Taspen.Web.Controllers
{
    [Authorize]
    public class FlaggingController : Controller
    {
        private readonly AppDataContext _context;

        public FlaggingController(AppDataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetFlaggingData()
        {
            var data = _context.Peserta
                .Where(p => p.IsFlaging)
                .Select(p => new
                {
                    p.Id,
                    p.NIP,
                    p.Notas,
                    p.Nama,
                    p.Instansi,
                    Provinsi = p.Provinsi,
                    Kota = p.Kota,
                    Kecamatan = p.Kecamatan,
                    Kelurahan = p.Kelurahan,
                    Alamat = p.Alamat,
                    NoTel = p.NoTel,
                    TanggalFlaging = p.IsFlagingDate.HasValue ? p.IsFlagingDate.Value.ToString("yyyy-MM-dd HH:mm") : string.Empty
                })
                .ToList();

            return Json(new { data });
        }
    }
}
