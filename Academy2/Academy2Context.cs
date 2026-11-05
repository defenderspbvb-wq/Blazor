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
    }
}
