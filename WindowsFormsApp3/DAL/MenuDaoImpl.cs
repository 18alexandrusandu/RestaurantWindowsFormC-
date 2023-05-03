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
    class MenuDaoImpl : MenuDao
    {
        private static MenuDao usersDAL = null;
        private String connectionString = @"Data Source=DESKTOP-QFVDQ5F\SQLEXPRESS;Initial Catalog=restaurant_db;Integrated Security=True";
        SqlConnection conn = null;

        private MenuDaoImpl()
        {
            conn = new SqlConnection(connectionString);
        }

        public static MenuDao getInstance()
        {
            if (usersDAL == null)
            {
                usersDAL = new MenuDaoImpl();
            }
            return usersDAL;
        }

        public bool createProduct(MenuItemEntity m)
        {

            MenuItemEntity u = null;
            String sql = "INSERT INTO dbo.menu(numePreparat,pret,stoc) VALUES('" + m.numePreparat + "'," + m.pret + "," + m.stoc + ")";
            try
            {
                conn.Open();
                Debug.WriteLine("Connection established in creaza produs");
                SqlCommand cmd = new SqlCommand(sql, conn);
                int result = cmd.ExecuteNonQuery();


                conn.Close();


                if (result <= 0)
                    return false;



            }
            catch (SqlException e)
            {
                Console.WriteLine("DAO ERROR"+e.Message);
                conn.Close();
                return false;
            }
            return true;
        }

        



        public MenuItemEntity getProductByName(String numePreparat)
        {
            MenuItemEntity u = null;
            String sql = "SELECT * FROM dbo.menu WHERE numePreparat='" + numePreparat+"'";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    u = new MenuItemEntity(reader["numePreparat"].ToString(),double.Parse(reader["pret"].ToString()), int.Parse(reader["stoc"].ToString()), long.Parse(reader["id"].ToString()));
                }
                else
                {
                    u = null;
                }
                conn.Close();

            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                conn.Close();
                return null;
            }

            return u;
        }

        public List<MenuItemEntity> getAllProducts()
        {
            List<MenuItemEntity> users = new List<MenuItemEntity>();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.menu", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string nume = reader["numePreparat"].ToString();
                    double pret = double.Parse(reader["pret"].ToString());
                    int stoc = int.Parse(reader["stoc"].ToString());
                    long id = long.Parse(reader["id"].ToString());
                    MenuItemEntity u = new MenuItemEntity(nume, pret, stoc, id);
                    users.Add(u);
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("DAO ERROR" + exc.Message);
                throw exc;
            }
            finally
            {
                conn.Close();
            }

            return users;
        }

        public void deleteProduct(MenuItemEntity m)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM dbo.menu WHERE numePreparat = '" + m.numePreparat + "'", conn);
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

        public MenuItemEntity EditMenuItemStoc(String numePreparat,int newStoc)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE dbo.menu SET  stoc="+newStoc+" WHERE numePreparat = '" +numePreparat + "'", conn);
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
            return null;

        }

        public MenuItemEntity updateProduct(MenuItemEntity m)
        {
            try
            {
                conn.Open();
                Debug.WriteLine("Connected in update product");

                if (m.stoc >=0)
                {
                    SqlCommand cmd = new SqlCommand("UPDATE dbo.menu SET  stoc=" + m.stoc + " WHERE numePreparat = '" + m.numePreparat + "'", conn);
                    int result = cmd.ExecuteNonQuery();
                    if(result<0)
                    {
                        return null;
                    }


                }

                if ( m.pret>=0)
                {
                    SqlCommand cmd = new SqlCommand("UPDATE dbo.menu SET  pret=" + m.pret + " WHERE numePreparat ='" + m.numePreparat + "'", conn);
                    int result2 = cmd.ExecuteNonQuery();
                    if (result2 < 0)
                    {
                        return null;
                    }
                }



            }
            catch (SqlException exc)
            {
                throw exc;
            }
            finally
            {
                conn.Close();
            }
            return m;
        }

        public bool changeProductName(string oldname, string newName)
        {
            try
            {
                conn.Open();

           

      
                    SqlCommand cmd = new SqlCommand("UPDATE dbo.menu SET  numePreparat='" + newName + "' WHERE numePreparat = '" + oldname + "'", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                return true;
              



            }
            catch (SqlException exc)
            {
                throw exc;
            }
            finally
            {
                conn.Close();
            }
            return false;



        }
    }

}
