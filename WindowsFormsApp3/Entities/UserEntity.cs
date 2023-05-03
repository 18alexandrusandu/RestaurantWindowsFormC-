using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3.Entities
{
   public class UserEntity
    {
        public String username;
        public String password;
        public String name;
        public String role;
        public long id;

        public UserEntity(string username, string password, string name, string role)
        {
            this.username = username;
            this.password = password;
            this.name = name;
            this.role = role;
  
        }

        public UserEntity(string username, string password, string name, string role, long id) : this(username, password, name, role)
        {
            this.id = id;
        }
    }
}
