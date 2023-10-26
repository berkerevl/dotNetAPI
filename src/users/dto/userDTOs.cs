namespace DotNetAPI.Users.Dtos
{
    public class CreateUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public double TotalWealth { get; set; }
        public string OrganizationId { get; set; }
    }

    public class UpdateUserDto
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string NickName { get; set; }
        public string OrganizationId { get; set; }
    }

    public enum UpdatedAttribute
    {
        FirstName,
        LastName,
        Email,
        PhoneNumber,
        NickName
    }

    public class DeleteUserDTO
    {
        public string UserId { get; set; }
        public string OrganizationId { get; set; }
    }

    public class GetMoneyDTO
    {
        public string UserId { get; set; }
        public string OrganizationId { get; set; }
    }

}

