using System.ComponentModel.DataAnnotations.Schema;

namespace Academy2.Models
{
    [Table("DependentDisciplines")]
    public class DependentDiscipline
    {
        [Column(TypeName = "SMALLINT")]
        public int discipline { get; set; }
        public Discipline? Discipline { get; set; }

        [Column(TypeName = "SMALLINT")]
        public int dependent_discipline { get; set; }
        public Discipline? Dependent { get; set; }
    }
}
