using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using Framework.Common.IOCOptions;

namespace UserMicro.Model.Models
{
    public partial class DbEntitys : DbContext
    {
        private readonly IOptionsMonitor<MySqlConnOptions> _optionsMonitor;
        private readonly string _connStr;

        public DbEntitys(IOptionsMonitor<MySqlConnOptions> optionsMonitor)
        {
            _optionsMonitor = optionsMonitor;
            _connStr = _optionsMonitor.CurrentValue.Url;
        }



        public DbEntitys(string connstr)
        {
            _connStr = connstr;
        }

        //public OrangeContext(DbContextOptions<OrangeContext> options)
        //	: base(options)
        //{
        //	//this.Database.l
        //}
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(_connStr, new MySqlServerVersion(new Version(5, 7, 32)))
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasComment("用户表");

                entity.HasIndex(e => e.Name)
                    .HasDatabaseName("username")
                    .IsUnique();

                entity.Property(e => e.id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("CreateTime")
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("varchar(32)")
                    .HasComment("密码，加密存储")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Mobile)
                    .HasColumnName("phone")
                    .HasColumnType("varchar(11)")
                    .HasComment("注册手机号")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasColumnName("salt")
                    .HasColumnType("varchar(32)")
                    .HasComment("密码加密的salt值")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
