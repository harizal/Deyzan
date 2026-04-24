namespace Flagging.Taspen.Web.Models
{
    public enum KotaTipe
    {
        Kota,
        Kabupaten
    }

    public enum KelurahanTipe
    {
        Kelurahan,
        Desa
    }

    public class Peserta : BaseModel
    {
        public string NIP { get; set; }
        public string Notas { get; set; }
        public string NoKPE { get; set; }
        public string Nama { get; set; }
        public DateTime TanggalLahir { get; set; }
        public string Instansi { get; set; }
        public string IdProvinsi { get; set; }
        public string Provinsi { get; set; }
        public string IdKota { get; set; }
        public string Kota { get; set; }
        public string IdKecamatan { get; set; }
        public string Kecamatan { get; set; }
        public string IdKelurahan { get; set; }
        public string Kelurahan { get; set; }
        public string Alamat { get; set; }
        public string RekKredit { get; set; }
        public string RekTabungan { get; set; }
        public string NIK { get; set; }
        public string Surat { get; set; }
        public DateTime TMTKredit { get; set; }
        public DateTime TATKredit { get; set; }

        public bool IsBooking { get; set; }
        public DateTime? IsBookingDate { get; set; }

        public bool IsFlaging { get; set; }
        public DateTime? IsFlagingDate { get; set; }
    }

    public class Provinsi
    {
        public int Id { get; set; }
        public string Kode { get; set; }        // "32" = Jawa Barat
        public string Nama { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Kota> Kota { get; set; } = new List<Kota>();
    }

    public class Kota
    {
        public int Id { get; set; }
        public string Kode { get; set; }        // "3204" = Kab. Bandung
        public string Nama { get; set; }
        public KotaTipe Tipe { get; set; }

        public int ProvinsiId { get; set; }
        public Provinsi Provinsi { get; set; }

        public ICollection<Kecamatan> Kecamatan { get; set; } = new List<Kecamatan>();
    }

    public class Kecamatan
    {
        public int Id { get; set; }
        public string Kode { get; set; }        // "320401" = Kec. Ciwidey
        public string Nama { get; set; }

        public int KotaId { get; set; }
        public Kota Kota { get; set; }

        public ICollection<Kelurahan> Kelurahan { get; set; } = new List<Kelurahan>();
    }

    public class Kelurahan
    {
        public int Id { get; set; }
        public string Kode { get; set; }        // "3204011001"
        public string Nama { get; set; }
        public string KodePos { get; set; }
        public KelurahanTipe Tipe { get; set; }

        public int KecamatanId { get; set; }
        public Kecamatan Kecamatan { get; set; }
    }
}
