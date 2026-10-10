using System.Collections.Generic;

namespace AcademyTest.Models;

// Этот класс представляет строку в таблице Groups
public class Group
{
    public int GroupId { get; set; }
    public string GroupName { get; set; } = null!;

    // Числовое поле внешнего ключа (Foreign Key), указывающее на курс
    public int CourseId { get; set; }

    // НАВИГАЦИОННЫЕ СВОЙСТВА:
    // 1. Позволяет через точку узнать данные курса, к которому привязана группа (например: group.Course.CourseName)
    public virtual Course Course { get; set; } = null!;

    // 2. Связь многие-ко-многим через промежуточную сущность: ведомость зачислений этой группы
    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
