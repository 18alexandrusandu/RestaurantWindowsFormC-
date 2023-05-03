using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp3.Entities;
namespace WindowsFormsApp3.BLL
{
   public interface UserService
    {

      UserEntity login(String username,String password);
      UserEntity creazaContUser(String name, String username, String password, String role);
      List<UserEntity> getAllUsers();
       
    }
}
