﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TotalCost.UI.Entity;

namespace TotalCost.UI.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20161216044022_MyFirstMigration")]
    partial class MyFirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("TotalCost.UI.Entity.Bill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<double>("Sum");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("TotalCost.UI.Entity.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Icon");

                    b.Property<string>("Name");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("TotalCost.UI.Entity.Limit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndDate");

                    b.Property<int?>("GroupId");

                    b.Property<DateTime>("StartDate");

                    b.Property<double>("Sum");

                    b.Property<double>("SumExceed");

                    b.Property<int>("TimeType");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Limits");
                });

            modelBuilder.Entity("TotalCost.UI.Entity.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BillId");

                    b.Property<DateTime>("Date");

                    b.Property<int?>("GroupId");

                    b.Property<string>("Note");

                    b.Property<double>("Sum");

                    b.HasKey("Id");

                    b.HasIndex("BillId");

                    b.HasIndex("GroupId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("TotalCost.UI.Entity.Limit", b =>
                {
                    b.HasOne("TotalCost.UI.Entity.Group", "Group")
                        .WithMany("Limits")
                        .HasForeignKey("GroupId");
                });

            modelBuilder.Entity("TotalCost.UI.Entity.Payment", b =>
                {
                    b.HasOne("TotalCost.UI.Entity.Bill", "Bill")
                        .WithMany("Payments")
                        .HasForeignKey("BillId");

                    b.HasOne("TotalCost.UI.Entity.Group", "Group")
                        .WithMany("Payments")
                        .HasForeignKey("GroupId");
                });
        }
    }
}
