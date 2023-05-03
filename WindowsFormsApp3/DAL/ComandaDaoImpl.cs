using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp3.Entities;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data;

namespace WindowsFormsApp3.DAL
{
    class ComandaDaoImpl:ComandaDao
    {
        private static ComandaDao ComandaEntitysDAL = null;
        private String connectionString = @"Data Source=DESKTOP-QFVDQ5F\SQLEXPRESS;Initial Catalog=restaurant_db;Integrated Security=True";
        SqlConnection conn = null;

        private ComandaDaoImpl()
        {
            conn = new SqlConnection(connectionString);
        }

        public static ComandaDao getInstance()
        {
            if (ComandaEntitysDAL == null)
            {
                ComandaEntitysDAL = new ComandaDaoImpl();
            }
            return ComandaEntitysDAL;
        }


      public  bool createComanda(ComandaEntity comanda,List<int> quantities )
        {

            //String sql = "INSERT INTO dbo.comanda(status,costTotal,dataOra) VALUES("+comanda.status+","+comanda.pretTotal+","+comanda.dateAndHour+ "); " +
            //         "SELECT SCOPE_IDENTITY()"; 
            String sql = "restaurant_db.dbo.INSERT_COMANDA";





            String sql2 = "INSERT INTO dbo.comanda_produs(id_comanda,id_menuitem,quantity) VALUES(";
            String sql3 = "SELECT * FROM dbo.menu where numePreparat='";

            long id_comanda = -1;

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@status", SqlDbType.NChar,30);
                cmd.Parameters.Add("@pretTotal",SqlDbType.Real);
                cmd.Parameters.Add("@dateAndHour",SqlDbType.DateTime);
                cmd.Parameters.Add("@id",SqlDbType.BigInt).Direction = ParameterDirection.Output;

                cmd.Parameters["@status"].Value = comanda.status;
                cmd.Parameters["@pretTotal"].Value = comanda.pretTotal;
                cmd.Parameters["@dateAndHour"].Value = comanda.dateAndHour;

                Debug.WriteLine("before insert ");
                int result = cmd.ExecuteNonQuery();
                Debug.WriteLine("insert executed succesfully ");
           
              if(result>0)
                {


                    id_comanda = Convert.ToInt64(cmd.Parameters["@id"].Value);
                    
                    Debug.WriteLine("ajuns aici fara eroare");
                    Debug.WriteLine("id comanda:" + id_comanda);

                    if (id_comanda >= 0)
                    {
                        int i = 0;
                        foreach (MenuItemEntity m in comanda.produseList)
                        {

                            SqlCommand cmd2 = new SqlCommand(sql3 + m.numePreparat + "'", conn);

                            SqlDataReader reader = cmd2.ExecuteReader();
                         
                            if (reader.HasRows)
                            {
                                reader.Read();

                       

                                long id_item = long.Parse(reader["id"].ToString());



                                reader.Close();

                                SqlCommand cmd3 = new SqlCommand(sql2 + id_comanda + "," + id_item +","+quantities[i]+ ")", conn);
                               int result2= cmd3.ExecuteNonQuery();


                            }

                            i++;




                        }
                        conn.Close();


                        return true;

                    }









                }

            }
            catch(Exception e)
            {

                Debug.WriteLine("there was an error" + e.Message);


            }

            conn.Close();

            return false;
        }

















        public ComandaEntity getComandaById(long id)
        {
            ComandaEntity comanda = null;
            String sql = "SELECT * FROM dbo.comanda WHERE id=" + id;
            String sql2 = "SELECT * FROM dbo.comanda_produs WHERE id_comanda=" + id;
            String sql3 = "SELECT * FROM dbo.menu where id=";

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlCommand cmd2 = new SqlCommand(sql2, conn);
             



                SqlDataReader reader = cmd.ExecuteReader();
             

                if (reader.HasRows)
                {

                    reader.Read();

                    comanda = new ComandaEntity(double.Parse(reader["costTotal"].ToString()), reader["status"].ToString(), long.Parse(reader["id"].ToString()), 
                        DateTime.Parse(reader["dataOra"].ToString()));

                    reader.Close();
                    SqlDataReader reader2 = cmd2.ExecuteReader();


                    List<string> id_menueitems = new List<string>();

                    comanda.quantities= new List<int>();

                    if (reader2.HasRows)
                    {
                      

                        comanda.produseList = new List<MenuItemEntity>();
                        while (reader2.Read())
                        {

                            id_menueitems.Add(reader2["id_menuitem"].ToString());
                            comanda.quantities.Add(int.Parse(reader2["quantity"].ToString()));

                        }

                        reader2.Close();

                        foreach(string s in id_menueitems)
                        { 


                            SqlCommand cmd3 = new SqlCommand(sql3 + s,  conn);
                            SqlDataReader reader3 = cmd3.ExecuteReader();

                            if (reader3.HasRows)
                            {
                                reader3.Read();


                                MenuItemEntity menuItem = new MenuItemEntity(reader3["numePreparat"].ToString(), double.Parse(reader3["pret"].ToString())
                                    , int.Parse(reader3["stoc"].ToString()), long.Parse(reader3["id"].ToString()));

                                comanda.produseList.Add(menuItem);


                            }
                            reader3.Close();


                        }
                        

                    }else
                    {
                        comanda.produseList = null;
                    }

                }
                else
                {
                    comanda = null;
                }

                conn.Close();

            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                conn.Close();
                return null;
            }
            return comanda;
        }

     




        public List<ComandaEntity> getAllComenzi()
        {
            List<ComandaEntity> ComandaEntitys = new List<ComandaEntity>();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.comanda", conn);
                String sql3 = "SELECT * FROM dbo.menu where id=";


                //citesti toate comenziile
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    List<MenuItemEntity> produseList = new List<MenuItemEntity>();
                    Double pretTotal = double.Parse(reader["costTotal"].ToString());
                    String status = reader["status"].ToString();
                    long id = long.Parse(reader["id"].ToString());
                    DateTime dateAndHour = DateTime.Parse(reader["dataOra"].ToString());

                    ComandaEntity u = new ComandaEntity(pretTotal, status, id, dateAndHour);
                    ComandaEntitys.Add(u);
                }
                Debug.WriteLine("There are " + ComandaEntitys.Count);
                reader.Close();
            

                //citesti toate legaturiile dintre comenzi si produse


                foreach (ComandaEntity c in ComandaEntitys)
                {
                       c.produseList = new List<MenuItemEntity>();
                    List<long> menuitem_ids = new List<long>();

                    SqlCommand cmd2 = new SqlCommand("SELECT * FROM dbo.comanda_produs WHERE id_comanda=" + c.id, conn);
                    SqlDataReader reader2 = cmd2.ExecuteReader();
                    c.quantities = new List<int>();


                    
           
                    if (reader2.HasRows)
                    {
                      

                        while (reader2.Read())
                        {
                            menuitem_ids.Add(long.Parse(reader2["id_menuitem"].ToString()));
                          

                            string quantityrepr = reader2["quantity"].ToString();

                            if (quantityrepr != "")
                                c.quantities.Add(int.Parse(reader2["quantity"].ToString()));
                            else
                                c.quantities.Add(-1);
                     

                        }
                        reader2.Close();
                       


                        //citesti fiecare menu item
                        foreach (long id in menuitem_ids)
                        {
                            SqlCommand cmd3 = new SqlCommand(sql3 + id.ToString(), conn);
                            SqlDataReader reader3 = cmd3.ExecuteReader();
                            if (reader3.HasRows)
                            {


                             
                                reader3.Read();


                                MenuItemEntity menuItem = new MenuItemEntity(reader3["numePreparat"].ToString(), double.Parse(reader3["pret"].ToString())
                                     , int.Parse(reader3["stoc"].ToString()), long.Parse(reader3["id"].ToString()));


                                c.produseList.Add(menuItem);


                            }
                            reader3.Close();
                        }

                    }
                    reader2.Close();
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

            Debug.WriteLine("There are again " + ComandaEntitys.Count);
            return ComandaEntitys;
        }



        public List<ComandaEntity> getComandaByInterval(DateTime start,DateTime end)
        {
            List<ComandaEntity> ComandaEntitys = new List<ComandaEntity>();
            try
            {
                conn.Open();



                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.comanda WHERE dataOra >='"+start.ToString()+"'AND  dataOra <= '"+end.ToString()+"'", conn);
                String sql3 = "SELECT * FROM dbo.menu where id=";


                Debug.WriteLine("first query is:" + cmd);


                //citesti toate comenziile
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    List<MenuItemEntity> produseList = new List<MenuItemEntity>();
                    Double pretTotal = double.Parse(reader["costTotal"].ToString());
                    String status = reader["status"].ToString();
                    long id = long.Parse(reader["id"].ToString());
                    DateTime dateAndHour = DateTime.Parse(reader["dataOra"].ToString());

                    ComandaEntity u = new ComandaEntity(pretTotal, status, id, dateAndHour);
                    ComandaEntitys.Add(u);
                }
                Debug.WriteLine("There are " + ComandaEntitys.Count);
                reader.Close();


                //citesti toate legaturiile dintre comenzi si produse


                foreach (ComandaEntity c in ComandaEntitys)
                {
                    c.produseList = new List<MenuItemEntity>();
                    List<long> menuitem_ids = new List<long>();

                    SqlCommand cmd2 = new SqlCommand("SELECT * FROM dbo.comanda_produs WHERE id_comanda=" + c.id, conn);
                    SqlDataReader reader2 = cmd2.ExecuteReader();
                    c.quantities = new List<int>();




                    if (reader2.HasRows)
                    {


                        while (reader2.Read())
                        {
                            menuitem_ids.Add(long.Parse(reader2["id_menuitem"].ToString()));


                            string quantityrepr = reader2["quantity"].ToString();

                            if (quantityrepr != "")
                                c.quantities.Add(int.Parse(reader2["quantity"].ToString()));
                            else
                                c.quantities.Add(-1);


                        }
                        reader2.Close();



                        //citesti fiecare menu item
                        foreach (long id in menuitem_ids)
                        {
                            SqlCommand cmd3 = new SqlCommand(sql3 + id.ToString(), conn);
                            SqlDataReader reader3 = cmd3.ExecuteReader();
                            if (reader3.HasRows)
                            {



                                reader3.Read();


                                MenuItemEntity menuItem = new MenuItemEntity(reader3["numePreparat"].ToString(), double.Parse(reader3["pret"].ToString())
                                     , int.Parse(reader3["stoc"].ToString()), long.Parse(reader3["id"].ToString()));


                                c.produseList.Add(menuItem);


                            }
                            reader3.Close();
                        }

                    }
                    reader2.Close();
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

            Debug.WriteLine("There are again " + ComandaEntitys.Count);
            return ComandaEntitys;
        }









        public bool deleteComanda(long id)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM dbo.comanda WHERE id = " + id , conn);
                SqlDataReader reader = cmd.ExecuteReader();


                conn.Close();
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
          
        }

        public bool updateComanda(ComandaEntity u)
        {
            return false;
        }

        public bool updateStatus(long id, string newStatus)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE dbo.comanda SET status = '"+ newStatus+"'  WHERE id = " + id, conn);
                SqlDataReader reader = cmd.ExecuteReader();


                conn.Close();
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
        }
    }


}
