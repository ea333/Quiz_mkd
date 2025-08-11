using Microsoft.AspNetCore.Identity;
using Quiz.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Repository.Interface
{
    public interface IApplicationUserRepository
    {
        public IEnumerable<ApplicationUser> GetAll(string? role = null);


        public ApplicationUser GetById(string userId);

        public ApplicationUser GetByEmail(string userEmail);

        public IdentityResult DeleteById(string userId);

        public  Task Update(ApplicationUser user);

        public Task ChangeUserRole(string userId,string changeTo);

        public string GetUserRole(string userId);

    }
}
