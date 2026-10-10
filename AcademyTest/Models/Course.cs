using System.Collections.Generic;

namespace AcademyTest.Models;

// Этот класс представляет одну строку в таблице Courses
public class Course
{
    // EF Core автоматически поймет, что это первичный ключ (PrimaryKey), так как имя оканчивается на Id
    public int CourseId { get; set; }

    // Название курса
    public string CourseName { get; set; } = null!;

    // НАВИГАЦИОННОЕ СВОЙСТВО (Связь один-ко-многим):
    // У одного курса может быть список связанных учебных групп.
    // virtual позволяет EF Core подгружать этот список только тогда, когда мы к нему обратимся (Lazy Loading)
    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
}
