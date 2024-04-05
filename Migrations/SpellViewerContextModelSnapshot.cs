﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SpellViewer.Data;

#nullable disable

namespace SpellViewer.Migrations
{
    [DbContext(typeof(SpellViewerContext))]
    partial class SpellViewerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.17")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SpellViewer.Models.Entities.CharacterEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CharacterClass")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CharacterName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CharacterRace")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("CharacterEntity");
                });

            modelBuilder.Entity("SpellViewer.Models.Entities.RoleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RoleEntity");
                });

            modelBuilder.Entity("SpellViewer.Models.Entities.Spell", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Casting_time")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CharacterEntityId")
                        .HasColumnType("int");

                    b.Property<int>("ComponentsId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Duration")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Is_ritual")
                        .HasColumnType("bit");

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Range")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("School")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CharacterEntityId");

                    b.HasIndex("ComponentsId");

                    b.ToTable("Masters_Spells");
                });

            modelBuilder.Entity("SpellViewer.Models.Entities.SpellClassesEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SpellId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SpellId");

                    b.ToTable("SpellClassesEntity");
                });

            modelBuilder.Entity("SpellViewer.Models.Entities.SpellComponentsEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Is_material")
                        .HasColumnType("bit");

                    b.Property<bool>("Is_somatic")
                        .HasColumnType("bit");

                    b.Property<bool>("Is_verbal")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("SpellComponentsEntity");
                });

            modelBuilder.Entity("SpellViewer.Models.Entities.SpellMaterialsEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Materials_needed")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SpellComponentsEntityId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SpellComponentsEntityId");

                    b.ToTable("SpellMaterialsEntity");
                });

            modelBuilder.Entity("SpellViewer.Models.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("MoreData")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SpellViewer.Models.Entities.CharacterEntity", b =>
                {
                    b.HasOne("SpellViewer.Models.Entities.User", null)
                        .WithMany("Characters")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SpellViewer.Models.Entities.RoleEntity", b =>
                {
                    b.HasOne("SpellViewer.Models.Entities.User", null)
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SpellViewer.Models.Entities.Spell", b =>
                {
                    b.HasOne("SpellViewer.Models.Entities.CharacterEntity", null)
                        .WithMany("KnownSpells")
                        .HasForeignKey("CharacterEntityId");

                    b.HasOne("SpellViewer.Models.Entities.SpellComponentsEntity", "Components")
                        .WithMany()
                        .HasForeignKey("ComponentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Components");
                });

            modelBuilder.Entity("SpellViewer.Models.Entities.SpellClassesEntity", b =>
                {
                    b.HasOne("SpellViewer.Models.Entities.Spell", null)
                        .WithMany("Classes")
                        .HasForeignKey("SpellId");
                });

            modelBuilder.Entity("SpellViewer.Models.Entities.SpellMaterialsEntity", b =>
                {
                    b.HasOne("SpellViewer.Models.Entities.SpellComponentsEntity", null)
                        .WithMany("Materials_needed")
                        .HasForeignKey("SpellComponentsEntityId");
                });

            modelBuilder.Entity("SpellViewer.Models.Entities.CharacterEntity", b =>
                {
                    b.Navigation("KnownSpells");
                });

            modelBuilder.Entity("SpellViewer.Models.Entities.Spell", b =>
                {
                    b.Navigation("Classes");
                });

            modelBuilder.Entity("SpellViewer.Models.Entities.SpellComponentsEntity", b =>
                {
                    b.Navigation("Materials_needed");
                });

            modelBuilder.Entity("SpellViewer.Models.Entities.User", b =>
                {
                    b.Navigation("Characters");

                    b.Navigation("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}
