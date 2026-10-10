using System.Collections.Generic;

namespace AcademyTest.Models;

// Этот класс представляет строку в таблице Students
public class Student
{
    public int StudentId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    // НАВИГАЦИОННОЕ СВОЙСТВО:
    // Список всех зачислений и оценок конкретного студента
    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
