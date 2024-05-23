using Microsoft.EntityFrameworkCore;
namespace REST_API.Models;

public partial class AuthContext : DbContext
{
    public AuthContext()
    { }

    public AuthContext(DbContextOptions<AuthContext> options)
        : base(options)
    { }

    public virtual DbSet<Auth> Auth { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    => optionsBuilder.UseSqlite("DataSource=Database\\\\auth.db;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Auth>(entity =>
        {
            entity.ToTable("auth");
            entity.Property(e => e.Login).HasColumnName("login");
            entity.Property(e => e.Password).HasColumnName("passwoed_hash");
            entity.Property(e => e.Role).HasColumnName("role");
        });
        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}


