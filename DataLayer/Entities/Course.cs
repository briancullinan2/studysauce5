using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities
{
    [Table("course")]
    public class Course : Entity<Course>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; protected set; }

        [Required]
        [MaxLength(200)]
        [Category("Content")]
        [Display(Name = "Course Title", Description = "The name of the educational course (e.g., Arizona Real Estate License)")]
        public string Title { get; set; } = string.Empty;

        [Category("Content")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Summary", Description = "A brief overview of what the course covers")]
        public string Summary { get; set; } = string.Empty;

        [Category("Content")]
        [Display(Name = "Category", Description = "Industry or subject area (e.g., Real Estate, Medical, Law)")]
        public string? Category { get; set; }

        [Category("Economics")]
        [Display(Name = "Bundled Price", Description = "Discounted price for purchasing the entire course instead of individual packs")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Price { get; set; }

        [Required]
        [Category("Status")]
        public bool IsPublished { get; set; } = false;

        [Category("System")]
        public DateTime Created { get; set; } = DateTime.UtcNow;

        // Relationship: A Course has many Packs
        [Category("Structure")]
        [Display(Name = "Study Packs", Description = "The individual modules/packs that make up this course")]
        public virtual ICollection<Pack> Packs { get; set; } = new HashSet<Pack>();

        // Optional: Track who created the course (similar to your Pack entity)
        [Category("Ownership")]
        public string? CreatorId { get; set; }

        [ForeignKey(nameof(CreatorId))]
        public virtual User? Creator { get; set; }
    }
}