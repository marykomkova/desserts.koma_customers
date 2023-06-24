using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DessertsKoma_Customers.Models
{
    public partial class DessertsKomaContext : DbContext
    {
        public DessertsKomaContext()
        {
        }

        public DessertsKomaContext(DbContextOptions<DessertsKomaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Десерты> Десерты { get; set; }
        public virtual DbSet<ДесертыВзаказе> ДесертыВзаказе { get; set; }
        public virtual DbSet<ДесертыВкорзине> ДесертыВкорзине { get; set; }
        public virtual DbSet<ЕдиницыИзмерения> ЕдиницыИзмерения { get; set; }
        public virtual DbSet<Заказы> Заказы { get; set; }
        public virtual DbSet<ЗаказыПредставление> ЗаказыПредставление { get; set; }
        public virtual DbSet<Изображения> Изображения { get; set; }
        public virtual DbSet<Ингредиенты> Ингредиенты { get; set; }
        public virtual DbSet<ИнгредиентыВдесерте> ИнгредиентыВдесерте { get; set; }
        public virtual DbSet<ИнгредиентыПредставление> ИнгредиентыПредставление { get; set; }
        public virtual DbSet<Корзина> Корзина { get; set; }
        public virtual DbSet<Пользователи> Пользователи { get; set; }
        public virtual DbSet<ПользователиПредставление> ПользователиПредставление { get; set; }
        public virtual DbSet<Роли> Роли { get; set; }
        public virtual DbSet<Скидка> Скидка { get; set; }
        public virtual DbSet<Сотрудники> Сотрудники { get; set; }
        public virtual DbSet<Статусы> Статусы { get; set; }
        public virtual DbSet<ТипыДесертов> ТипыДесертов { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=MARUSYA\\SQLEXPRESS;Database=DessertsKoma;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Десерты>(entity =>
            {
                entity.HasKey(e => e.Номер)
                    .HasName("Prim_kod_dessert");

                entity.Property(e => e.Название)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.ИзображениеNavigation)
                    .WithMany(p => p.Десерты)
                    .HasForeignKey(d => d.Изображение)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dessert_image");

                entity.HasOne(d => d.ТипNavigation)
                    .WithMany(p => p.Десерты)
                    .HasForeignKey(d => d.Тип)
                    .HasConstraintName("FK_dessert_type");
            });

            modelBuilder.Entity<ДесертыВзаказе>(entity =>
            {
                entity.HasKey(e => e.Номер)
                    .HasName("Prim_kod_dessert_in_order");

                entity.ToTable("ДесертыВЗаказе");

                entity.HasOne(d => d.ДесертNavigation)
                    .WithMany(p => p.ДесертыВзаказе)
                    .HasForeignKey(d => d.Десерт)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_dessertInOrder_dessert");

                entity.HasOne(d => d.ЗаказNavigation)
                    .WithMany(p => p.ДесертыВзаказе)
                    .HasForeignKey(d => d.Заказ)
                    .HasConstraintName("FK_dessertInOrder_order");
            });

            modelBuilder.Entity<ДесертыВкорзине>(entity =>
            {
                entity.HasKey(e => e.Номер)
                    .HasName("PK__ДесертыВ__063C4BF6AF52B913");

                entity.ToTable("ДесертыВКорзине");

                entity.HasOne(d => d.ДесертNavigation)
                    .WithMany(p => p.ДесертыВкорзине)
                    .HasForeignKey(d => d.Десерт)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_des");

                entity.HasOne(d => d.КорзинаNavigation)
                    .WithMany(p => p.ДесертыВкорзине)
                    .HasForeignKey(d => d.Корзина)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bask");
            });

            modelBuilder.Entity<ЕдиницыИзмерения>(entity =>
            {
                entity.HasKey(e => e.Номер)
                    .HasName("Prim_kod_amount");

                entity.Property(e => e.Название).HasMaxLength(50);
            });

            modelBuilder.Entity<Заказы>(entity =>
            {
                entity.HasKey(e => e.Номер)
                    .HasName("Prim_kod_order");

                entity.HasIndex(e => new { e.Пользователь, e.ДатаЗаказа })
                    .HasName("orderss");

                entity.Property(e => e.ДатаВыдачи)
                    .HasColumnName("Дата выдачи")
                    .HasColumnType("date");

                entity.Property(e => e.ДатаЗаказа)
                    .HasColumnName("Дата заказа")
                    .HasColumnType("date");

                entity.HasOne(d => d.ПользовательNavigation)
                    .WithMany(p => p.ЗаказыПользовательNavigation)
                    .HasForeignKey(d => d.Пользователь)
                    .HasConstraintName("FK_order_user");

                entity.HasOne(d => d.СкидкаNavigation)
                    .WithMany(p => p.Заказы)
                    .HasForeignKey(d => d.Скидка)
                    .HasConstraintName("FK_order_sale");

                entity.HasOne(d => d.СотрудникNavigation)
                    .WithMany(p => p.ЗаказыСотрудникNavigation)
                    .HasForeignKey(d => d.Сотрудник)
                    .HasConstraintName("FK_order_employee");

                entity.HasOne(d => d.СтатусNavigation)
                    .WithMany(p => p.Заказы)
                    .HasForeignKey(d => d.Статус)
                    .HasConstraintName("FK_order_status");
            });

            modelBuilder.Entity<ЗаказыПредставление>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ЗаказыПредставление");

                entity.Property(e => e.ДатаЗаказа)
                    .HasColumnName("Дата заказа")
                    .HasColumnType("date");

                entity.Property(e => e.Логин).HasMaxLength(100);

                entity.Property(e => e.Название).HasMaxLength(100);

                entity.Property(e => e.НомерЗаказа).HasColumnName("Номер заказа");
            });

            modelBuilder.Entity<Изображения>(entity =>
            {
                entity.HasKey(e => e.Номер)
                    .HasName("Prim_kod_image");

                entity.Property(e => e.Данные).IsRequired();

                entity.Property(e => e.ИмяФайла)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Название)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Ингредиенты>(entity =>
            {
                entity.HasKey(e => e.Номер)
                    .HasName("Prim_kod_ingredient");

                entity.Property(e => e.Название).HasMaxLength(100);

                entity.HasOne(d => d.ЕдиницаИзмеренияNavigation)
                    .WithMany(p => p.Ингредиенты)
                    .HasForeignKey(d => d.ЕдиницаИзмерения)
                    .HasConstraintName("FK_ingredient_amount");
            });

            modelBuilder.Entity<ИнгредиентыВдесерте>(entity =>
            {
                entity.HasKey(e => e.Номер)
                    .HasName("Prim_kod_ingredient_in_dessert");

                entity.ToTable("ИнгредиентыВДесерте");

                entity.HasOne(d => d.ДесертNavigation)
                    .WithMany(p => p.ИнгредиентыВдесерте)
                    .HasForeignKey(d => d.Десерт)
                    .HasConstraintName("FK_ingredientInDessert_dessert");

                entity.HasOne(d => d.ИнгредиентNavigation)
                    .WithMany(p => p.ИнгредиентыВдесерте)
                    .HasForeignKey(d => d.Ингредиент)
                    .HasConstraintName("FK_ingredientInDessert_ingredient");
            });

            modelBuilder.Entity<ИнгредиентыПредставление>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ИнгредиентыПредставление");

                entity.Property(e => e.ЕдиницаИзмерения)
                    .HasColumnName("Единица измерения")
                    .HasMaxLength(50);

                entity.Property(e => e.Ингредиент).HasMaxLength(100);
            });

            modelBuilder.Entity<Корзина>(entity =>
            {
                entity.HasKey(e => e.Номер)
                    .HasName("PK__Корзина__063C4BF643F40220");

                entity.HasOne(d => d.ПользовательNavigation)
                    .WithMany(p => p.Корзина)
                    .HasForeignKey(d => d.Пользователь)
                    .HasConstraintName("FK_user_basket");

                entity.HasOne(d => d.СкидкаNavigation)
                    .WithMany(p => p.Корзина)
                    .HasForeignKey(d => d.Скидка)
                    .HasConstraintName("FK_basket_sale");
            });

            modelBuilder.Entity<Пользователи>(entity =>
            {
                entity.HasKey(e => e.Номер)
                    .HasName("Prim_kod_user");

                entity.Property(e => e.Имя)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Логин)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.НомерТелефона).HasMaxLength(19);

                entity.Property(e => e.Пароль)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.ИзображениеNavigation)
                    .WithMany(p => p.Пользователи)
                    .HasForeignKey(d => d.Изображение)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_user_image");

                entity.HasOne(d => d.РольNavigation)
                    .WithMany(p => p.Пользователи)
                    .HasForeignKey(d => d.Роль)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_user_role");
            });

            modelBuilder.Entity<ПользователиПредставление>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ПользователиПредставление");

                entity.Property(e => e.Имя).HasMaxLength(100);

                entity.Property(e => e.Логин).HasMaxLength(100);

                entity.Property(e => e.НомерИзображения).HasColumnName("Номер изображения");

                entity.Property(e => e.НомерТелефона)
                    .HasColumnName("Номер телефона")
                    .HasMaxLength(13);

                entity.Property(e => e.ОтображениеНаСайте).HasColumnName("Отображение на сайте");

                entity.Property(e => e.Пароль).HasMaxLength(100);

                entity.Property(e => e.ПодтверждениеАккаунта).HasColumnName("Подтверждение аккаунта");

                entity.Property(e => e.Роль).HasMaxLength(100);
            });

            modelBuilder.Entity<Роли>(entity =>
            {
                entity.HasKey(e => e.Номер)
                    .HasName("Prim_kod_role");

                entity.Property(e => e.Название).HasMaxLength(100);
            });

            modelBuilder.Entity<Скидка>(entity =>
            {
                entity.HasKey(e => e.Номер)
                    .HasName("Prim_kod_sale");

                entity.Property(e => e.Фраза)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Сотрудники>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Сотрудники");

                entity.Property(e => e.Логин)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Статусы>(entity =>
            {
                entity.HasKey(e => e.Номер)
                    .HasName("Prim_kod_sattus");

                entity.Property(e => e.Название).HasMaxLength(100);
            });

            modelBuilder.Entity<ТипыДесертов>(entity =>
            {
                entity.HasKey(e => e.Номер)
                    .HasName("Prim_kod_dessert_type");

                entity.Property(e => e.Название).HasMaxLength(50);

                entity.Property(e => e.Описание).HasMaxLength(100);

                entity.HasOne(d => d.ЕдиницаИзмеренияNavigation)
                    .WithMany(p => p.ТипыДесертов)
                    .HasForeignKey(d => d.ЕдиницаИзмерения)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_type_amount");

                entity.HasOne(d => d.ИзображениеNavigation)
                    .WithMany(p => p.ТипыДесертов)
                    .HasForeignKey(d => d.Изображение)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_type_image");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
