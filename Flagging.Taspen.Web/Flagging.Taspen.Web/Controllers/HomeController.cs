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
            var peserta = await _context.Peserta.FirstOrDefaultAsync(m => m.NIP.Contains(query) || m.Notas.Contains(query));
            if (peserta == null)
                throw new Exception("This is a dummy endpoint. Replace with actual implementation.");

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
    }
}
