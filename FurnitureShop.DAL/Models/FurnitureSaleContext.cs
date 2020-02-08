using Microsoft.EntityFrameworkCore;

namespace FurnitureShopApp.DAL.Models
{
    public partial class FurnitureSaleContext : DbContext
    {
        public FurnitureSaleContext()
        {
        }

        public FurnitureSaleContext(DbContextOptions<FurnitureSaleContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CheckDetails> CheckDetails { get; set; }
        public virtual DbSet<CompanyDeveloper> CompanyDeveloper { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Delivery> Delivery { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmployeePosition> EmployeePosition { get; set; }
        public virtual DbSet<EmployeeUser> EmployeeUser { get; set; }
        public virtual DbSet<Furniture> Furniture { get; set; }
        public virtual DbSet<FurnitureInStorage> FurnitureInStorage { get; set; }
        public virtual DbSet<FurnitureSale> FurnitureSale { get; set; }
        public virtual DbSet<FurnitureShop> FurnitureShop { get; set; }
        public virtual DbSet<Storage> Storage { get; set; }
        public virtual DbSet<Subtype> Subtype { get; set; }
        public virtual DbSet<Type> Type { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-OM7JO4T;Database=FurnitureSale;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<CheckDetails>(entity =>
            {
                entity.HasOne(d => d.Check)
                    .WithMany(p => p.CheckDetails)
                    .HasForeignKey(d => d.CheckId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CheckDetails_FurnitureSale");

                entity.HasOne(d => d.Furniture)
                    .WithMany(p => p.CheckDetails)
                    .HasForeignKey(d => d.FurnitureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CheckDetails_FurnitureInStorage");
            });

            modelBuilder.Entity<CompanyDeveloper>(entity =>
            {
                entity.HasKey(e => e.CompanyId)
                    .HasName("XPKCompanyDeveloper");
            });

            modelBuilder.Entity<Delivery>(entity =>
            {
                entity.HasOne(d => d.Check)
                    .WithMany(p => p.Delivery)
                    .HasForeignKey(d => d.CheckId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Delivery_FurnitureSale");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasOne(d => d.Position)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.PositionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_EmployeePosition");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.ShopId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_FurnitureShop");
            });

            modelBuilder.Entity<EmployeePosition>(entity =>
            {
                entity.HasKey(e => e.PositionId)
                    .HasName("XPKEmployeePosition");
            });

            modelBuilder.Entity<EmployeeUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_User");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeUser)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Employee");
            });

            modelBuilder.Entity<Furniture>(entity =>
            {
                entity.HasKey(e => e.CatalogId)
                    .HasName("XPKFurnitureCatalog");

                entity.HasIndex(e => e.Color)
                    .HasName("XIE1FurnitureCatalog");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Furniture)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FurnitureCatalog_CompanyDeveloper");

                entity.HasOne(d => d.Subtype)
                    .WithMany(p => p.Furniture)
                    .HasForeignKey(d => d.SubtypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FurnitureCatalog_Subtype");
            });

            modelBuilder.Entity<FurnitureInStorage>(entity =>
            {
                entity.HasKey(e => e.FurnitureId)
                    .HasName("XPKFurnitureInStorage");

                entity.HasOne(d => d.Catalog)
                    .WithMany(p => p.FurnitureInStorage)
                    .HasForeignKey(d => d.CatalogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FurnitureInStorage_FurnitureCatalog");

                entity.HasOne(d => d.Storage)
                    .WithMany(p => p.FurnitureInStorage)
                    .HasForeignKey(d => d.StorageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FurnitureInStorage_Storage");
            });

            modelBuilder.Entity<FurnitureSale>(entity =>
            {
                entity.HasKey(e => e.CheckId)
                    .HasName("XPKFurnitureSale");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.FurnitureSale)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_FurnitureSale_Customer");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.FurnitureSale)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FurnitureSale_Employee");
            });

            modelBuilder.Entity<FurnitureShop>(entity =>
            {
                entity.HasKey(e => e.ShopId)
                    .HasName("XPKFurnitureShop");
            });

            modelBuilder.Entity<Storage>(entity =>
            {
                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.Storage)
                    .HasForeignKey(d => d.ShopId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Storage_FurnitureShop");
            });

            modelBuilder.Entity<Subtype>(entity =>
            {
                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Subtype)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Subtype_Type");
            });
        }
    }
}
