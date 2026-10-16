using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Academy2.Models
{
    [Table("Groups")] // Указываем точное имя таблицы в БД
    public class Group
    {
        [Key]
        [Column("group_id")]
        public int group_id { get; set; } // Здесь остается int, так как в БД тип int

        [Required]
        [StringLength(10, MinimumLength = 3)]
        [Column("group_name")]
        public string group_name { get; set; }

        [Column("direction", TypeName = "TINYINT")]
        [ForeignKey(nameof(Direction))]
        public byte direction { get; set; } // СОГЛАСОВАНО: теперь строго byte

        [Column("weekdays", TypeName = "TINYINT")]
        public byte? learning_days { get; set; } // СОГЛАСОВАНО: tinyint -> byte?

        [Column("start_time", TypeName = "TIME(0)")]
        public TimeOnly? start_time { get; set; }

        [Column("start_date", TypeName = "DATE")]
        public DateOnly? start_date { get; set; }

        // Навигационное свойство: связь «многие к одному» к исправленному классу Direction
        public Direction Direction { get; set; }
    }
}



