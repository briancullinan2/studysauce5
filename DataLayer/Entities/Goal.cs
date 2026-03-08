using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace DataLayer.Entities
{
    [Table("goal")]
    // Composite Unique Constraint: One type of goal per user (e.g., only one 'GPA' goal)
    [Index(nameof(UserId), nameof(Type), IsUnique = true, Name = "type_idx")]
    public class Goal : Entity<Goal>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; } = null!;

        [Required]
        [MaxLength(10)]
        [Column("type")]
        public string Type { get; set; } = string.Empty;

        [Required]
        [MaxLength(256)]
        [Column("goal")] // Keep DB column name as 'goal'
        public string Description { get; set; } = string.Empty;

        [Required]
        [Column("reward")]
        public string Reward { get; set; } = string.Empty;

        [Required]
        [Column("created")]
        public DateTime Created { get; set; } = DateTime.UtcNow;

        // Navigation property for Claims (One-to-Many)
        public virtual ICollection<Claim> Claims { get; set; } = new HashSet<Claim>();

        public Goal()
        {
            Created = DateTime.UtcNow;
        }
    }
}