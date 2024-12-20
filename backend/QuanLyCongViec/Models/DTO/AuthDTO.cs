using QuanLyCongViec.Models.Core;

namespace QuanLyCongViec.Models.DTO
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class UserMeDTO
    {
        public UserMeDTO(User UserData, IList<string> _Roles)
        {
            Id = UserData.Id;
            UserName = UserData.UserName;
            IsActived = UserData.IsActived;
            CreatedAt = UserData.CreatedAt;
            UpdatedAt = UserData.UpdatedAt;
            Information = UserData.Information;
            ProfilePictureLink = UserData.ProfilePictureLink;
            SocialLinks = UserData.SocialLinks;
            Email = UserData.Email;
            EmailConfirmed = UserData.EmailConfirmed;
            PhoneNumber = UserData.PhoneNumber;
            PhoneNumberConfirmed = UserData.PhoneNumberConfirmed;
            Role = _Roles.Any() ? _Roles.First() : null;
        }
        public Guid Id {  get; set; }
        public string? UserName { get; set; }
        public bool IsActived { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? Information { get; set; }
        public string? ProfilePictureLink { get; set; }
        public string? SocialLinks { get; set; }
        public string? Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string? PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string? Role { get; set; }
    }

    public class RegisterModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UpdateUserDTO
    {
        public string? Information { get; set; }
        public string? ProfilePictureLink { get; set; }
        public string? SocialLinks { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }

    public class UpdatePasswordDTO
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
