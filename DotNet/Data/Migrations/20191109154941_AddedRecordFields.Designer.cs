﻿// <auto-generated />
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20191109154941_AddedRecordFields")]
    partial class AddedRecordFields
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("API.Entities.Record", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Anamneza");

                    b.Property<string>("AparatCardiovascular");

                    b.Property<string>("AparatDigestiv");

                    b.Property<string>("AparatRespirator");

                    b.Property<string>("AparatUroGeneral");

                    b.Property<string>("Constienta");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Diagnosis");

                    b.Property<string>("Facies");

                    b.Property<string>("Fanere");

                    b.Property<string>("FicatSplina");

                    b.Property<int>("Greutate");

                    b.Property<string>("IstoriculBolii");

                    b.Property<string>("MotiveleInternarii");

                    b.Property<string>("Mucoase");

                    b.Property<string>("Nutritie");

                    b.Property<Guid>("PatientId");

                    b.Property<string>("SistemEndocrin");

                    b.Property<string>("SistemGanglionar");

                    b.Property<string>("SistemMuscular");

                    b.Property<string>("SistemOsteoArticular");

                    b.Property<string>("StareaGenerala");

                    b.Property<int>("Talie");

                    b.Property<string>("Tegumente");

                    b.Property<string>("TesutConjunctiv");

                    b.HasKey("Id");

                    b.ToTable("Records");
                });
#pragma warning restore 612, 618
        }
    }
}
