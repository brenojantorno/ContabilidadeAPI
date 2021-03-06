// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApp.Data;

namespace WebApp.Migrations
{
    [DbContext(typeof(WebAppContext))]
    [Migration("20210623020021_UserMigrations")]
    partial class UserMigrations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.6");

            modelBuilder.Entity("WebApp.Models.Expenditure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_Expenditure");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Fixed")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("IdNatureExpenditure")
                        .HasColumnType("int")
                        .HasColumnName("id_NatureExpenditure");

                    b.Property<int>("IdPerson")
                        .HasColumnType("int")
                        .HasColumnName("id_Pessoa");

                    b.Property<string>("Observation")
                        .HasColumnType("longtext");

                    b.Property<decimal>("Value")
                        .HasPrecision(15, 2)
                        .HasColumnType("numeric(15,2)");

                    b.HasKey("Id");

                    b.HasIndex("IdNatureExpenditure");

                    b.HasIndex("IdPerson");

                    b.ToTable("Expenditure");
                });

            modelBuilder.Entity("WebApp.Models.FinancialTransfer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_FinancialTransfer");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("IdPerson")
                        .HasColumnType("int")
                        .HasColumnName("id_Person");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("Value")
                        .HasPrecision(15, 2)
                        .HasColumnType("numeric(15,2)");

                    b.HasKey("Id");

                    b.HasIndex("IdPerson");

                    b.ToTable("FinancialTransfer");
                });

            modelBuilder.Entity("WebApp.Models.NatureExpenditure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_NatureExpenditure");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("NatureExpenditure");
                });

            modelBuilder.Entity("WebApp.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_Person");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("WebApp.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_User");

                    b.Property<int>("IdPerson")
                        .HasColumnType("int");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Passsword")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("IdPerson");

                    b.ToTable("User");
                });

            modelBuilder.Entity("WebApp.Security.SessionUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_Session");

                    b.Property<string>("Browser")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("idUser")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("idUser");

                    b.ToTable("SessionUser");
                });

            modelBuilder.Entity("WebApp.Models.Expenditure", b =>
                {
                    b.HasOne("WebApp.Models.NatureExpenditure", "NatureExpenditure")
                        .WithMany("Expenditures")
                        .HasForeignKey("IdNatureExpenditure")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApp.Models.Person", "Person")
                        .WithMany("Expenditures")
                        .HasForeignKey("IdPerson")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NatureExpenditure");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("WebApp.Models.FinancialTransfer", b =>
                {
                    b.HasOne("WebApp.Models.Person", "Person")
                        .WithMany("FinancialTransfers")
                        .HasForeignKey("IdPerson")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("WebApp.Models.User", b =>
                {
                    b.HasOne("WebApp.Models.Person", "Person")
                        .WithMany("Users")
                        .HasForeignKey("IdPerson")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("WebApp.Security.SessionUser", b =>
                {
                    b.HasOne("WebApp.Models.User", "User")
                        .WithMany("Sessions")
                        .HasForeignKey("idUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebApp.Models.NatureExpenditure", b =>
                {
                    b.Navigation("Expenditures");
                });

            modelBuilder.Entity("WebApp.Models.Person", b =>
                {
                    b.Navigation("Expenditures");

                    b.Navigation("FinancialTransfers");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("WebApp.Models.User", b =>
                {
                    b.Navigation("Sessions");
                });
#pragma warning restore 612, 618
        }
    }
}
