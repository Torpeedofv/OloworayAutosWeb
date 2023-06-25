using System;
using BCrypt.Net;

namespace OloworayAutos.Models.Security
{
	public class HashedPassword
	{
		public static string HashPassword(string password)
		{
			// Generate a random salt
			string salt = BCrypt.Net.BCrypt.GenerateSalt();

			// Hash the password with the salt
			string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

			return hashedPassword;
		}

		public static bool VerifyPassword(string password, string hashedPassword)
		{
			// Verify the password against the hashed password
			bool isValid = BCrypt.Net.BCrypt.Verify(password, hashedPassword);

			return isValid;
		}
	}
}
