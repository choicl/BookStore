using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Data
{
    public partial class BookStoreDbContext : IdentityDbContext<APIUser>
    {
        public BookStoreDbContext()
        {
        }

        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<BookAuthor> BookAuthors { get; set; } = null!;
        public virtual DbSet<BooksGenre> BooksGenres { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<Publisher> Publishers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("BookStoreDbConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(e => e.Bio).HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);
            });

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER",
                    Id = "c87afbef-b901-482f-a669-9d241d85dc06"
                },
                new IdentityRole
                {
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                    Id = "108aeca5-61b7-4058-9564-54cde5ff5d78"
                }
            );

            var hasher = new PasswordHasher<APIUser>();
            modelBuilder.Entity<APIUser>().HasData(
                new APIUser
                { 
                    Id = "e19bcb87-a979-49f7-8c6f-60f5802b3bec",
                    Email = "admin@bookstore.com",
                    NormalizedEmail = "ADMIN@BOOKSTORE.COM",
                    UserName = "admin@bookstore.com",
                    NormalizedUserName = "ADMIN@BOOKSTORE.COM",
                    FirstName = "System",
                    LastName = "Admin",
                    PasswordHash = hasher.HashPassword(null, "P@ssword1")
                },
                new APIUser 
                { 
                    Id = "2af25b2c-aea7-41eb-ba87-97fd6dcfc24f",
                    Email = "user@bookstore.com",
                    NormalizedEmail = "USER@BOOKSTORE.COM",
                    UserName = "user@bookstore.com",
                    NormalizedUserName = "USER@BOOKSTORE.COM",
                    FirstName = "System",
                    LastName = "User",
                    PasswordHash = hasher.HashPassword(null, "P@ssword1")
                });
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                { 
                    RoleId = "c87afbef-b901-482f-a669-9d241d85dc06",
                    UserId = "2af25b2c-aea7-41eb-ba87-97fd6dcfc24f"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "108aeca5-61b7-4058-9564-54cde5ff5d78",
                    UserId = "e19bcb87-a979-49f7-8c6f-60f5802b3bec"
                });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasIndex(e => e.Isbn, "UQ__Books__447D36EA863B9DEC")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Image).HasMaxLength(50);

                entity.Property(e => e.Isbn)
                    .HasMaxLength(50)
                    .HasColumnName("ISBN");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PublishDate).HasColumnType("datetime");

                entity.Property(e => e.Rating).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Summary).HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK_Authors");

                entity.HasOne(d => d.Publisher)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.PublisherId)
                    .HasConstraintName("FK_Publishers");
            });

            modelBuilder.Entity<BookAuthor>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Book_Author");

                entity.HasOne(d => d.Author)
                    .WithMany()
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK_Book_Author");

                entity.HasOne(d => d.Book)
                    .WithMany()
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK_Book");
            });

            modelBuilder.Entity<BooksGenre>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Books_Genres");

                entity.HasOne(d => d.Book)
                    .WithMany()
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK_Books_Genres");

                entity.HasOne(d => d.Genre)
                    .WithMany()
                    .HasForeignKey(d => d.GenreId)
                    .HasConstraintName("FK_Genres");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("Genre");
            });

            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
