using Microsoft.EntityFrameworkCore;
using OloworayAutos.Models.Data;
using OloworayAutos.Models.Models;
using OloworayAutos.Models.Repository.Interface;
using OloworayAutos.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OloworayAutos.Models.Repository.Repo
{
	public class UserDbRepository : IUserRepository
	{
		private readonly OloworayAutosDataBaseContext context;

		public UserDbRepository(OloworayAutosDataBaseContext context)
        {
			this.context = context;
		}
        public Users Add(Users newUser)
		{
			var existingUser = context.Users.FirstOrDefault(u => u.Email == newUser.Email);
			var existingUserPhone = context.Users.FirstOrDefault(u => u.PhoneNumber == newUser.PhoneNumber);

			if (existingUser != null || existingUserPhone != null) 
			{
				return null;
			}
			else
			{
				newUser.Password = HashedPassword.HashPassword(newUser.Password);
				newUser.UpdatedDateTime = DateTime.UtcNow.AddHours(1);
				context.Users.Add(newUser);
				context.SaveChanges();
				return newUser;
			}
		}

		public Users Delete(string Uid)
		{
			Users user = context.Users.Find(Uid);
			if (user != null)
			{
				context.Users.Remove(user);
				context.SaveChanges();
			}
			return user;
		}

		public IEnumerable<Users> GetAllUser()
		{
			return context.Users;
		}

		public Users GetByPhoneNumber(string phoneNumber)
		{
			return context.Users.Find(phoneNumber);
		}

		public Users GetUser(string Uid)
		{
			return context.Users.FirstOrDefault(u => u.Uid == Uid);
		}

		public Users GetUserByEmail(string email)
		{
			return context.Users.FirstOrDefault(u => u.Email == email);
		}

        /*public async Task<User> GetUserByIdAsync(int id)
        {
			return await context.Users.FirstOrDefaultAsync(i => i.Id == id);
        }*/

        public async Task<Users> GetUserByUid(string Uid)
        {
			return await context.Users.FirstOrDefaultAsync(g => g.Uid == Uid);
        }

        public Users GetUserByUsername(string username)
		{
			return context.Users.Find(username);
		}

		public Users Update(Users updatedUser)
		{
			var user = context.Users.Attach(updatedUser);
			user.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			updatedUser.UpdatedDateTime = DateTime.UtcNow.AddHours(1);
			context.SaveChanges();
			return updatedUser;
		}

		
	}
}
