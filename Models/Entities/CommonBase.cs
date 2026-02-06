using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduReg.Models.Entities
{
    public class CommonBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        public int ActiveStatus { get; set; }
 

    }
}
