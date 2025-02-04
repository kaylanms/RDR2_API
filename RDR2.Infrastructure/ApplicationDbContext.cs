using Microsoft.EntityFrameworkCore;
using RDR2.Domain.Entities;

namespace RDR2.Infrastructure;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Character> Characters { get; set; }
    public DbSet<Gun> Guns { get; set; }
    public DbSet<Gang> Gangs { get; set; }
    public DbSet<CharacterGun> CharacterGuns { get; set; }
    public DbSet<Mission> Missions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Character>(entity =>
        {
            entity.Property(c => c.FirstName).HasMaxLength(100);
            entity.Property(c => c.LastName).HasMaxLength(100);

            // Relacionamento de Character com Gang
            entity.HasOne(c => c.Gang)
            .WithMany(g => g.Characters)
            .HasForeignKey(g => g.GangId)
            .OnDelete(DeleteBehavior.SetNull); // Se a Gang for deletada, os personagens ficam sem gangue

            entity.ToTable("Characters");
        });

        modelBuilder.Entity<Gun>(entity =>
        {
            entity.Property(g => g.Name).HasMaxLength(100);

            entity.ToTable("Guns");
        });

        // Configuração da relação CharacterGun (muitos-para-muitos)
        modelBuilder.Entity<CharacterGun>()
            .HasKey(cg => new { cg.CharacterId, cg.GunId }); // Chave composta

        modelBuilder.Entity<CharacterGun>()
            .HasOne(cg => cg.Character)
            .WithMany(c => c.CharacterGuns)
            .HasForeignKey(cg => cg.CharacterId);

        modelBuilder.Entity<CharacterGun>()
            .HasOne(cg => cg.Gun)
            .WithMany(g => g.CharacterGuns)
            .HasForeignKey(cg => cg.GunId);

        modelBuilder.Entity<Gang>(entity =>
        {
            entity.HasOne(g => g.Leader)
                .WithMany()  // O líder não precisa ter uma lista de Gangs
                .HasForeignKey(g => g.LeaderId)
                .OnDelete(DeleteBehavior.Restrict); // Impede exclusão em cascata

            entity.ToTable("Gangs");
        });

        modelBuilder.Entity<Mission>(entity =>
        {
            entity.Property(m => m.Name).HasMaxLength(100);
            entity.Property(m => m.Overview).HasMaxLength(500);

            entity.ToTable("Missions");
        });
    }
}