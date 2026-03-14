using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities
{
    [PrimaryKey(nameof(Name), nameof(Guid), nameof(RoleId))]
    [Table("setting")]
    public class Setting : Entity<Setting>
    {
        public string? Name { get; set; }
        public string? Value { get; set; }
        public string? RoleId { get; set; }
        [ForeignKey(nameof(RoleId))]
        public DataLayer.Entities.Role? Role { get; set; }
        public string? Guid { get; set; }
        [ForeignKey(nameof(Guid))]
        public DataLayer.Entities.User? User { get; set; }
    }
}
