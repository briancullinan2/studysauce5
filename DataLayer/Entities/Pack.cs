using DataLayer.Customization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace DataLayer.Entities
{
    [Table("pack")]
    public class Pack : Entity<Pack>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; protected set; }

        [Category("Ownership")]
        [Display(Name = "Owner Group", Description = "The primary group owning this pack")]
        public int? GroupId { get; set; }

        [ForeignKey(nameof(GroupId))]
        public virtual Group? Group { get; set; }

        [Category("Ownership")]
        [Display(Name = "Created By", Description = "The user who created this pack")]
        public int? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User? User { get; set; }

        [Required]
        [Category("Content")]
        [Display(Name = "Title", Description = "The display title of the card pack", Order = 0)]
        public string Title { get; set; } = string.Empty;

        [Category("Content")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description", Description = "Detailed description of the pack's content", Order = 1)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0, 999999.99)]
        [Column(TypeName = "decimal(18, 2)")]
        [Category("Economics")]
        [Display(Name = "Price", Description = "Set the purchase price for this pack")]
        public decimal Price { get; set; } = 0;

        [Required]
        [Range(0, 999999)]
        [Category("Economics")]
        [Display(Name = "Tokens", Description = "Set the purchase tokens for this pack")]
        public int Tokens { get; set; } = 0;

        [Required]
        [MaxLength(16)]
        [Category("Status")]
        [Display(Name = "Status", Description = "Current publication state")]
        public PackStatus Status { get; set; } = PackStatus.Unpublished;

        [Category("Stats")]
        [Display(Name = "Downloads", Description = "Total number of times this pack was acquired")]
        public int Downloads { get; set; } = 0;

        [Range(0, 5)]
        [Column(TypeName = "decimal(3, 2)")]
        [Category("Stats")]
        [Display(Name = "Rating")]
        public decimal Rating { get; set; } = 0;

        [Category("Scheduling")]
        [Display(Name = "Active From", Description = "Date when pack becomes available")]
        public DateTime? ActiveFrom { get; set; }

        [Category("Scheduling")]
        [Display(Name = "Active To", Description = "Date when pack expires")]
        public DateTime? ActiveTo { get; set; }

        [Required]
        [Category("System")]
        public DateTime Created { get; set; } = DateTime.UtcNow;

        [Category("System")]
        public DateTime? Modified { get; set; }

        // Mapped to Doctrine's simple_array or array types
        [NotMapped]
        public List<string> Tags { get; set; } = new();

        // Navigation Collections (Doctrine ManyToMany/OneToMany)
        //public virtual ICollection<Group> Groups { get; set; } = new HashSet<Group>();
        //public virtual ICollection<Coupon> Coupons { get; set; } = new HashSet<Coupon>();
        //public virtual ICollection<UserPack> UserPacks { get; set; } = new HashSet<UserPack>();
        [Category("Content")]
        [Display(Name = "Cards", Description = "Set of card content to display to users")]
        public virtual ICollection<Card> Cards { get; set; } = new HashSet<Card>();
    }
}