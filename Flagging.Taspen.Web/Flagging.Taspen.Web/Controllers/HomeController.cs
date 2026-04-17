using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Flagging.Taspen.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SearchData(string query)
        {
            await Task.Delay(2000); // Simulate async work, e.g., database call
            throw new Exception("This is a dummy endpoint. Replace with actual implementation.");
            // Dummy data - replace with DB call later
            var dummyData = new
            {
                found = !string.IsNullOrEmpty(query),
                nip = query,
                notas = query,
                nama = "Nama Dummy",
                status = "Aktif"
            };

            return Json(dummyData);
        }
    }
}
