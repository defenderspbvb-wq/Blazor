using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Academy2.Models
{
    public class TeacherDiscipline
    {
        [Column(TypeName = "SMALLINT")]
        public int teacher_id { get; set; }
        public Teacher Teacher { get; set; } = null!;
        [Column(TypeName = "SMALLINT")]
        public int discipline_id { get; set; }
        public Discipline Discipline { get; set; } = null!;
    }
}
