using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Academy2.Models
{
    public class Student : Human
    {
        [Key]
        [Column("stud_id")]
        public int stud_id { get; set; }

        [Required]
        [Column("group")]
        public int group { get; set; }

        [ForeignKey(nameof(group))]
        public Group? Group { get; set; }
    }
}