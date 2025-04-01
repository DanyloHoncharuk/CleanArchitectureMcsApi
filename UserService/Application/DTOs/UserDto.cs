namespace UserService.Application.DTOs
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Login { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string DateOfBirth { get; set; }
        public string CreationDate { get; set; }
        public string UpdateDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

    }
}
