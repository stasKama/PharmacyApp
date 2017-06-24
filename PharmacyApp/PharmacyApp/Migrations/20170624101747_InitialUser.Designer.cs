using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PharmacyApp.Models;

namespace PharmacyApp.Migrations
{
    [DbContext(typeof(PharmacyContext))]
    [Migration("20170624101747_InitialUser")]
    partial class InitialUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PharmacyApp.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CountryId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("PharmacyApp.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("PharmacyApp.Models.InternationalName", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("InternationalNames");
                });

            modelBuilder.Entity("PharmacyApp.Models.Medicine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AvailabilityPharmacy");

                    b.Property<int>("CompanyId");

                    b.Property<string>("Description");

                    b.Property<int>("Dosage");

                    b.Property<int>("InternationalNameId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<bool>("PrescriptionMedicine");

                    b.Property<decimal>("Price");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("InternationalNameId");

                    b.ToTable("Medicines");
                });

            modelBuilder.Entity("PharmacyApp.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("PharmacyApp.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Password");

                    b.Property<int?>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PharmacyApp.Models.Company", b =>
                {
                    b.HasOne("PharmacyApp.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PharmacyApp.Models.Medicine", b =>
                {
                    b.HasOne("PharmacyApp.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PharmacyApp.Models.InternationalName", "InternationalName")
                        .WithMany()
                        .HasForeignKey("InternationalNameId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PharmacyApp.Models.User", b =>
                {
                    b.HasOne("PharmacyApp.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId");
                });
        }
    }
}
