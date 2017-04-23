using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using OrderManagementSystem.Models;

namespace Backend.Migrations
{
    [DbContext(typeof(OmsContext))]
    partial class OmsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Backend.Models.ProductMaterial", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("ProductMaterials");
                });

            modelBuilder.Entity("Backend.Models.ProductType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("ProductTypes");
                });

            modelBuilder.Entity("OrderManagementSystem.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Description");

                    b.Property<double?>("Height");

                    b.Property<double?>("Length");

                    b.Property<decimal>("Price");

                    b.Property<int>("ProductMaterialId");

                    b.Property<int>("ProductTypeId");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<double?>("Weight");

                    b.Property<double?>("Width");

                    b.HasKey("Id");

                    b.HasIndex("ProductMaterialId");

                    b.HasIndex("ProductTypeId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("OrderManagementSystem.Models.Product", b =>
                {
                    b.HasOne("Backend.Models.ProductMaterial", "ProductMaterial")
                        .WithOne("Product")
                        .HasForeignKey("OrderManagementSystem.Models.Product", "ProductMaterialId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Backend.Models.ProductType", "ProductType")
                        .WithOne("Product")
                        .HasForeignKey("OrderManagementSystem.Models.Product", "ProductTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
