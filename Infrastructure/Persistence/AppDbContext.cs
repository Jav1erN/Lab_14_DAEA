using System;
using System.Collections.Generic;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<response> responses { get; set; }

    public virtual DbSet<role> roles { get; set; }

    public virtual DbSet<ticket> tickets { get; set; }

    public virtual DbSet<user> users { get; set; }

    public virtual DbSet<user_role> user_roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<response>(entity =>
        {
            entity.HasKey(e => e.response_id);

            entity.HasIndex(e => e.responder_id, "IX_responses_responder_id");

            entity.HasIndex(e => e.ticket_id, "IX_responses_ticket_id");

            entity.Property(e => e.response_id).ValueGeneratedNever();
            entity.Property(e => e.created_at).HasDefaultValueSql("now()");

            entity.HasOne(d => d.responder).WithMany(p => p.responses)
                .HasForeignKey(d => d.responder_id)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.ticket).WithMany(p => p.responses).HasForeignKey(d => d.ticket_id);
        });

        modelBuilder.Entity<role>(entity =>
        {
            entity.HasKey(e => e.role_id);

            entity.HasIndex(e => e.role_name, "IX_roles_role_name").IsUnique();

            entity.Property(e => e.role_id).ValueGeneratedNever();
            entity.Property(e => e.role_name).HasMaxLength(50);
        });

        modelBuilder.Entity<ticket>(entity =>
        {
            entity.HasKey(e => e.ticket_id);

            entity.HasIndex(e => e.user_id, "IX_tickets_user_id");

            entity.Property(e => e.ticket_id).ValueGeneratedNever();
            entity.Property(e => e.created_at).HasDefaultValueSql("now()");
            entity.Property(e => e.status).HasMaxLength(20);
            entity.Property(e => e.title).HasMaxLength(255);

            entity.HasOne(d => d.user).WithMany(p => p.tickets)
                .HasForeignKey(d => d.user_id)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<user>(entity =>
        {
            entity.HasKey(e => e.user_id);

            entity.HasIndex(e => e.email, "IX_users_email").IsUnique();

            entity.HasIndex(e => e.username, "IX_users_username").IsUnique();

            entity.Property(e => e.user_id).ValueGeneratedNever();
            entity.Property(e => e.created_at).HasDefaultValueSql("now()");
            entity.Property(e => e.email).HasMaxLength(150);
            entity.Property(e => e.password_hash).HasMaxLength(255);
            entity.Property(e => e.username).HasMaxLength(100);
        });

        modelBuilder.Entity<user_role>(entity =>
        {
            entity.HasKey(e => new { e.user_id, e.role_id });

            entity.HasIndex(e => e.role_id, "IX_user_roles_role_id");

            entity.Property(e => e.assigned_at).HasDefaultValueSql("now()");

            entity.HasOne(d => d.role).WithMany(p => p.user_roles).HasForeignKey(d => d.role_id);

            entity.HasOne(d => d.user).WithMany(p => p.user_roles).HasForeignKey(d => d.user_id);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
