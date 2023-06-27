namespace API.Utilities
{
    public class Hashing
    {
        private static string GeneralSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12); // 12 is default
        }

        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, GeneralSalt());
        }

        public static bool ValidatePassword(string password, string hashPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashPassword);
        }
    }
}
