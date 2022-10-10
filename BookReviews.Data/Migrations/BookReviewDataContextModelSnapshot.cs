﻿// <auto-generated />
using System;
using BookReviews.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BookReviews.Data.Migrations
{
    [DbContext(typeof(BookReviewDataContext))]
    partial class BookReviewDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseSerialColumns(modelBuilder);

            modelBuilder.Entity("BookReviews.Data.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<int?>("DeletedByUserId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Authors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "Stephen",
                            IsDeleted = false,
                            LastName = "King"
                        },
                        new
                        {
                            Id = 2,
                            FirstName = "Frank",
                            IsDeleted = false,
                            LastName = "Herbert"
                        },
                        new
                        {
                            Id = 3,
                            FirstName = "Brian",
                            IsDeleted = false,
                            LastName = "Herbert"
                        });
                });

            modelBuilder.Entity("BookReviews.Data.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("integer");

                    b.Property<int?>("DeletedByUserId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("NumberOfPages")
                        .HasColumnType("integer");

                    b.Property<double?>("StarRating")
                        .HasColumnType("double precision");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AuthorId = 1,
                            IsDeleted = false,
                            NumberOfPages = 400,
                            StarRating = 2.0,
                            Title = "Book 1"
                        },
                        new
                        {
                            Id = 2,
                            AuthorId = 1,
                            IsDeleted = false,
                            NumberOfPages = 400,
                            StarRating = 2.0,
                            Title = "Book 2"
                        },
                        new
                        {
                            Id = 3,
                            AuthorId = 1,
                            IsDeleted = false,
                            NumberOfPages = 400,
                            StarRating = 2.0,
                            Title = "Book 3"
                        });
                });

            modelBuilder.Entity("BookReviews.Data.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<int>("BookId")
                        .HasColumnType("integer");

                    b.Property<int?>("DeletedByUserId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("StarRatingId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Reviews");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BookId = 1,
                            IsDeleted = false,
                            StarRatingId = 4,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            BookId = 2,
                            IsDeleted = false,
                            StarRatingId = 3,
                            UserId = 1
                        },
                        new
                        {
                            Id = 3,
                            BookId = 3,
                            IsDeleted = false,
                            StarRatingId = 5,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("BookReviews.Data.StarRating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("StarRatings");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Text = "*",
                            Value = "Poor"
                        },
                        new
                        {
                            Id = 2,
                            Text = "**",
                            Value = "Moderate"
                        },
                        new
                        {
                            Id = 3,
                            Text = "***",
                            Value = "Good"
                        },
                        new
                        {
                            Id = 4,
                            Text = "****",
                            Value = "Very Good"
                        },
                        new
                        {
                            Id = 5,
                            Text = "*****",
                            Value = "Excellent"
                        });
                });

            modelBuilder.Entity("BookReviews.Data.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<int?>("DeletedByUserId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "admin@admin.com",
                            FirstName = "Admin",
                            IsDeleted = false,
                            LastName = "User",
                            Password = "zsczfP6mHLQ3FQpFtTRM2OrSKCwWhNesmM8eWMb/vvA="
                        },
                        new
                        {
                            Id = 2,
                            Email = "jefe101073@gmail.com",
                            FirstName = "Jeff",
                            IsDeleted = false,
                            LastName = "McCann",
                            Password = "zsczfP6mHLQ3FQpFtTRM2OrSKCwWhNesmM8eWMb/vvA="
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
