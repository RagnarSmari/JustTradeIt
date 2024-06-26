﻿// <auto-generated />
using System;
using JustTradeIt.Software.API.Repositories.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JustTradeIt.Software.API.Migrations
{
    [DbContext(typeof(JustTradeItDbContext))]
    [Migration("20211105101844_secondinitial")]
    partial class secondinitial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("JustTradeIt.Software.API.Models.Entities.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ItemConditionId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("OwnerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PublicIdentifier")
                        .HasColumnType("TEXT");

                    b.Property<string>("ShortDescription")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ItemConditionId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("JustTradeIt.Software.API.Models.Entities.ItemCondition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConditionCode")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ItemConditions");
                });

            modelBuilder.Entity("JustTradeIt.Software.API.Models.Entities.ItemImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("TEXT");

                    b.Property<int>("ItemId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.ToTable("ItemImages");
                });

            modelBuilder.Entity("JustTradeIt.Software.API.Models.Entities.JwtToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Blacklisted")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("JwtTokens");
                });

            modelBuilder.Entity("JustTradeIt.Software.API.Models.Entities.Trade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("IssueDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("PublicIdentifier")
                        .HasColumnType("TEXT");

                    b.Property<int>("RecieverId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SenderId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TradeStatus")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RecieverId");

                    b.HasIndex("SenderId");

                    b.ToTable("Trades");
                });

            modelBuilder.Entity("JustTradeIt.Software.API.Models.Entities.TradeItem", b =>
                {
                    b.Property<int>("ItemId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TradeId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ItemId", "TradeId", "UserId");

                    b.HasIndex("TradeId");

                    b.HasIndex("UserId");

                    b.ToTable("TradeItems");
                });

            modelBuilder.Entity("JustTradeIt.Software.API.Models.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .HasColumnType("TEXT");

                    b.Property<string>("HashedPassword")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProfileImageUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("PublicIdentifier")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("JustTradeIt.Software.API.Models.Entities.Item", b =>
                {
                    b.HasOne("JustTradeIt.Software.API.Models.Entities.ItemCondition", "ItemConditionLink")
                        .WithMany()
                        .HasForeignKey("ItemConditionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JustTradeIt.Software.API.Models.Entities.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");

                    b.Navigation("ItemConditionLink");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("JustTradeIt.Software.API.Models.Entities.ItemImage", b =>
                {
                    b.HasOne("JustTradeIt.Software.API.Models.Entities.Item", "Item")
                        .WithMany("ItemImages")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");
                });

            modelBuilder.Entity("JustTradeIt.Software.API.Models.Entities.Trade", b =>
                {
                    b.HasOne("JustTradeIt.Software.API.Models.Entities.User", "Reciever")
                        .WithMany("Recievers")
                        .HasForeignKey("RecieverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JustTradeIt.Software.API.Models.Entities.User", "Sender")
                        .WithMany("Senders")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reciever");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("JustTradeIt.Software.API.Models.Entities.TradeItem", b =>
                {
                    b.HasOne("JustTradeIt.Software.API.Models.Entities.Item", "Item")
                        .WithMany("TradeItems")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JustTradeIt.Software.API.Models.Entities.Trade", "Trade")
                        .WithMany("TradeItems")
                        .HasForeignKey("TradeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JustTradeIt.Software.API.Models.Entities.User", "User")
                        .WithMany("TradeItems")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Trade");

                    b.Navigation("User");
                });

            modelBuilder.Entity("JustTradeIt.Software.API.Models.Entities.Item", b =>
                {
                    b.Navigation("ItemImages");

                    b.Navigation("TradeItems");
                });

            modelBuilder.Entity("JustTradeIt.Software.API.Models.Entities.Trade", b =>
                {
                    b.Navigation("TradeItems");
                });

            modelBuilder.Entity("JustTradeIt.Software.API.Models.Entities.User", b =>
                {
                    b.Navigation("Recievers");

                    b.Navigation("Senders");

                    b.Navigation("TradeItems");
                });
#pragma warning restore 612, 618
        }
    }
}
