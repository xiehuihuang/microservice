using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Framework.Common.IOCOptions;

namespace BrandMicro.Model.Models
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
        public virtual DbSet<Brand> Brand { get; set; }

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

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.ToTable("brand");

                entity.HasComment("品牌表，一个品牌下有多个商品（spu），一对多关系");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)")
                    .HasComment("品牌id");

                entity.Property(e => e.Image)
                    .HasColumnName("image")
                    .HasColumnType("varchar(128)")
                    .HasDefaultValueSql("''")
                    .HasComment("品牌图片地址")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Letter)
                    .HasColumnName("letter")
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("''")
                    .HasComment("品牌的首字母")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(32)")
                    .HasComment("品牌名称")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
