using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Academy2.Models
{
    [Table("Directions")] // Указываем точное имя таблицы в БД
    public class Direction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("direction_id", TypeName = "TINYINT")]
        public byte direction_id { get; set; } // ИСПРАВЛЕНО: int заменен на byte для соответствия TINYINT

        [Required]
        [StringLength(100)] // Ограничение длины строки, настройте под вашу БД
        [Column("direction_name")]
        public string? direction_name { get; set; }

        // Навигационное свойство: у одного направления может быть много групп (один ко многим)
        public ICollection<Group> Groups { get; set; } = new List<Group>();
    }
}

