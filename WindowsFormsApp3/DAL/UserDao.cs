using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp3.Entities;
namespace WindowsFormsApp3.DAL
{
   public interface UserDao
    {
        UserEntity getUserByUsername(String username);
        List<UserEntity> ListUsers();
        void DeleteUserEntity(UserEntity u);
        UserEntity EditUserEntity(UserEntity u);
        UserEntity createUserEntity(UserEntity u);

    }
}
