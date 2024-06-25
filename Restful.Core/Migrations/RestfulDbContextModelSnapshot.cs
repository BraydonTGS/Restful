﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Restful.Core.Context;

#nullable disable

namespace Restful.Core.Migrations
{
    [DbContext(typeof(RestfulDbContext))]
    partial class RestfulDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.6");

            modelBuilder.Entity("Restful.Entity.Entities.CollectionEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("Id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDirty")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsEntityRegistered")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("TEXT")
                        .HasColumnName("Title");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Collections");
                });

            modelBuilder.Entity("Restful.Entity.Entities.HeaderEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("Id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Enabled")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Enabled");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDirty")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsEntityRegistered")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT")
                        .HasColumnName("Key");

                    b.Property<Guid>("RequestId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT")
                        .HasColumnName("Value");

                    b.HasKey("Id");

                    b.HasIndex("RequestId");

                    b.ToTable("Headers");
                });

            modelBuilder.Entity("Restful.Entity.Entities.ParameterEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("Id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Enabled")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Enabled");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDirty")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsEntityRegistered")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT")
                        .HasColumnName("Key");

                    b.Property<Guid>("RequestId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT")
                        .HasColumnName("Value");

                    b.HasKey("Id");

                    b.HasIndex("RequestId");

                    b.ToTable("Parameters");
                });

            modelBuilder.Entity("Restful.Entity.Entities.PasswordEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("Id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Hash")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("BLOB")
                        .HasColumnName("Hash");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDirty")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsEntityRegistered")
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasColumnType("BLOB")
                        .HasColumnName("Salt");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Passwords");
                });

            modelBuilder.Entity("Restful.Entity.Entities.RequestEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("Id");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Body");

                    b.Property<Guid?>("CollectionId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("HttpMethod")
                        .HasColumnType("INTEGER")
                        .HasColumnName("HttpMethod");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDirty")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsEntityRegistered")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT")
                        .HasColumnName("Name");

                    b.Property<string>("TempResult")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("TempResult");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Url")
                        .HasColumnType("TEXT")
                        .HasColumnName("Url");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CollectionId");

                    b.HasIndex("UserId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("Restful.Entity.Entities.ResponseEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("Id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDirty")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsEntityRegistered")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("RequestId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Result")
                        .HasColumnType("TEXT")
                        .HasColumnName("Result");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RequestId");

                    b.ToTable("Results");
                });

            modelBuilder.Entity("Restful.Entity.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("Id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("TEXT")
                        .HasColumnName("Email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT")
                        .HasColumnName("FirstName");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDirty")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsEntityRegistered")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("TEXT")
                        .HasColumnName("LastName");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("UserName");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Restful.Entity.Entities.CollectionEntity", b =>
                {
                    b.HasOne("Restful.Entity.Entities.UserEntity", "UserEntity")
                        .WithMany("CollectionEntities")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserEntity");
                });

            modelBuilder.Entity("Restful.Entity.Entities.HeaderEntity", b =>
                {
                    b.HasOne("Restful.Entity.Entities.RequestEntity", "Request")
                        .WithMany("HeaderEntities")
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Request");
                });

            modelBuilder.Entity("Restful.Entity.Entities.ParameterEntity", b =>
                {
                    b.HasOne("Restful.Entity.Entities.RequestEntity", "Request")
                        .WithMany("ParameterEntities")
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Request");
                });

            modelBuilder.Entity("Restful.Entity.Entities.PasswordEntity", b =>
                {
                    b.HasOne("Restful.Entity.Entities.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Restful.Entity.Entities.RequestEntity", b =>
                {
                    b.HasOne("Restful.Entity.Entities.CollectionEntity", "CollectionEntity")
                        .WithMany("Requests")
                        .HasForeignKey("CollectionId");

                    b.HasOne("Restful.Entity.Entities.UserEntity", "UserEntity")
                        .WithMany("RequestEntities")
                        .HasForeignKey("UserId");

                    b.Navigation("CollectionEntity");

                    b.Navigation("UserEntity");
                });

            modelBuilder.Entity("Restful.Entity.Entities.ResponseEntity", b =>
                {
                    b.HasOne("Restful.Entity.Entities.RequestEntity", "Request")
                        .WithMany()
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Request");
                });

            modelBuilder.Entity("Restful.Entity.Entities.CollectionEntity", b =>
                {
                    b.Navigation("Requests");
                });

            modelBuilder.Entity("Restful.Entity.Entities.RequestEntity", b =>
                {
                    b.Navigation("HeaderEntities");

                    b.Navigation("ParameterEntities");
                });

            modelBuilder.Entity("Restful.Entity.Entities.UserEntity", b =>
                {
                    b.Navigation("CollectionEntities");

                    b.Navigation("RequestEntities");
                });
#pragma warning restore 612, 618
        }
    }
}
