using Microsoft.EntityFrameworkCore;
using VetQueue.Api.Models;

namespace VetQueue.Api.Data;

public class VetQueueDbContext : DbContext
{
    public VetQueueDbContext(
        DbContextOptions<VetQueueDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    public DbSet<Pet> Pets => Set<Pet>();

    public DbSet<Clinic> Clinics => Set<Clinic>();

    public DbSet<Appointment> Appointments => Set<Appointment>();

    public DbSet<QueueEntry> QueueEntries => Set<QueueEntry>();

    public DbSet<Paid> Paid => Set<Paid>();

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ==========================================
        // USER
        // ==========================================

        modelBuilder.Entity<User>()
            .HasKey(u => u.Id);

        // ==========================================
        // PET
        // ==========================================

        modelBuilder.Entity<Pet>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<Pet>()
            .HasOne(p => p.User)
            .WithMany(u => u.Pets)
            .HasForeignKey(p => p.UserId)
            .HasPrincipalKey(u => u.Id)
            .OnDelete(DeleteBehavior.Cascade);

        // ==========================================
        // CLINIC
        // ==========================================

        modelBuilder.Entity<Clinic>()
            .HasKey(c => c.Id);

        // ==========================================
        // PAID
        // ==========================================

        modelBuilder.Entity<Paid>()
            .HasKey(p => p.Id);

        // ==========================================
        // APPOINTMENT
        // ==========================================

        modelBuilder.Entity<Appointment>()
            .HasKey(a => a.Id);

        // Appointment → User

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.User)
            .WithMany(u => u.Appointments)
            .HasForeignKey(a => a.UserId)
            .HasPrincipalKey(u => u.Id)
            .OnDelete(DeleteBehavior.Restrict);

        // Appointment → Pet

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Pet)
            .WithMany(p => p.Appointments)
            .HasForeignKey(a => a.PetId)
            .HasPrincipalKey(p => p.Id)
            .OnDelete(DeleteBehavior.Restrict);

        // Appointment → Clinic

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Clinic)
            .WithMany(c => c.Appointments)
            .HasForeignKey(a => a.ClinicId)
            .HasPrincipalKey(c => c.Id)
            .OnDelete(DeleteBehavior.Restrict);

        // Appointment → Paid

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Pay)
            .WithMany(p => p.Appointments)
            .HasForeignKey(a => a.PaidId)
            .HasPrincipalKey(p => p.Id)
            .OnDelete(DeleteBehavior.Restrict);

        // ==========================================
        // QUEUE
        // ==========================================

        modelBuilder.Entity<QueueEntry>()
            .HasKey(q => q.Id);

        // Queue → User

        modelBuilder.Entity<QueueEntry>()
            .HasOne(q => q.User)
            .WithMany()
            .HasForeignKey(q => q.UserId)
            .HasPrincipalKey(u => u.Id)
            .OnDelete(DeleteBehavior.Restrict);

        // Queue → Pet

        modelBuilder.Entity<QueueEntry>()
            .HasOne(q => q.Pet)
            .WithMany()
            .HasForeignKey(q => q.PetId)
            .HasPrincipalKey(p => p.Id)
            .OnDelete(DeleteBehavior.Restrict);

        // Queue → Clinic

        modelBuilder.Entity<QueueEntry>()
            .HasOne(q => q.Clinic)
            .WithMany()
            .HasForeignKey(q => q.ClinicId)
            .HasPrincipalKey(c => c.Id)
            .OnDelete(DeleteBehavior.Restrict);
    }
}