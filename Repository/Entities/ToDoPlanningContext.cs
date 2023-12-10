using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ToDo_Planning.Entities;

public partial class ToDoPlanningContext : DbContext
{
    public ToDoPlanningContext()
    {
    }

    public ToDoPlanningContext(DbContextOptions<ToDoPlanningContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Developer> Developers { get; set; }

    public virtual DbSet<DeveloperProjectTaskCr> DeveloperProjectTaskCrs { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectTask> ProjectTasks { get; set; }

    private partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        // Burada model oluşturmayla ilgili ek ayarlamaları yapabilirsiniz.
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Developer>(entity =>
        {
            entity.HasKey(e => e.IdDeveloper);

            entity.ToTable("Developer");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<DeveloperProjectTaskCr>(entity =>
        {
            entity.HasKey(e => e.IdDeveloperTaskCr);

            entity.ToTable("DeveloperProjectTaskCr");

            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.StartTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.IdProject);

            entity.ToTable("Project");

            entity.Property(e => e.ProjectName).HasMaxLength(50);
        });

        modelBuilder.Entity<ProjectTask>(entity =>
        {
            entity.HasKey(e => e.IdTask).HasName("PK_Task");

            entity.ToTable("ProjectTask");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    private partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}