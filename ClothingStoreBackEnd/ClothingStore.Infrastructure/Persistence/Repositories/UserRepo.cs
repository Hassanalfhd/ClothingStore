using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Entities;

namespace ClothingStore.Infrastructure.Persistence.Repositories
{
    public class UserRepo: IUserRepo
    {

        private readonly ApplicationDbContext  _context;

        public UserRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public long Add(UserProfile userProfile)
        {
           _context.UserProfiles.AddAsync(userProfile);

            return userProfile.Id;
        }
    }
}
