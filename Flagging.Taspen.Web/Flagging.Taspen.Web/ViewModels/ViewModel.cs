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

    public class FlagingPensiunViewModel
    {
        public string NIP { get; set; }
        public string Notas { get; set; }
        public string NoKPE { get; set; }
        public string Nama { get; set; }
        public DateTime TanggalLahir { get; set; }
        public string Instansi { get; set; }

        public string Provinsi { get; set; }
        public string Kota { get; set; }
        public string Kecamatan { get; set; }
        public string Kelurahan { get; set; }
        public string Alamat { get; set; }
        public string NoRekeningKredit { get; set; }
        public string NoRekeningTabungan { get; set; }
        public string NIK { get; set; }
        public IFormFile SuratPernyataan { get; set; }
        public DateTime TMTKredit { get; set; }
        public DateTime TATKredit { get; set; }
        public string NoTel { get; set; }
    }
}