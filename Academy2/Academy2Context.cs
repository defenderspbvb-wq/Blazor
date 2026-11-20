using Microsoft.EntityFrameworkCore;
using Academy2.Models;

public class Academy2Context(DbContextOptions<Academy2Context> options) : DbContext(options)
{
    public DbSet<Academy2.Models.Direction> Directions { get; set; } = default!;
    public DbSet<Academy2.Models.Group> Groups { get; set; } = default!;
    public DbSet<Academy2.Models.Student> Students { get; set; } = default!;
    public DbSet<Academy2.Models.Discipline> Disciplines { get; set; } = default!;
    public DbSet<Academy2.Models.Teacher> Teachers { get; set; } = default!;
    public DbSet<Academy2.Models.TeacherDiscipline> TeacherDisciplines { get; set; } = default!;
    public DbSet<Academy2.Models.DependentDiscipline> DependentDisciplines { get; set; } = default!;
    public DbSet<Academy2.Models.RequiredDiscipline> RequiredDisciplines { get; set; } = default!;

    // Составной ключ и связи
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Составной первичный ключ (комбинация ID учителя и ID дисциплины)
        modelBuilder.Entity<TeacherDiscipline>()
            .HasKey(td => new { td.teacher_id, td.discipline_id });

        // Связь "один ко многим" между Teacher и TeacherDiscipline
        modelBuilder.Entity<TeacherDiscipline>()
            .HasOne(td => td.Teacher)
            .WithMany(t => t.TeacherDisciplines)
            .HasForeignKey(td => td.teacher_id);

        // Связь "один ко многим" между Discipline и TeacherDiscipline
        modelBuilder.Entity<TeacherDiscipline>()
            .HasOne(td => td.Discipline)
            .WithMany(d => d.TeacherDisciplines)
            .HasForeignKey(td => td.discipline_id);

        // Зависимые дисциплины
        modelBuilder.Entity<DependentDiscipline>()
            .HasKey(dd => new { dd.discipline, dd.dependent_discipline });

        modelBuilder.Entity<DependentDiscipline>()
            .HasOne(dd => dd.Discipline)
            .WithMany(d => d.DependentDisciplines)
            .HasForeignKey(dd => dd.discipline)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<DependentDiscipline>()
            .HasOne(dd => dd.Dependent)
            .WithMany()
            .HasForeignKey(dd => dd.dependent_discipline)
            .OnDelete(DeleteBehavior.Restrict);

        // Требуемые дисциплины
        modelBuilder.Entity<RequiredDiscipline>()
            .HasKey(rd => new { rd.discipline, rd.required_discipline });

        modelBuilder.Entity<RequiredDiscipline>()
            .HasOne(rd => rd.Discipline)
            .WithMany(d => d.RequiredDisciplines)
            .HasForeignKey(rd => rd.discipline)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RequiredDiscipline>()
            .HasOne(rd => rd.Required)
            .WithMany()
            .HasForeignKey(rd => rd.required_discipline)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
