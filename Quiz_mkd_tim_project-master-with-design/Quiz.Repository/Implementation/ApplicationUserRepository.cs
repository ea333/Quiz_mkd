using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Quiz.Domain.Identity;
using Quiz.Repository.Data;
using Quiz.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Repository.Implementation
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        public ApplicationUserRepository(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }

        public  IdentityResult DeleteById(string userId)
        {
            var user = GetById(userId);
            var result =  _userManager.DeleteAsync(user).GetAwaiter().GetResult();
            return result;
        }

       
        public ApplicationUser GetById(string userId)
        {
            var user = _userManager.FindByIdAsync(userId).GetAwaiter().GetResult();
            return user;
        }

        public ApplicationUser GetByEmail(string userEmail)
        {
            var user = _userManager.FindByEmailAsync(userEmail).GetAwaiter().GetResult();
            return user;
        }

        public IEnumerable<ApplicationUser> GetAll(string? role)
        {
            var allUsers = _userManager.Users.ToList();

            if (role == null) 
            {
                return allUsers;
            }
            var listUser = new List<ApplicationUser>();
            foreach (var user in allUsers)
            {
                var roles =  _userManager.GetRolesAsync(user).GetAwaiter().GetResult();
                if (roles.Contains(role))
                {
                    listUser.Add(user);
                }
            }
            return listUser;
        }

        public async Task ChangeUserRole(string userId, string changeTo)
        {
            var user = GetById(userId);
            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);
            await _userManager.AddToRoleAsync(user, changeTo);
        }

        public string GetUserRole(string userId) 
        {
            var user = GetById(userId);
            var currentRoles =  _userManager.GetRolesAsync(user).GetAwaiter().GetResult();
            return currentRoles.First();
        }

        public async Task Update(ApplicationUser user)
        {
            await _userManager.UpdateAsync(user);
        }


    }
}
