using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEntity
{
    class AppContext : DbContext
    {
        public DbSet<Worker> Workers { get; set; } 
        public DbSet<Boss> Bosses { get; set; }
        public DbSet<Description> Descriptions { get; set; }

       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EntityDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent API
            base.OnModelCreating(modelBuilder);
            // Добавляем сущность
            modelBuilder.Entity<Description>();
            // исключение сущности
            //modelBuilder.Ignore<Company>();

            modelBuilder.Entity<Worker>(WorkerConfigure);

            // установка ключа
            modelBuilder.Entity<Description>().HasKey(d => d.DescriptionID);        

            // индексирование 
            modelBuilder.Entity<Boss>().HasIndex(b => b.Name)
                .HasDatabaseName("BossNameIndex")
                .IsUnique();
        }

        public void WorkerConfigure(EntityTypeBuilder<Worker> builder)
        {
            // переименование столбика таблицы
            builder.Property(w => w.Id).HasColumnName("worker_id");

            // отключение автогенерации
            //builder.Property(w => w.Id).ValueGeneratedNever();

            // значение по умолчанию
            builder.Property(w => w.Age).HasDefaultValue(18);

            // составное свойство
            builder.Property(w => w.FullName)
                .HasComputedColumnSql("[FirstName] + '' + [Surname]");

            // альтернативный ключ
            builder.HasAlternateKey(w => w.Surname);
        }
    }
}
