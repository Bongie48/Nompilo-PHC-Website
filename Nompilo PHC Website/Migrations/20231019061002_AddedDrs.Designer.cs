﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nompilo_PHC_Website.Data;

#nullable disable

namespace Nompilo_PHC_Website.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231019061002_AddedDrs")]
    partial class AddedDrs
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Identity")
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", "Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", "Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", "Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", "Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", "Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", "Identity");
                });

            modelBuilder.Entity("Nompilo_PHC_Website.Data.DataGeeksUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", "Identity");
                });

            modelBuilder.Entity("Nompilo_PHC_Website.Models.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("datetimes")
                        .HasColumnType("datetime2");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("services")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Bookings", "Identity");
                });

            modelBuilder.Entity("Nompilo_PHC_Website.Models.Chronicpatients", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Chronicdisease")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Chronicpatients", "Identity");
                });

            modelBuilder.Entity("Nompilo_PHC_Website.Models.ContraceptiveRecordN", b =>
                {
                    b.Property<int>("CRId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CRId"), 1L, 1);

                    b.Property<string>("BloodPressure")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CurrentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailAddres")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdentityNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastMenstrualPeriod")
                        .HasColumnType("datetime2");

                    b.Property<string>("Method")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MonthYear")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReturnDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TestResults")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Weight")
                        .HasColumnType("real");

                    b.Property<string>("Year")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CRId");

                    b.ToTable("ContraceptiveRecordN", "Identity");
                });

            modelBuilder.Entity("Nompilo_PHC_Website.Models.ContraRegister", b =>
                {
                    b.Property<int>("RegId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RegId"), 1L, 1);

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdentityNumber")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.HasKey("RegId");

                    b.ToTable("ContraRegister", "Identity");
                });

            modelBuilder.Entity("Nompilo_PHC_Website.Models.Doctors", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DoctorSurname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Qualifications")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Speciality")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tittle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("doctors", "Identity");
                });

            modelBuilder.Entity("Nompilo_PHC_Website.Models.EverithingPeriodsF", b =>
                {
                    b.Property<int>("PeriodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PeriodId"), 1L, 1);

                    b.Property<string>("Abnormality")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DayDifference")
                        .HasColumnType("int");

                    b.Property<string>("EmailAdd")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("End")
                        .HasColumnType("datetime2");

                    b.Property<string>("MonthYear")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NextPeriod")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Ovulation1")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PregLastDay")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PregnancyPrediction")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.Property<string>("Symptoms")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Year")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PeriodId");

                    b.ToTable("EverithingPeriodsF", "Identity");
                });

            modelBuilder.Entity("Nompilo_PHC_Website.Models.FPregnancy", b =>
                {
                    b.Property<int>("PregId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PregId"), 1L, 1);

                    b.Property<string>("Results")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PregId");

                    b.ToTable("FPregnancy", "Identity");
                });

            modelBuilder.Entity("Nompilo_PHC_Website.Models.FpregRefers", b =>
                {
                    b.Property<int>("RefId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RefId"), 1L, 1);

                    b.Property<DateTime>("RefDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("RefLastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RefId");

                    b.ToTable("FpregRefers", "Identity");
                });

            modelBuilder.Entity("Nompilo_PHC_Website.Models.Instrument", b =>
                {
                    b.Property<int>("instrumentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("instrumentId"), 1L, 1);

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("testId")
                        .HasColumnType("int");

                    b.HasKey("instrumentId");

                    b.HasIndex("testId");

                    b.ToTable("Instruments", "Identity");
                });

            modelBuilder.Entity("Nompilo_PHC_Website.Models.LogSyms", b =>
                {
                    b.Property<int>("SymId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SymId"), 1L, 1);

                    b.Property<DateTime>("Day")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailAdd")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Symptoms")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SymId");

                    b.ToTable("LogSyms", "Identity");
                });

            modelBuilder.Entity("Nompilo_PHC_Website.Models.MigTest", b =>
                {
                    b.Property<int>("instId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("instId"), 1L, 1);

                    b.Property<string>("Migdescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("instId");

                    b.ToTable("MigTests", "Identity");
                });

            modelBuilder.Entity("Nompilo_PHC_Website.Models.PrenatalPatients", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Months")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PrenatalPatients", "Identity");
                });

            modelBuilder.Entity("Nompilo_PHC_Website.Models.RegisterFPatients", b =>
                {
                    b.Property<int>("RegId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RegId"), 1L, 1);

                    b.Property<string>("CheckRegister")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RegId");

                    b.ToTable("RegisterFPatients", "Identity");
                });

            modelBuilder.Entity("Nompilo_PHC_Website.Models.Reminder", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Days")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Time")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Reminder", "Identity");
                });

            modelBuilder.Entity("Nompilo_PHC_Website.Models.RepOption", b =>
                {
                    b.Property<int>("RepId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RepId"), 1L, 1);

                    b.Property<string>("ReportOption")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RepId");

                    b.ToTable("RepOption", "Identity");
                });

            modelBuilder.Entity("Nompilo_PHC_Website.Models.Rvalues", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("value")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Rvalues", "Identity");
                });

            modelBuilder.Entity("Nompilo_PHC_Website.Models.Test", b =>
                {
                    b.Property<int>("testId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("testId"), 1L, 1);

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("pName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("pSurname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("testStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("testfor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("testId");

                    b.ToTable("Tests", "Identity");
                });

            modelBuilder.Entity("Nompilo_PHC_Website.Models.TestMethod", b =>
                {
                    b.Property<int>("methodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("methodId"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MethodName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("instrumentId")
                        .HasColumnType("int");

                    b.Property<int>("testId")
                        .HasColumnType("int");

                    b.HasKey("methodId");

                    b.HasIndex("instrumentId");

                    b.HasIndex("testId");

                    b.ToTable("TestMethods", "Identity");
                });

            modelBuilder.Entity("Nompilo_PHC_Website.Models.TestResult", b =>
                {
                    b.Property<int>("resultsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("resultsId"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Result")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("testId")
                        .HasColumnType("int");

                    b.Property<int>("testmethodId")
                        .HasColumnType("int");

                    b.HasKey("resultsId");

                    b.HasIndex("UserId");

                    b.HasIndex("testId");

                    b.HasIndex("testmethodId");

                    b.ToTable("TestResults", "Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Nompilo_PHC_Website.Data.DataGeeksUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Nompilo_PHC_Website.Data.DataGeeksUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nompilo_PHC_Website.Data.DataGeeksUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Nompilo_PHC_Website.Data.DataGeeksUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Nompilo_PHC_Website.Models.Instrument", b =>
                {
                    b.HasOne("Nompilo_PHC_Website.Models.Test", "Tests")
                        .WithMany("Instruments")
                        .HasForeignKey("testId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tests");
                });

            modelBuilder.Entity("Nompilo_PHC_Website.Models.TestMethod", b =>
                {
                    b.HasOne("Nompilo_PHC_Website.Models.Instrument", "Instruments")
                        .WithMany("TestMethods")
                        .HasForeignKey("instrumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nompilo_PHC_Website.Models.Test", "Tests")
                        .WithMany("TestMethods")
                        .HasForeignKey("testId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Instruments");

                    b.Navigation("Tests");
                });

            modelBuilder.Entity("Nompilo_PHC_Website.Models.TestResult", b =>
                {
                    b.HasOne("Nompilo_PHC_Website.Data.DataGeeksUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nompilo_PHC_Website.Models.Test", "Tests")
                        .WithMany("TestResults")
                        .HasForeignKey("testId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nompilo_PHC_Website.Models.TestMethod", "TestMethods")
                        .WithMany()
                        .HasForeignKey("testmethodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TestMethods");

                    b.Navigation("Tests");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Nompilo_PHC_Website.Models.Instrument", b =>
                {
                    b.Navigation("TestMethods");
                });

            modelBuilder.Entity("Nompilo_PHC_Website.Models.Test", b =>
                {
                    b.Navigation("Instruments");

                    b.Navigation("TestMethods");

                    b.Navigation("TestResults");
                });
#pragma warning restore 612, 618
        }
    }
}
