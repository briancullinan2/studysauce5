using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities
{
    [PrimaryKey(nameof(UserId), nameof(PackId))]
    public class UserPack : Entity<UserPack>
    {
        // Composite Key Properties
        public string? UserId { get; set; }
        public virtual User? User { get; set; }

        public int PackId { get; set; }
        public virtual Pack? Pack { get; set; }

        // Columns
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Priority { get; set; } = 0;

        public DateTime? RetryFrom { get; set; }
        public DateTime? RetryTo { get; set; }
        public DateTime? Downloaded { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;

        // EF Core doesn't support 'array' type natively like Doctrine. 
        // We store it as a JSON string or a serialized blob.
        public string? RetentionJson { get; set; }

        public bool Removed { get; set; } = false;

        // Business Logic ported from PHP
        public List<Response> GetResponses(out int correctCount)
        {
            correctCount = 0;
            var rids = new HashSet<int>();
            var responses = new List<Response>();

            foreach (var r in User?.Responses.Where(r => r.Card?.PackId == PackId) ?? [])
            {
                if (r.CardId != null && rids.Add((int)r.CardId))
                {
                    responses.Add(r);
                    if (r.IsCorrect) correctCount++;
                }
            }
            return responses;
        }
    }
}