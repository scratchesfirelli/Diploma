using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using OrderManagementSystem.Models;

namespace Backend.Migrations
{
    [DbContext(typeof(OmsContext))]
    [Migration("20170417130838_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OrderManagementSystem.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Description");

                    b.Property<double>("Height");

                    b.Property<double>("Length");

                    b.Property<string>("Material");

                    b.Property<decimal>("Price");

                    b.Property<string>("Title");

                    b.Property<string>("Type");

                    b.Property<double>("Weight");

                    b.Property<double>("Width");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });
        }
    }
}
