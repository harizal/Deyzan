using System;
using Flagging.Taspen.Web.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Flagging.Taspen.Web.Migrations
{
    [DbContext(typeof(AppDataContext))]
    [Migration("20260420120000_AddBookingFieldsToPeserta")]
    partial class AddBookingFieldsToPeserta
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7");

            modelBuilder.Entity("Flagging.Taspen.Web.Models.Peserta", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Alamat")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Instansi")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("IsBookingDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsBooking")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Kecamatan")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Kelurahan")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Kota")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NIK")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NIP")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nama")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NoKPE")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Notas")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Provinsi")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RekKredit")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RekTabungan")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Surat")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TATKredit")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TMTKredit")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TanggalLahir")
                        .HasColumnType("TEXT");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Peserta");
                });
#pragma warning restore 612, 618
        }
    }
}
