namespace Flagging.Taspen.Web.Models
{
    public class Peserta : BaseModel
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
        public string RekKredit { get; set; }
        public string RekTabungan { get; set; }
        public string NIK { get; set; }
        public string Surat { get; set; }
        public DateTime TMTKredit { get; set; }
        public DateTime TATKredit { get; set; }

    }
}
