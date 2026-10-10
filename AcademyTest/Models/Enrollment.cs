namespace AcademyTest.Models;

// Этот класс представляет строку промежуточной таблицы, которая связывает Студента и Группу
public class Enrollment
{
    // Составные внешние ключи
    public int StudentId { get; set; }
    public int GroupId { get; set; }

    // Оценка. Знак "?" означает Nullable, так как в базе данных это поле может быть пустым (NULL)
    public int? Grade { get; set; }

    // НАВИГАЦИОННЫЕ СВОЙСТВА:
    // Находясь внутри записи журнала, мы можем мгновенно «прыгнуть» к данным студента или группы
    public virtual Student Student { get; set; } = null!;
    public virtual Group Group { get; set; } = null!;
}
