using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
