namespace Flagging.Taspen.Web.ViewModels
{
    public class PesertaViewModel
    {
        public string ID { get; set; }
        public string Nama { get; set; }
        public string TanggalLahir { get; set; }
        public string TanggalBUP { get; set; }
        public string Instansi { get; set; }
        public string Status { get; set; } = "Belum Floging Pensiun";
        public bool IsBooking { get; set; }
    }
}