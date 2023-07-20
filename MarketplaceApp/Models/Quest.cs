using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MarketplaceApp.Models
{
    public class Quest
    {
        [Key]
        public int questId { get; set; }
        public int userId { get; set; }
        public DateTime? publishingDate { get; set; }
        public DateTime? endDate { get; set; }
        public bool isAvailable { get; set; }
        public string title { get; set; }
        public string? description { get; set; }
        public bool isSolved { get; set; }
        public List<Solution>? solutions { get; set; } = new(); //!
        public User? user { get; set; }
        public override string ToString() => JsonSerializer.Serialize<Quest>(this);
    }
}
