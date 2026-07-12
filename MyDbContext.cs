using Microsoft.EntityFrameworkCore;


public class MyDbContext : DbContext {


    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

    // Ejemplo: tabla de usuarios
    public DbSet<Us> Us { get; set; }

    public DbSet<Us> Us { get; set; }
}

