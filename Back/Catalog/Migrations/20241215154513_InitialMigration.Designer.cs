﻿// <auto-generated />
using System;
using Catalog.Host.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Catalog.Host.Migrations
{
    [DbContext(typeof(CatalogDbContext))]
    [Migration("20241215154513_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.HasSequence("color_hilo")
                .IncrementsBy(10);

            modelBuilder.HasSequence("groupe_hilo")
                .IncrementsBy(10);

            modelBuilder.HasSequence("item_hilo")
                .IncrementsBy(10);

            modelBuilder.HasSequence("size_hilo")
                .IncrementsBy(10);

            modelBuilder.HasSequence("type_hilo")
                .IncrementsBy(10);

            modelBuilder.HasSequence("uniqueItem_hilo")
                .IncrementsBy(10);

            modelBuilder.Entity("Catalog.Host.Data.Entities.ColorEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "color_hilo");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Color", (string)null);
                });

            modelBuilder.Entity("Catalog.Host.Data.Entities.GroupeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "groupe_hilo");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Groupe", (string)null);
                });

            modelBuilder.Entity("Catalog.Host.Data.Entities.ItemEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "item_hilo");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<double>("Discount")
                        .HasColumnType("double precision");

                    b.Property<int>("GroupeId")
                        .HasColumnType("integer");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<int>("Sex")
                        .HasColumnType("integer");

                    b.Property<int>("Sold")
                        .HasColumnType("integer");

                    b.Property<int>("TypeId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("GroupeId");

                    b.HasIndex("TypeId");

                    b.ToTable("Item", (string)null);
                });

            modelBuilder.Entity("Catalog.Host.Data.Entities.SizeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "size_hilo");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Size", (string)null);
                });

            modelBuilder.Entity("Catalog.Host.Data.Entities.TypeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "type_hilo");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Type", (string)null);
                });

            modelBuilder.Entity("Catalog.Host.Data.Entities.UniqueItemEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "uniqueItem_hilo");

                    b.Property<int>("ColorId")
                        .HasColumnType("integer");

                    b.Property<int>("ItemId")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<int>("SizeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ColorId");

                    b.HasIndex("ItemId");

                    b.HasIndex("SizeId");

                    b.ToTable("UniqueItem", (string)null);
                });

            modelBuilder.Entity("Catalog.Host.Data.Entities.ItemEntity", b =>
                {
                    b.HasOne("Catalog.Host.Data.Entities.GroupeEntity", "Groupe")
                        .WithMany("Items")
                        .HasForeignKey("GroupeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Catalog.Host.Data.Entities.TypeEntity", "Type")
                        .WithMany("Items")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Groupe");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Catalog.Host.Data.Entities.UniqueItemEntity", b =>
                {
                    b.HasOne("Catalog.Host.Data.Entities.ColorEntity", "Color")
                        .WithMany("UniqueItems")
                        .HasForeignKey("ColorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Catalog.Host.Data.Entities.ItemEntity", "Item")
                        .WithMany("UniqueItems")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Catalog.Host.Data.Entities.SizeEntity", "Size")
                        .WithMany("UniqueItems")
                        .HasForeignKey("SizeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Color");

                    b.Navigation("Item");

                    b.Navigation("Size");
                });

            modelBuilder.Entity("Catalog.Host.Data.Entities.ColorEntity", b =>
                {
                    b.Navigation("UniqueItems");
                });

            modelBuilder.Entity("Catalog.Host.Data.Entities.GroupeEntity", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Catalog.Host.Data.Entities.ItemEntity", b =>
                {
                    b.Navigation("UniqueItems");
                });

            modelBuilder.Entity("Catalog.Host.Data.Entities.SizeEntity", b =>
                {
                    b.Navigation("UniqueItems");
                });

            modelBuilder.Entity("Catalog.Host.Data.Entities.TypeEntity", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
