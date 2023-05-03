using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp3.DAL;
using WindowsFormsApp3.Entities;
namespace WindowsFormsApp3.BLL
{
    class UserServiceImpl:UserService
    {

        public UserDao userDao;

        static string getMd5Hash(string input)

        {

            // Create a new instance of the MD5CryptoServiceProvider object. 

            MD5 md5Hasher = MD5.Create();



            // Convert the input string to a byte array and compute the hash. 

            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));



            // Create a new Stringbuilder to collect the bytes 

            // and create a string. 

            StringBuilder sBuilder = new StringBuilder();



            // Loop through each byte of the hashed data  

            // and format each one as a hexadecimal string. 

            for (int i = 0; i < data.Length; i++)

            {

                sBuilder.Append(data[i].ToString("x2"));

            }



            // Return the hexadecimal string. 

            return sBuilder.ToString();

        }




        public UserEntity login(string username, string password)
        {


           // Debug.WriteLine("uncoded password:" +password+"something");



            String codedPassword = getMd5Hash(password);
            UserEntity user=userDao.getUserByUsername(username);
            if (user == null)
            {
              
                return user;

            }

           // Debug.WriteLine("User password:" + user.password+"some");
           // Debug.WriteLine("Attempt password:" + codedPassword+"some");
            if (codedPassword == user.password)
            {
                Debug.WriteLine("entered here");
                return user;
            }
            else
                return null;
            

           

        }
        public UserEntity creazaContUser(String name,String username,String password,String role)
        {


            String codedPassword = getMd5Hash(password);


            UserEntity user = new UserEntity(username, codedPassword, name, role);

           user= userDao.createUserEntity(user);
            if (user == null)
                Debug.Write("Eroare nu s-a putut crea utilizator");
            else
                user.password = null;
            
            return user;


        }

        public List<UserEntity> getAllUsers()
        {
            return userDao.ListUsers();
        }
    }
}
