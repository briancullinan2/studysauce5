using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities
{
    [Table("file")]
    public class File : Entity<File>
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("filename")]
        [StringLength(256)]
        public string Filename { get; set; } = string.Empty;

        [Column("upload_id")]
        [StringLength(256)]
        public string UploadId { get; set; } = string.Empty;

        [Column("url")]
        [StringLength(256)]
        public string? Url { get; set; }

        // Doctrine 'array' type is best handled as a JSON string in EF Core
        [Column("parts")]
        public string? PartsJson { get; set; } = "[]";

        [Column("created")]
        public DateTime Created { get; set; } = DateTime.UtcNow;

        // Relationship: Many Files to One User
        [Column("user_id")]
        public string? UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

        // Relationship: One File to One Response (Mapped by 'file' in Response entity)
        //public virtual Entities.Response? Response { get; set; }

        // Helper property to mimic PHP basename logic
        [NotMapped]
        public string ComputedFilename =>
            string.IsNullOrEmpty(Filename) && !string.IsNullOrEmpty(Url)
                ? System.IO.Path.GetFileName(Url)
                : Filename;

        public File()
        {
            Created = DateTime.UtcNow;
        }
    }
}