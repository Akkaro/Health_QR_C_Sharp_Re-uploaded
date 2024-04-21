using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Health_QR.Areas.Identity.Data;

// Add profile data for application users by adding properties to the Health_QRUser class
public class Health_QRUser : IdentityUser
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    //public string Role { get; set; }
}

