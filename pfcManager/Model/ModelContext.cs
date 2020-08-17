using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;

namespace pfcManager.Model
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Eating> Eating { get; set; }
        public virtual DbSet<Food> Food { get; set; }
        public virtual DbSet<Sports> Sports { get; set; }
        public virtual DbSet<Typesport> Typesport { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Weight> Weight { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectingString = Settings1.Default.ConnectionString;
                optionsBuilder.UseNpgsql(connectingString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Eating>(entity =>
            {
                entity.ToTable("eating");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Datatime).HasColumnName("datatime");

                entity.Property(e => e.Idfood).HasColumnName("idfood");

                entity.Property(e => e.Idusers).HasColumnName("idusers");

                entity.Property(e => e.Weight).HasColumnName("weight");

                entity.HasOne(d => d.IdfoodNavigation)
                    .WithMany(p => p.Eating)
                    .HasForeignKey(d => d.Idfood)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("eating_fk2");

                entity.HasOne(d => d.IdusersNavigation)
                    .WithMany(p => p.Eating)
                    .HasForeignKey(d => d.Idusers)
                    .HasConstraintName("eating_fk");
            });

            modelBuilder.Entity<Food>(entity =>
            {
                entity.ToTable("food");

                entity.HasIndex(e => e.Name)
                    .HasName("food_un")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Carbohydrates).HasColumnName("carbohydrates");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(400);

                entity.Property(e => e.Fats).HasColumnName("fats");

                entity.Property(e => e.Kkal).HasColumnName("kkal");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(300);

                entity.Property(e => e.Protein).HasColumnName("protein");
            });

            modelBuilder.Entity<Sports>(entity =>
            {
                entity.ToTable("sports");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Att1).HasColumnName("att1");

                entity.Property(e => e.Att2).HasColumnName("att2");

                entity.Property(e => e.Att3).HasColumnName("att3");

                entity.Property(e => e.Att4).HasColumnName("att4");

                entity.Property(e => e.Idsports).HasColumnName("idsports");

                entity.Property(e => e.Idusers).HasColumnName("idusers");

                entity.HasOne(d => d.IdsportsNavigation)
                    .WithMany(p => p.Sports)
                    .HasForeignKey(d => d.Idsports)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("sports_fk2");

                entity.HasOne(d => d.IdusersNavigation)
                    .WithMany(p => p.Sports)
                    .HasForeignKey(d => d.Idusers)
                    .HasConstraintName("sports_fk");
            });

            modelBuilder.Entity<Typesport>(entity =>
            {
                entity.ToTable("typesport");

                entity.HasIndex(e => e.Name)
                    .HasName("typesport_un")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Login)
                    .HasName("users_un")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasColumnName("firstname")
                    .HasMaxLength(150);

                entity.Property(e => e.Hash)
                    .IsRequired()
                    .HasColumnName("hash")
                    .HasMaxLength(40);

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasColumnName("lastname")
                    .HasMaxLength(150);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnName("login")
                    .HasMaxLength(150);

                entity.Property(e => e.Midname)
                    .HasColumnName("midname")
                    .HasMaxLength(150);

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasColumnName("salt")
                    .HasMaxLength(40);

                entity.Property(e => e.Sex).HasColumnName("sex");
            });

            modelBuilder.Entity<Weight>(entity =>
            {
                entity.ToTable("weight");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Datatime).HasColumnName("datatime");

                entity.Property(e => e.Idusers).HasColumnName("idusers");

                entity.Property(e => e.Value).HasColumnName("value");

                entity.HasOne(d => d.IdusersNavigation)
                    .WithMany(p => p.Weight)
                    .HasForeignKey(d => d.Idusers)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("weight_fk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
