﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UserApi.Data;

#nullable disable

namespace UserApi.Migrations
{
    [DbContext(typeof(UserApiContext))]
    [Migration("20220725165958_addCat")]
    partial class addCat
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ApiUserSessions", b =>
                {
                    b.Property<Guid>("SessionsSessionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UsersId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("SessionsSessionId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("ApiUserSessions");
                });

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

                    b.ToTable("AspNetRoles", (string)null);
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

                    b.ToTable("AspNetRoleClaims", (string)null);
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

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("UserApi.Data.ApiUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<int>("Argent")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<Guid?>("IdStage")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("NbSessions")
                        .HasColumnType("int");

                    b.Property<int>("NbSessionsPermis")
                        .HasColumnType("int");

                    b.Property<int>("NbSessionsPolice")
                        .HasColumnType("int");

                    b.Property<string>("Nom")
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

                    b.Property<int>("Permis")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sexe")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("IdStage");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("UserApi.Data.Maker", b =>
                {
                    b.Property<Guid>("IdMaker")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id_Maker")
                        .HasDefaultValueSql("(newid())");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int?>("Founded")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Origin")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Related")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("WikiLink")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("IdMaker");

                    b.ToTable("Maker", (string)null);
                });

            modelBuilder.Entity("UserApi.Data.Markdown", b =>
                {
                    b.Property<Guid>("TextId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CatName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FormatType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RawText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TextId");

                    b.ToTable("Markdown");
                });

            modelBuilder.Entity("UserApi.Data.OriginalCar", b =>
                {
                    b.Property<Guid>("IdCar")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id_Car")
                        .HasDefaultValueSql("(newid())");

                    b.Property<decimal>("Accelerate")
                        .HasColumnType("decimal(8,2)");

                    b.Property<string>("Aspiration")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Aviability")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<decimal>("Braking")
                        .HasColumnType("decimal(8,2)");

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<int>("CarOrdinal")
                        .HasColumnType("int");

                    b.Property<string>("Class")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("EngineConfiguration")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<decimal>("EngineDisplacement")
                        .HasColumnType("decimal(8,2)");

                    b.Property<string>("EnginePosition")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<int>("GearBox")
                        .HasColumnType("int");

                    b.Property<decimal>("Handling")
                        .HasColumnType("decimal(8,2)");

                    b.Property<decimal>("Launch")
                        .HasColumnType("decimal(8,2)");

                    b.Property<Guid>("MakerId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Maker_Id");

                    b.Property<string>("Model")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<int>("NbCylindre")
                        .HasColumnType("int");

                    b.Property<decimal>("Offroad")
                        .HasColumnType("decimal(8,2)");

                    b.Property<int>("Pi")
                        .HasColumnType("int")
                        .HasColumnName("PI");

                    b.Property<string>("PictureLink")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<decimal>("PowerBhp")
                        .HasColumnType("decimal(8,2)")
                        .HasColumnName("PowerBHP");

                    b.Property<decimal>("PowerHp")
                        .HasColumnType("decimal(8,2)")
                        .HasColumnName("PowerKW");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("Rarity")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("RequiredDlc")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("RequiredDLC");

                    b.Property<decimal>("Speed")
                        .HasColumnType("decimal(8,2)");

                    b.Property<decimal>("TorqueLbft")
                        .HasColumnType("decimal(8,2)")
                        .HasColumnName("TorqueLBFT");

                    b.Property<decimal>("TorqueNm")
                        .HasColumnType("decimal(8,2)")
                        .HasColumnName("TorqueNM");

                    b.Property<string>("Transmission")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<decimal>("WeightKg")
                        .HasColumnType("decimal(8,2)")
                        .HasColumnName("WeightKG");

                    b.Property<decimal>("WeightLbs")
                        .HasColumnType("decimal(8,2)")
                        .HasColumnName("WeightLBS");

                    b.Property<string>("WikiLink")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("IdCar")
                        .HasName("PK_Car_1");

                    b.HasIndex(new[] { "MakerId" }, "IX_Car_Maker_Id");

                    b.ToTable("OriginalCar", (string)null);
                });

            modelBuilder.Entity("UserApi.Data.Sessions", b =>
                {
                    b.Property<Guid>("SessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Debut")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NbParticipant")
                        .HasColumnType("int");

                    b.Property<int>("SessionNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SessionNumber"), 1L, 1);

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("SessionId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("UserApi.Data.Stage", b =>
                {
                    b.Property<Guid>("StageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Name")
                        .HasColumnType("int");

                    b.Property<int>("NbSessionsRequis")
                        .HasColumnType("int");

                    b.Property<int>("PermisRequis")
                        .HasColumnType("int");

                    b.Property<int>("StageRequis")
                        .HasColumnType("int");

                    b.HasKey("StageId");

                    b.ToTable("Stage");
                });

            modelBuilder.Entity("UserApi.Data.Voitures", b =>
                {
                    b.Property<Guid>("KeyCar")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Accelerate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Aspiration")
                        .HasColumnType("int");

                    b.Property<decimal>("Braking")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Class")
                        .HasColumnType("int");

                    b.Property<decimal>("EngineDisplacement")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("EnginePosition")
                        .HasColumnType("int");

                    b.Property<int>("GearBox")
                        .HasColumnType("int");

                    b.Property<decimal>("Handling")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("IdCar")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("IdUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Imatriculation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Launch")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("NbCylindre")
                        .HasColumnType("int");

                    b.Property<decimal>("Offroad")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Pi")
                        .HasColumnType("int");

                    b.Property<int>("PowerBHP")
                        .HasColumnType("int");

                    b.Property<int>("PowerKW")
                        .HasColumnType("int");

                    b.Property<int>("PrixModif")
                        .HasColumnType("int");

                    b.Property<int>("PrixTotal")
                        .HasColumnType("int");

                    b.Property<decimal>("Speed")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TorqueLBFT")
                        .HasColumnType("int");

                    b.Property<int>("TorqueNM")
                        .HasColumnType("int");

                    b.Property<string>("Transmission")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WeightKG")
                        .HasColumnType("int");

                    b.Property<int>("WeightLBS")
                        .HasColumnType("int");

                    b.HasKey("KeyCar");

                    b.HasIndex("IdUser");

                    b.ToTable("Voitures");
                });

            modelBuilder.Entity("ApiUserSessions", b =>
                {
                    b.HasOne("UserApi.Data.Sessions", null)
                        .WithMany()
                        .HasForeignKey("SessionsSessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UserApi.Data.ApiUser", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
                    b.HasOne("UserApi.Data.ApiUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("UserApi.Data.ApiUser", null)
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

                    b.HasOne("UserApi.Data.ApiUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("UserApi.Data.ApiUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UserApi.Data.ApiUser", b =>
                {
                    b.HasOne("UserApi.Data.Stage", "Stage")
                        .WithMany("Users")
                        .HasForeignKey("IdStage")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Stage");
                });

            modelBuilder.Entity("UserApi.Data.OriginalCar", b =>
                {
                    b.HasOne("UserApi.Data.Maker", "Maker")
                        .WithMany("Cars")
                        .HasForeignKey("MakerId")
                        .IsRequired()
                        .HasConstraintName("FK_Car_Maker");

                    b.Navigation("Maker");
                });

            modelBuilder.Entity("UserApi.Data.Voitures", b =>
                {
                    b.HasOne("UserApi.Data.ApiUser", "User")
                        .WithMany("Voitures")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("UserApi.Data.ApiUser", b =>
                {
                    b.Navigation("Voitures");
                });

            modelBuilder.Entity("UserApi.Data.Maker", b =>
                {
                    b.Navigation("Cars");
                });

            modelBuilder.Entity("UserApi.Data.Stage", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
