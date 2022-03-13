using System.ComponentModel.DataAnnotations;
using WebScheduler.Domain.Interfaces;

namespace WebScheduler.Domain.Models
{
    public class AllowedFileType : IEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public string FileType { get; set; }
        public double FileSize { get; set; }
    }
}
