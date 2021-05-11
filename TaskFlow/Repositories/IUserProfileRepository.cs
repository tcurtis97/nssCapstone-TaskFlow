using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskFlow.Models;

namespace TaskFlow.Repositories
{
    public interface IUserProfileRepository
    {
        void Add(UserProfile userProfile);
        UserProfile GetByFirebaseUserId(string firebaseUserId);

        UserProfile GetUserProfileById(int id);

        List<UserProfile> GetAllUsers();

    }
}
