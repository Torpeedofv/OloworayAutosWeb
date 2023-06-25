using OloworayAutos.Models;
using OloworayAutos.Models.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OloworayAutos.Models.Repository.Interface
{ 
	public interface IUserRepository
	{
		IEnumerable<Users> GetAllUser();
		//Task<User> GetUserByIdAsync(int id);
		Task<Users> GetUserByUid(string Uid);
		Users GetUserByEmail(string email);
		Users GetUserByUsername(string username);
		Users Update(Users updatedUser);
		Users Delete(string uid);
		Users Add(Users newUser);
		Users GetByPhoneNumber(string phoneNumber);
		
	}
}
