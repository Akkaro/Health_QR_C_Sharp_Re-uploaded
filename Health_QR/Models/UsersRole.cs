using Microsoft.Build.Framework;

namespace Health_QR.Models
{
    public class UsersRole
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string RoleId { get; set; }

    }
}