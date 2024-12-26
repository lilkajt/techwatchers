using Microsoft.EntityFrameworkCore;

public class TechwatchersContext : DbContext
{
    public TechwatchersContext(DbContextOptions<TechwatchersContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");
            entity.HasKey(e => e.id);
            entity.Property(e => e.username).HasMaxLength(50);
            entity.Property(e => e.email).HasMaxLength(40);
            entity.Property(e => e.password).HasMaxLength(50);
            entity.Property(e => e.statute).HasColumnType("tinyint(1)");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.ToTable("posts");
            entity.HasKey(e => e.id);
            entity.HasOne(d => d.user)
                .WithMany()
                .HasForeignKey(d => d.user_id)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(d => d.category)
                .WithMany()
                .HasForeignKey(d => d.category_id)
                .OnDelete(DeleteBehavior.Restrict);
            entity.Property(e => e.title).HasMaxLength(256);
            entity.Property(e => e.description).HasColumnType("text");
            entity.Property(e => e.dateCreation).HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP"); 
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("categories");
            entity.HasKey(e => e.id);
            entity.Property(e => e.name).HasMaxLength(50);
        });
    }
}
