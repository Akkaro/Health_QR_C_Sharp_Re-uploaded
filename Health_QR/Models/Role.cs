using Microsoft.Build.Framework;

namespace Health_QR.Models
{
    public class Role
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }

    }
}
