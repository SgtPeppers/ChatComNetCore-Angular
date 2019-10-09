﻿using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().Property(p => p.Name).IsRequired();
            modelBuilder.Entity<User>().Property(p => p.PasswordHash).IsRequired();
            modelBuilder.Entity<User>().Property(p => p.ImagePath).IsRequired();
            modelBuilder.Entity<User>().ToTable("tbl_users");

            // Colocar todas as string para nvarchar(100) na base de dados
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entity.GetProperties())
                {
                    if (property.ClrType == typeof(string))
                    {
                        property.MySql().ColumnType = "varchar(100)";

                        if (property.Name == "ImagePath")
                            property.MySql().DefaultValue = "none";
                    }
                }
            }

        }
    }
}
