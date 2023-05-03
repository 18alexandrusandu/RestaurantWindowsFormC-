using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp3.Entities;
using System.Data.SqlClient;
using System.Diagnostics;

namespace WindowsFormsApp3.DAL
{
    class UserDaoImpl:UserDao
    {
        private static UserDao usersDAL = null;
        private String connectionString = @"Data Source=DESKTOP-QFVDQ5F\SQLEXPRESS;Initial Catalog=restaurant_db;Integrated Security=True";
        SqlConnection conn = null;

        private UserDaoImpl()
        {
            conn = new SqlConnection(connectionString);
        }

        public static UserDao getInstance()
        {
            if (usersDAL == null)
            {
                usersDAL = new UserDaoImpl();
            }
            return usersDAL;
        }





        public UserEntity createUserEntity(UserEntity u)
        {
           
            String sql = "INSERT INTO dbo.users(username,name,password,role) VALUES('" + u.username + "'"    
                +",'"+u.name+"',"+"'"+u.password+"','"+u.role+"')";

            String sql2 = "SELECT * FROM dbo.users WHERE username='" + u.username + "'";
            try
            {
                conn.Open();



                Debug.WriteLine("Connection established");

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlCommand cmd2 = new SqlCommand(sql2, conn);

                cmd.ExecuteNonQuery();

                SqlDataReader reader = cmd2.ExecuteReader();
                if (reader.HasRows)
                {


                    reader.Read();
                    u = new UserEntity(reader["username"].ToString(), reader["password"].ToString(),
                        reader["name"].ToString(), reader["role"].ToString(), long.Parse(reader["id"].ToString()));
                }
                else
                {
                    u = null;
                }
                conn.Close();

            }
            catch (SqlException e)
            {
                Debug.WriteLine("Eror is:",e.Message);
                conn.Close();
                return null;
            }
            return u;
        }




        public UserEntity getUserByUsername(String username)
        {
            UserEntity u = null;
            String sql = "SELECT * FROM dbo.users WHERE username='" + username + "'";
            try
            {
                conn.Open();
                Debug.WriteLine("Connection established:");
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
              
                if (reader.HasRows)
                {
                    Debug.WriteLine("found user");
                    reader.Read();
                    u = new UserEntity(reader["username"].ToString(), reader["password"].ToString(),
                        reader["name"].ToString(), reader["role"].ToString(),long.Parse(reader["id"].ToString()));
                }
                else
                {
                    Debug.WriteLine("not in here");
                    u = null;
                }
                conn.Close();

            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.Message);
                conn.Close();
                return null;
            }
            return u;
        }

        public List<UserEntity>  ListUsers()
        {
            List<UserEntity>  users = new List<UserEntity> ();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.users", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string username = reader["username"].ToString();
                    string password = reader["password"].ToString();
                    string name = reader["name"].ToString();
                    string role = reader["role"].ToString();
                    UserEntity u = new UserEntity(username, password, name, role);
                    users.Add(u);
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                conn.Close();
            }

            return users;
        }

        public void DeleteUserEntity(UserEntity u)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM dbo.users WHERE id = '" + u.username + "'", conn);
                SqlDataReader reader = cmd.ExecuteReader();
            }
            catch (SqlException exc)
            {
                throw exc;
            }
            finally
            {
                conn.Close();
            }
        }

        public UserEntity EditUserEntity(UserEntity u)
        {
            return u;
        }

    }
}

