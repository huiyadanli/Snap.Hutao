﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Snap.Hutao.Model.Entity.Database;

#nullable disable

namespace Snap.Hutao.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231126113631_AddUserLastUpdateTime")]
    partial class AddUserLastUpdateTime
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("Snap.Hutao.Model.Entity.Achievement", b =>
                {
                    b.Property<Guid>("InnerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ArchiveId")
                        .HasColumnType("TEXT");

                    b.Property<uint>("Current")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("Time")
                        .HasColumnType("TEXT");

                    b.HasKey("InnerId");

                    b.HasIndex("ArchiveId");

                    b.ToTable("achievements");
                });

            modelBuilder.Entity("Snap.Hutao.Model.Entity.AchievementArchive", b =>
                {
                    b.Property<Guid>("InnerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsSelected")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("InnerId");

                    b.ToTable("achievement_archives");
                });

            modelBuilder.Entity("Snap.Hutao.Model.Entity.AvatarInfo", b =>
                {
                    b.Property<Guid>("InnerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("CalculatorRefreshTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("GameRecordRefreshTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Info")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("ShowcaseRefreshTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Uid")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("InnerId");

                    b.ToTable("avatar_infos");
                });

            modelBuilder.Entity("Snap.Hutao.Model.Entity.CultivateEntry", b =>
                {
                    b.Property<Guid>("InnerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<uint>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("InnerId");

                    b.HasIndex("ProjectId");

                    b.ToTable("cultivate_entries");
                });

            modelBuilder.Entity("Snap.Hutao.Model.Entity.CultivateItem", b =>
                {
                    b.Property<Guid>("InnerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Count")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("EntryId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsFinished")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ItemId")
                        .HasColumnType("INTEGER");

                    b.HasKey("InnerId");

                    b.HasIndex("EntryId");

                    b.ToTable("cultivate_items");
                });

            modelBuilder.Entity("Snap.Hutao.Model.Entity.CultivateProject", b =>
                {
                    b.Property<Guid>("InnerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("AttachedUid")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsSelected")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("InnerId");

                    b.ToTable("cultivate_projects");
                });

            modelBuilder.Entity("Snap.Hutao.Model.Entity.DailyNoteEntry", b =>
                {
                    b.Property<Guid>("InnerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("DailyNote")
                        .HasColumnType("TEXT");

                    b.Property<bool>("DailyTaskNotify")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("DailyTaskNotifySuppressed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ExpeditionNotify")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ExpeditionNotifySuppressed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("HomeCoinNotifySuppressed")
                        .HasColumnType("INTEGER");

                    b.Property<int>("HomeCoinNotifyThreshold")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("RefreshTime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("ResinNotifySuppressed")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ResinNotifyThreshold")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("TransformerNotify")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("TransformerNotifySuppressed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Uid")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("InnerId");

                    b.HasIndex("UserId");

                    b.ToTable("daily_notes");
                });

            modelBuilder.Entity("Snap.Hutao.Model.Entity.GachaArchive", b =>
                {
                    b.Property<Guid>("InnerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsSelected")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Uid")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("InnerId");

                    b.ToTable("gacha_archives");
                });

            modelBuilder.Entity("Snap.Hutao.Model.Entity.GachaItem", b =>
                {
                    b.Property<Guid>("InnerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ArchiveId")
                        .HasColumnType("TEXT");

                    b.Property<int>("GachaType")
                        .HasColumnType("INTEGER");

                    b.Property<long>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("ItemId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("QueryType")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("Time")
                        .HasColumnType("TEXT");

                    b.HasKey("InnerId");

                    b.HasIndex("ArchiveId");

                    b.ToTable("gacha_items");
                });

            modelBuilder.Entity("Snap.Hutao.Model.Entity.GameAccount", b =>
                {
                    b.Property<Guid>("InnerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("AttachUid")
                        .HasColumnType("TEXT");

                    b.Property<string>("MihoyoSDK")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("InnerId");

                    b.ToTable("game_accounts");
                });

            modelBuilder.Entity("Snap.Hutao.Model.Entity.InventoryItem", b =>
                {
                    b.Property<Guid>("InnerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<uint>("Count")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("ItemId")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("TEXT");

                    b.HasKey("InnerId");

                    b.HasIndex("ProjectId");

                    b.ToTable("inventory_items");
                });

            modelBuilder.Entity("Snap.Hutao.Model.Entity.InventoryReliquary", b =>
                {
                    b.Property<Guid>("InnerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("AppendPropIdList")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ItemId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Level")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MainPropId")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("TEXT");

                    b.HasKey("InnerId");

                    b.HasIndex("ProjectId");

                    b.ToTable("inventory_reliquaries");
                });

            modelBuilder.Entity("Snap.Hutao.Model.Entity.InventoryWeapon", b =>
                {
                    b.Property<Guid>("InnerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("ItemId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Level")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("TEXT");

                    b.Property<int>("PromoteLevel")
                        .HasColumnType("INTEGER");

                    b.HasKey("InnerId");

                    b.HasIndex("ProjectId");

                    b.ToTable("inventory_weapons");
                });

            modelBuilder.Entity("Snap.Hutao.Model.Entity.ObjectCacheEntry", b =>
                {
                    b.Property<string>("Key")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("ExpireTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("Key");

                    b.ToTable("object_cache");
                });

            modelBuilder.Entity("Snap.Hutao.Model.Entity.SettingEntry", b =>
                {
                    b.Property<string>("Key")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("Key");

                    b.ToTable("settings");
                });

            modelBuilder.Entity("Snap.Hutao.Model.Entity.SpiralAbyssEntry", b =>
                {
                    b.Property<Guid>("InnerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<uint>("ScheduleId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SpiralAbyss")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Uid")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("InnerId");

                    b.ToTable("spiral_abysses");
                });

            modelBuilder.Entity("Snap.Hutao.Model.Entity.User", b =>
                {
                    b.Property<Guid>("InnerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Aid")
                        .HasColumnType("TEXT");

                    b.Property<string>("CookieToken")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("CookieTokenLastUpdateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Fingerprint")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("FingerprintLastUpdateTime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsOversea")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsSelected")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LToken")
                        .HasColumnType("TEXT")
                        .HasColumnName("Ltoken");

                    b.Property<string>("Mid")
                        .HasColumnType("TEXT");

                    b.Property<string>("SToken")
                        .HasColumnType("TEXT")
                        .HasColumnName("Stoken");

                    b.HasKey("InnerId");

                    b.ToTable("users");
                });

            modelBuilder.Entity("Snap.Hutao.Model.Entity.Achievement", b =>
                {
                    b.HasOne("Snap.Hutao.Model.Entity.AchievementArchive", "Archive")
                        .WithMany()
                        .HasForeignKey("ArchiveId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Archive");
                });

            modelBuilder.Entity("Snap.Hutao.Model.Entity.CultivateEntry", b =>
                {
                    b.HasOne("Snap.Hutao.Model.Entity.CultivateProject", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Snap.Hutao.Model.Entity.CultivateItem", b =>
                {
                    b.HasOne("Snap.Hutao.Model.Entity.CultivateEntry", "Entry")
                        .WithMany()
                        .HasForeignKey("EntryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Entry");
                });

            modelBuilder.Entity("Snap.Hutao.Model.Entity.DailyNoteEntry", b =>
                {
                    b.HasOne("Snap.Hutao.Model.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Snap.Hutao.Model.Entity.GachaItem", b =>
                {
                    b.HasOne("Snap.Hutao.Model.Entity.GachaArchive", "Archive")
                        .WithMany()
                        .HasForeignKey("ArchiveId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Archive");
                });

            modelBuilder.Entity("Snap.Hutao.Model.Entity.InventoryItem", b =>
                {
                    b.HasOne("Snap.Hutao.Model.Entity.CultivateProject", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Snap.Hutao.Model.Entity.InventoryReliquary", b =>
                {
                    b.HasOne("Snap.Hutao.Model.Entity.CultivateProject", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Snap.Hutao.Model.Entity.InventoryWeapon", b =>
                {
                    b.HasOne("Snap.Hutao.Model.Entity.CultivateProject", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });
#pragma warning restore 612, 618
        }
    }
}
