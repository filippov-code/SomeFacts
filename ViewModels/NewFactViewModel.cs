using System.ComponentModel.DataAnnotations;

namespace SomeFacts.ViewModels
{
    public class NewFactViewModel
    {
        [Required]
        public string Text { get; set; }
    }
}
