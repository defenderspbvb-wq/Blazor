using System.ComponentModel.DataAnnotations.Schema;

namespace Academy2.Models
{
    [Table("RequiredDisciplines")]
    public class RequiredDiscipline
    {
        [Column(TypeName = "SMALLINT")]
        public int discipline { get; set; }
        public Discipline? Discipline { get; set; }

        [Column(TypeName = "SMALLINT")]
        public int required_discipline { get; set; }
        public Discipline? Required { get; set; }
    }
}

