using Domain.User.Model.Enum;

namespace Domain.User.Model.Dto
{
    public class UserDto
    {
        public string Name { get; set; }
        public string Credential { get; set; }
        public UserTypeEnum Type { get; set; }
        public Guid EntityId { get; set; }
    }
}
