using System;
using System.Collections.Generic;
using DocToData.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DocToDomain.Infrastructure.Data;

public partial class DocToDataDBContext : DbContext
{
    public DocToDataDBContext()
    {
    }

    public DocToDataDBContext(DbContextOptions<DocToDataDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<ExpenseCategory> ExpenseCategories { get; set; }

    public virtual DbSet<ExpenseSubCategory> ExpenseSubCategories { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Merchant> Merchants { get; set; }

    public virtual DbSet<PaymentType> PaymentTypes { get; set; }

    public virtual DbSet<Receipt> Receipts { get; set; }

    public virtual DbSet<ReceiptCategory> ReceiptCategories { get; set; }

    public virtual DbSet<ReceiptImage> ReceiptImages { get; set; }

    public virtual DbSet<ReceiptItem> ReceiptItems { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<StockTransaction> StockTransactions { get; set; }

    public virtual DbSet<TransactionType> TransactionTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:dbs-solutioneersforge.database.windows.net,1433;Initial Catalog=db-doc2data;Persist Security Info=False;User ID=serveradmin;Password=9U[X!mDG2_n89Ep:;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>(entity =>
        {
            entity.ToTable("Countries", "Common");

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(150);
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasKey(e => e.CurrenctId);

            entity.ToTable("Currencies", "Common");

            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Symbol).HasMaxLength(50);
        });

        modelBuilder.Entity<ExpenseCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId);

            entity.ToTable("ExpenseCategories", "Common");

            entity.Property(e => e.CategoryName).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<ExpenseSubCategory>(entity =>
        {
            entity.HasKey(e => e.SubCategoryId);

            entity.ToTable("ExpenseSubCategories", "Common");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.SubCategoryName).HasMaxLength(100);
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.ToTable("Items", "Inventory");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.ItemName).HasMaxLength(255);
        });

        modelBuilder.Entity<Merchant>(entity =>
        {
            entity.ToTable("Merchants", "Common");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.CompanyRegNo).HasMaxLength(250);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.TaxCompanyRegNo).HasMaxLength(250);
            entity.Property(e => e.Website).HasMaxLength(250);
        });

        modelBuilder.Entity<PaymentType>(entity =>
        {
            entity.ToTable("PaymentTypes", "Common");

            entity.Property(e => e.PaymentType1)
                .HasMaxLength(50)
                .HasColumnName("PaymentType");
        });

        modelBuilder.Entity<Receipt>(entity =>
        {
            entity.ToTable("Receipts", "Receipt");

            entity.Property(e => e.ReceiptId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.OtherCharge).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ReceiptDate).HasColumnType("datetime");
            entity.Property(e => e.ReceiptNumber).HasMaxLength(50);
            entity.Property(e => e.ServiceCharge).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SubTotal).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TaxAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.Country).WithMany(p => p.Receipts)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Receipts_Countries");

            entity.HasOne(d => d.Currency).WithMany(p => p.Receipts)
                .HasForeignKey(d => d.CurrencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Receipts_Currencies");

            entity.HasOne(d => d.PaymentType).WithMany(p => p.Receipts)
                .HasForeignKey(d => d.PaymentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Receipts_PaymentTypes");

            entity.HasOne(d => d.Status).WithMany(p => p.Receipts)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Receipts_Status");

            entity.HasOne(d => d.SubCategory).WithMany(p => p.Receipts)
                .HasForeignKey(d => d.SubCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Receipts_ExpenseSubCategories");

            entity.HasOne(d => d.User).WithMany(p => p.Receipts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Receipts_Users");
        });

        modelBuilder.Entity<ReceiptCategory>(entity =>
        {
            entity.ToTable("ReceiptCategories", "Receipt");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ReceiptId).HasColumnName("ReceiptID");

            entity.HasOne(d => d.Receipt).WithMany(p => p.ReceiptCategories)
                .HasForeignKey(d => d.ReceiptId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReceiptCategories_Receipts");
        });

        modelBuilder.Entity<ReceiptImage>(entity =>
        {
            entity.ToTable("ReceiptImages", "Receipt");

            entity.Property(e => e.ReceiptImageId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ReceiptImageID");
            entity.Property(e => e.FileName).HasMaxLength(150);
            entity.Property(e => e.UploadedDateTime).HasColumnType("datetime");

            entity.HasOne(d => d.Receipt).WithMany(p => p.ReceiptImages)
                .HasForeignKey(d => d.ReceiptId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReceiptImages_Receipts");
        });

        modelBuilder.Entity<ReceiptItem>(entity =>
        {
            entity.ToTable("ReceiptItems", "Receipt");

            entity.Property(e => e.ReceiptItemId)
                .ValueGeneratedNever()
                .HasColumnName("ReceiptItemID");
            entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SubTotal).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Item).WithMany(p => p.ReceiptItems)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReceiptItems_Items");

            entity.HasOne(d => d.Receipt).WithMany(p => p.ReceiptItems)
                .HasForeignKey(d => d.ReceiptId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReceiptItems_Receipts");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.ToTable("Status", "Common");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<StockTransaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId);

            entity.ToTable("StockTransactions", "Inventory");

            entity.Property(e => e.Notes).HasMaxLength(255);
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");

            entity.HasOne(d => d.Item).WithMany(p => p.StockTransactions)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StockTransactions_Items");

            entity.HasOne(d => d.TransactionType).WithMany(p => p.StockTransactions)
                .HasForeignKey(d => d.TransactionTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StockTransactions_TransactionTypes");
        });

        modelBuilder.Entity<TransactionType>(entity =>
        {
            entity.ToTable("TransactionTypes", "Inventory");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_Users_1");

            entity.ToTable("Users", "Authentication");

            entity.Property(e => e.UserId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
