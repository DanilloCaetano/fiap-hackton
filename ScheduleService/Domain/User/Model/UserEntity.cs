using Domain.Base.Entity;
using Domain.User.Model.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.User.Model
{
    [Table("User")]
    public class UserEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Credential { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserTypeEnum Type { get; set; }
        public Guid EntityId { get; set; }
    }
}
