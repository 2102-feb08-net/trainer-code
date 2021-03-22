﻿// <auto-generated />
using System;
using EmailApp.DataAccess.EfModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EmailApp.DataAccess.Migrations
{
    [DbContext(typeof(EmailContext))]
    [Migration("20210322161014_AddOutlookAccountSeedData")]
    partial class AddOutlookAccountSeedData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EmailApp.DataAccess.EfModel.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Address")
                        .IsUnique();

                    b.ToTable("Accounts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "nick.escalona@revature.com"
                        },
                        new
                        {
                            Id = 2,
                            Address = "nicholasescalona@outlook.com"
                        });
                });

            modelBuilder.Entity("EmailApp.DataAccess.EfModel.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Body")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FromId")
                        .HasColumnType("int");

                    b.Property<Guid>("Guid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTimeOffset>("OrigDate")
                        .HasColumnType("datetimeoffset(0)");

                    b.Property<string>("Subject")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ToId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FromId");

                    b.HasIndex("Guid")
                        .IsUnique();

                    b.HasIndex("ToId");

                    b.ToTable("Messages");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Body = "this is a message to say hello",
                            FromId = 1,
                            Guid = new Guid("57d462ca-a9ce-4417-b8a4-d9b59907c7a6"),
                            IsDeleted = false,
                            OrigDate = new DateTimeOffset(new DateTime(2021, 3, 20, 22, 37, 10, 0, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)),
                            Subject = "hello",
                            ToId = 2
                        },
                        new
                        {
                            Id = 2,
                            Body = "this is a reply to hello",
                            FromId = 2,
                            Guid = new Guid("bd682c41-68db-4c00-9dd2-814b8013e563"),
                            IsDeleted = false,
                            OrigDate = new DateTimeOffset(new DateTime(2021, 3, 20, 22, 40, 1, 0, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)),
                            Subject = "Re: hello",
                            ToId = 1
                        });
                });

            modelBuilder.Entity("EmailApp.DataAccess.EfModel.Message", b =>
                {
                    b.HasOne("EmailApp.DataAccess.EfModel.Account", "From")
                        .WithMany("SentMessages")
                        .HasForeignKey("FromId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmailApp.DataAccess.EfModel.Account", "To")
                        .WithMany("ReceivedMessages")
                        .HasForeignKey("ToId");

                    b.Navigation("From");

                    b.Navigation("To");
                });

            modelBuilder.Entity("EmailApp.DataAccess.EfModel.Account", b =>
                {
                    b.Navigation("ReceivedMessages");

                    b.Navigation("SentMessages");
                });
#pragma warning restore 612, 618
        }
    }
}
