using QuanLyCongViec.Models.Core;

namespace QuanLyCongViec.Models.Auth
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class UserMeDTO
    {
        public UserMeDTO(User UserData, IList<string> _Roles)
        {
            Username = UserData.UserName;
            IsActive = UserData.is_active;
            Created_at = UserData.created_at;
            Updated_at = UserData.updated_at;
            Information = UserData.information;
            Profile_picture = UserData.profile_picture;
            Social_links = UserData.social_links;
            Email = UserData.Email;
            EmailConfirmed = UserData.EmailConfirmed;
            PhoneNumber = UserData.PhoneNumber;
            PhoneNumberConfirmed = UserData.PhoneNumberConfirmed;
            Roles = _Roles;
        }
        public string Username { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime? Updated_at { get; set; }
        public string Information { get; set; }
        public string Profile_picture { get; set; }
        public string Social_links { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public IList<string> Roles { get; set; }
    }

    public class RegisterModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UpdateUserDTO
    {
        public string? Information { get; set; }
        public string? Profile_picture { get; set; }
        public string? Social_links { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }

    public class UpdatePasswordDTO
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
