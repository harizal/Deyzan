using Flagging.Taspen.Web.DBContext;
using Flagging.Taspen.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            var peserta = _context.Peserta.FirstOrDefault(m => m.NIP.Contains(query) || m.Notas.Contains(query));
            if (peserta == null)
                throw new Exception("This is a dummy endpoint. Replace with actual implementation.");
            var pesertaViewModel = new PesertaViewModel
            {
                ID = peserta.Id,
                Nama = Helpers.Utils.MaskName(peserta.Nama),
                TanggalLahir = peserta.TanggalLahir.ToString("yyyy-MM"),
                TanggalBUP = "",
                Instansi = peserta.Instansi
            };

            return Json(new BaseViewModel<PesertaViewModel>
            {
                IsSuccess = true,
                Message = "Data found",
                Data = pesertaViewModel
            });
        }
    }
}
