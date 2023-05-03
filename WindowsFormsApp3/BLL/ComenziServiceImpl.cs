using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp3.DAL;
using WindowsFormsApp3.Entities;
namespace WindowsFormsApp3.BLL
{
    class ComenziServiceImpl:ComenziService
    {
        public ComandaDao comandaDao;
      

        public ComandaEntity adaugaComanda(List<MenuItemEntity>  produse, List<int> quantities)
        {

            DateTime dataComanda = DateTime.Now;
            string status = "New";
            double pretTotal = 0;

            for (int i = 0; i < produse.Count; i++)
            {
                pretTotal += quantities[i] * produse[i].pret;


            }

                ComandaEntity com = new ComandaEntity(produse,pretTotal,status,dataComanda);
              if(comandaDao.createComanda(com, quantities))
                return com;
            return null;


        }

        public void modifyStatusComanda(long id, string newStatus)
        {
            comandaDao.updateStatus(id, newStatus);

        }
        public ComandaEntity getComandaById(long id)
        {

           return comandaDao.getComandaById(id);


        }
        public string raportStatisticiProduseComandate()
        {

            List<ComandaEntity> cmds= comandaDao.getAllComenzi();
            SortedDictionary<MenuItemEntity, int> map=new SortedDictionary<MenuItemEntity, int>();

            foreach (ComandaEntity cmd in cmds)
            {


                foreach (MenuItemEntity m in cmd.produseList)
                {
                    if(map.Keys!=null)
                    if (!map.Keys.Contains(m))
                    {
                        map.Add(m, 1);

                    }
                    else
                    {
                        map[m] += 1;
                    }

                }

            }
            var sortedDict = from entry in map orderby entry.Value descending select entry;
            StringBuilder stringBuilder = new StringBuilder();
            foreach(KeyValuePair<MenuItemEntity,int> pair in sortedDict )
            {
                stringBuilder.Append("product_Id: ").Append(pair.Key.id).Append(" product_name: ").Append(pair.Key.numePreparat).
                 Append(" pret: ").Append(pair.Key.pret).Append(" stoc: ").Append(pair.Key.stoc).Append(" frequency: ").
                Append(pair.Value).Append(Environment.NewLine);

            }




            return stringBuilder.ToString();

        }
        public string generateRaportComenzi(DateTime day1, DateTime day2)
        {


            List<ComandaEntity>  cmds=comandaDao.getComandaByInterval(day1, day2);
            StringBuilder stringBuilder = new StringBuilder();
            foreach(ComandaEntity cmd in cmds)
            {
                    stringBuilder.Append("comanda_Id: ").Append(cmd.id).Append(" pret_total: ").Append(cmd.pretTotal).Append(" status: ").
                        Append(cmd.status).Append(Environment.NewLine);


                int i = 0;
                foreach (MenuItemEntity m in cmd.produseList)
                {
                    stringBuilder.Append("product_Id: ").Append(m.id).Append(" product_name: ").Append( m.numePreparat).
                         Append(" pret: ").Append(m.pret).Append(" stoc: ").Append(m.stoc).Append(Environment.NewLine);


                }



            }






            return stringBuilder.ToString();



        }

        public List<ComandaEntity> getAllComenzi()
        {
            return comandaDao.getAllComenzi();
        }
    }
}
