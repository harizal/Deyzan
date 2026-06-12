using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Flagging.Taspen.Web.ViewModels
{
    public class PesertaViewModel
    {
        public string ID { get; set; }
        public string Nama { get; set; }
        public string TanggalLahir { get; set; }
        public string TanggalBUP { get; set; }
        public string Instansi { get; set; }
        public string Status { get; set; } = "Belum Booking Pensiun";
        public bool IsBooking { get; set; }
        public string StatusFlaging { get; set; } = "Belum Flaging Pensiun";
        public bool IsFlaging { get; set; }
    }

    public class PesertaCreateEditViewModel
    {
        public string? ID { get; set; }

        [Required]
        public string NIP { get; set; }
        [Required]
        public string Notas { get; set; }
        public string? NoKPE { get; set; }
        [Required]
        public string Nama { get; set; }
        [Required]
        public DateTime TanggalLahir { get; set; }
        [Required]
        public string Instansi { get; set; }

        [Required]
        public string IdProvinsi { get; set; }
        public string? Provinsi { get; set; }
        [Required]
        public string IdKota { get; set; }
        public string? Kota { get; set; }
        [Required]
        public string IdKecamatan { get; set; }
        public string? Kecamatan { get; set; }
        [Required]
        public string IdKelurahan { get; set; }
        public string? Kelurahan { get; set; }
        [Required]
        public string Alamat { get; set; }
        public string? RekKredit { get; set; }
        public string? RekTabungan { get; set; }
        public string? NIK { get; set; }
        public string? Surat { get; set; }
        public IFormFile? SuratPernyataan { get; set; }
        public DateTime? TMTKredit { get; set; }
        public DateTime? TATKredit { get; set; }
        public string? NoTel { get; set; }

        public SelectList? ProvinsiList { get; set; }
        public SelectList? KotaList { get; set; }
        public SelectList? KecamatanList { get; set; }
        public SelectList? KelurahanList { get; set; }
    }

    public class FlagingPensiunViewModel
    {
        public string ID { get; set; }
        public string NIP { get; set; }
        public string Notas { get; set; }
        public string NoKPE { get; set; }
        public string Nama { get; set; }
        public DateTime TanggalLahir { get; set; }
        public string? TanggalBUP { get; set; }
        public string Instansi { get; set; }

        [Required]
        public string IdProvinsi { get; set; }
        // Provinsi (nama) kept for display; IdProvinsi is posted
        public string? Provinsi { get; set; }
        [Required]
        public string IdKota { get; set; }
        public string? Kota { get; set; }
        [Required]
        public string IdKecamatan { get; set; }
        public string? Kecamatan { get; set; }
        [Required]
        public string IdKelurahan { get; set; }
        public string? Kelurahan { get; set; }
        [Required]
        public string Alamat { get; set; }
        [Required]
        public string NoRekeningKredit { get; set; }
        [Required]
        public string NoRekeningTabungan { get; set; }
        [Required]
        public string NIK { get; set; }
        public string? Surat { get; set; }
        public IFormFile? SuratPernyataan { get; set; }
        [Required]
        public DateTime TMTKredit { get; set; }
        [Required]
        public DateTime TATKredit { get; set; }
        [Required]
        public string NoTel { get; set; }

        public SelectList? ProvinsiList { get; set; }
        public SelectList? KotaList { get; set; }
        public SelectList? KecamatanList { get; set; }
        public SelectList? KelurahanList { get; set; }
    }
}