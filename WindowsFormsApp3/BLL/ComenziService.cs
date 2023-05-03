using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp3.Entities;
namespace WindowsFormsApp3.BLL
{
   public  interface ComenziService
    {

       ComandaEntity adaugaComanda(List<MenuItemEntity> produse, List<int> quantities);
       void modifyStatusComanda(long id, string newStatus);
       ComandaEntity getComandaById(long id);
       List<ComandaEntity> getAllComenzi();


        string raportStatisticiProduseComandate();
       string generateRaportComenzi(DateTime day1, DateTime day2);



    }
}
