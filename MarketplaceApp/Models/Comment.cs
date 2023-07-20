using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace MarketplaceApp.Models
{
    public class Comment
    {
        [Key]
        public int? commentId { get; set; }
        public int solutionId { get; set; }
        public int? userId { get; set; }
        public string? text { get; set; }
        public User? user { get; set; }
        public override string ToString() => JsonSerializer.Serialize<Comment>(this);
    }
}
