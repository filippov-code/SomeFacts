using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SomeFacts.Domains
{
    public class Fact
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }
    }
}
