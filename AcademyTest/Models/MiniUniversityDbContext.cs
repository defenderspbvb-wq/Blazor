using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace AcademyTest.Models;

// Наш контекст наследуется от системного класса DbContext библиотеки EF Core
public class MiniUniversityDbContext : DbContext
{
    // Пустой конструктор (нужен для внутренних механизмов EF Core)
    public MiniUniversityDbContext() { }

    // Конструктор, который будет принимать настройки подключения (строку подключения) из Program.cs
    public MiniUniversityDbContext(DbContextOptions<MiniUniversityDbContext> options)
        : base(options)
    {
    }

    // Объявляем виртуальные таблицы (DbSet). Через них мы будем делать запросы в коде
    public virtual DbSet<Course> Courses { get; set; } = null!;
    public virtual DbSet<Group> Groups { get; set; } = null!;
    public virtual DbSet<Student> Students { get; set; } = null!;
    public virtual DbSet<Enrollment> Enrollments { get; set; } = null!;

    // Метод, где мы настраиваем сложные правила, которые невозможно описать обычными классами
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 1. Настройка таблицы Enrollments (так как у неё составной ключ)
        modelBuilder.Entity<Enrollment>(entity =>
        {
            // Говорим EF Core, что первичный ключ состоит сразу из двух колонок (как в нашем SQL-скрипте PRIMARY KEY(StudentId, GroupId))
            entity.HasKey(e => new { e.StudentId, e.GroupId });

            // Явно связываем C#-свойство StudentId с внешним ключом таблицы Students
            entity.HasOne(d => d.Student)               // У зачисления есть один Студент
                .WithMany(p => p.Enrollments)           // У студента много зачислений
                .HasForeignKey(d => d.StudentId)         // Внешний ключ — StudentId
                .OnDelete(DeleteBehavior.Cascade);      // Включаем каскадное удаление (ON DELETE CASCADE)

            // Точно так же связываем с таблицей Групп
            entity.HasOne(d => d.Group)                 // У зачисления есть одна Группа
                .WithMany(p => p.Enrollments)           // У группы много зачислений студентов
                .HasForeignKey(d => d.GroupId)           // Внешний ключ — GroupId
                .OnDelete(DeleteBehavior.Cascade);
        });

        // 2. Настройка таблицы Groups
        modelBuilder.Entity<Group>(entity =>
        {
            // Настраиваем связь один-ко-многим между Группой и Курсом
            entity.HasOne(d => d.Course)                // У группы есть один Курс
                .WithMany(p => p.Groups)                // На курсе может быть много Групп
                .HasForeignKey(d => d.CourseId)         // Внешний ключ — CourseId
                .OnDelete(DeleteBehavior.ClientSetNull); // При удалении курса группы не удаляются каскадно
        });
    }
}
