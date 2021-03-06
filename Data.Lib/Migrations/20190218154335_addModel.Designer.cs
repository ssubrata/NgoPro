﻿// <auto-generated />
using System;
using Data.Lib;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Lib.Migrations
{
    [DbContext(typeof(DataDbContext))]
    [Migration("20190218154335_addModel")]
    partial class addModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Data.Lib.Entity.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountNo");

                    b.Property<int>("BookId");

                    b.Property<DateTime>("CreateDate");

                    b.Property<int>("MemberId");

                    b.Property<string>("Status");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("MemberId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Data.Lib.Entity.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BookName");

                    b.Property<bool>("IsActive");

                    b.Property<string>("PaymentAmount");

                    b.Property<int>("PaymentMethodId");

                    b.HasKey("Id");

                    b.HasIndex("PaymentMethodId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("Data.Lib.Entity.Deposit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountId");

                    b.Property<int?>("BookId");

                    b.Property<decimal>("DepositAmount");

                    b.Property<DateTime>("DepositDate");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("BookId");

                    b.ToTable("Deposits");
                });

            modelBuilder.Entity("Data.Lib.Entity.PaymentMethod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PaymentMethodName");

                    b.HasKey("Id");

                    b.ToTable("PaymentMethods");
                });

            modelBuilder.Entity("Data.Lib.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContactNo");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("Email");

                    b.Property<string>("FatherName");

                    b.Property<string>("FullName");

                    b.Property<string>("Gender");

                    b.Property<string>("MemberNo");

                    b.Property<string>("NationalNo");

                    b.Property<string>("PermanentAddress");

                    b.Property<string>("PresentAddress");

                    b.HasKey("Id");

                    b.HasIndex("MemberNo")
                        .IsUnique()
                        .HasFilter("[MemberNo] IS NOT NULL");

                    b.HasIndex("NationalNo")
                        .IsUnique()
                        .HasFilter("[NationalNo] IS NOT NULL");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("Data.Lib.Entity.Account", b =>
                {
                    b.HasOne("Data.Lib.Entity.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Data.Lib.Member", "Member")
                        .WithMany()
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Data.Lib.Entity.Book", b =>
                {
                    b.HasOne("Data.Lib.Entity.PaymentMethod", "PaymentMethod")
                        .WithMany()
                        .HasForeignKey("PaymentMethodId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Data.Lib.Entity.Deposit", b =>
                {
                    b.HasOne("Data.Lib.Entity.Account", "Account")
                        .WithMany("Deposit")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Data.Lib.Entity.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId");
                });
#pragma warning restore 612, 618
        }
    }
}
