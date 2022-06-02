namespace UserApi.Settings
{
    public class Roles
    {
        public const string Admin = "Admin";
        public const string Staff = "Staff";
        public const string User = "User";

        public static string[] GetAllRoles() => new string[] { Admin, Staff, User };
    }
}
