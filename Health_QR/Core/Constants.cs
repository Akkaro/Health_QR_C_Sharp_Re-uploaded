namespace Health_QR.Core
{
    public class Constants
    {
        public static class Roles
        {
            public const string Administrator = "Administrator";
            public const string Doctor = "Doctor";
            public const string Nurse = "Nurse";
        }

        public static class Policies
        {
            public const string RequireAdmin = "RequireAdmin";
            public const string RequireDoctor = "RequireDoctor";
            public const string RequireNurse = "RequireNurse";
        }
    }
}


