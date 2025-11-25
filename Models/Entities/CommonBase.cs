using System.ComponentModel.DataAnnotations;

namespace EduReg.Models.Entities
{
    public class CommonBase
    {
        [Key]
        public int Id { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        public int ActiveStatus { get; set; }
 

    }
}
