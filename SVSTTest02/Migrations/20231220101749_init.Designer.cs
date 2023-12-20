﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SVSTTest02.Data;

#nullable disable

namespace SVSTTest02.Migrations
{
    [DbContext(typeof(AppDataContextModel))]
    [Migration("20231220101749_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SVSTTest002Lib.GAS_VALUESModel", b =>
                {
                    b.Property<int>("GAS_VAL_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("GAS_VAL_ID"));

                    b.Property<DateTime>("GAS_VAL_DATE")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("H2_VAL")
                        .HasColumnType("double precision");

                    b.Property<double>("O2_VAL")
                        .HasColumnType("double precision");

                    b.HasKey("GAS_VAL_ID");

                    b.ToTable("GAS_VALUES");
                });
#pragma warning restore 612, 618
        }
    }
}