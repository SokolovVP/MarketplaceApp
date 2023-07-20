using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace MarketplaceApp.Models
{
    public class Solution
    {
        [Key]
        public int solutionId { get; set; }
        public int userId { get; set; }
        [ForeignKey("questId")]
        public int questId { get; set; }
        public string? description { get; set; }
        public string? sourceLink { get; set; }
        public int? rating { get; set; }
        public bool isSelected { get; set; }
        public List<Comment>? comments { get; set; } = new();
        public override string ToString() => JsonSerializer.Serialize<Solution>(this);
    }
}
