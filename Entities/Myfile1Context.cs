using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Entities
{
    public partial class Myfile1Context : DbContext
    {
        public virtual DbSet<TB_File_Info> TB_File_Info { get; set; }
        public virtual DbSet<TB_File_Meta> TB_File_Meta { get; set; }
        public virtual DbSet<TB_Folder_Info> TB_Folder_Info { get; set; }
        public virtual DbSet<TB_Folder_Meta> TB_Folder_Meta { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Data Source=172.16.31.106;Initial Catalog=MyFile1;Persist Security Info=True;User ID=;Password=");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TB_File_Info>(entity =>
            {
                entity.HasIndex(e => e.FolderID);

                entity.Property(e => e.ContentType).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Path).HasMaxLength(255);

                entity.Property(e => e.UserID).HasMaxLength(20);

                entity.HasOne(d => d.Folder)
                    .WithMany(p => p.TB_File_Info)
                    .HasForeignKey(d => d.FolderID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FileInfo_Folders");
            });

            modelBuilder.Entity<TB_File_Meta>(entity =>
            {
                entity.HasKey(e => e.FileID);
            });

            modelBuilder.Entity<TB_Folder_Info>(entity =>
            {
                entity.Property(e => e.UserID).HasMaxLength(20);
            });

            modelBuilder.Entity<TB_Folder_Meta>(entity =>
            {
                entity.HasKey(e => e.FileID);
            });
        }
    }
}
