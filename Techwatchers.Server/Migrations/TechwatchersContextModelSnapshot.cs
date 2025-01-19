﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Techwatchers.Server.Migrations
{
    [DbContext(typeof(TechwatchersContext))]
    partial class TechwatchersContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Category", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("id");

                    b.ToTable("categories", (string)null);
                });

            modelBuilder.Entity("Post", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("category_id")
                        .HasColumnType("int");

                    b.Property<DateTime>("dateCreation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("likes")
                        .HasColumnType("int");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("category_id");

                    b.HasIndex("user_id");

                    b.ToTable("posts", (string)null);
                });

            modelBuilder.Entity("PostComment", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("comment_content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("dateCreation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int>("post_id")
                        .HasColumnType("int");

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("post_id");

                    b.HasIndex("user_id");

                    b.ToTable("post_comments", (string)null);
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("statute")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("id");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Post", b =>
                {
                    b.HasOne("Category", "category")
                        .WithMany()
                        .HasForeignKey("category_id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("User", "user")
                        .WithMany()
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("category");

                    b.Navigation("user");
                });

            modelBuilder.Entity("PostComment", b =>
                {
                    b.HasOne("Post", "post")
                        .WithMany()
                        .HasForeignKey("post_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("User", "user")
                        .WithMany()
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("post");

                    b.Navigation("user");
                });
#pragma warning restore 612, 618
        }
    }
}
