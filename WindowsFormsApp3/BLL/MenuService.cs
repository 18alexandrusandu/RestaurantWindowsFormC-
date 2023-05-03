using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp3.Entities;
namespace WindowsFormsApp3.BLL
{
   public interface MenuService
    {

        MenuItemEntity creazaProdus(string productName, double pret, int stoc);
        MenuItemEntity getMenuItemForCommand(string productName,int quantity);
        List<MenuItemEntity> getAllMenu();
        bool updateStocProdus(string menuItemname, int newStoc);
        bool updateNumeProdus(string menuItemname, string newName);
        bool updatePretProdus(string menuItemname, double newPret);
        List<MenuItemEntity> getListaAllProducts();
        void deleteProduct(string Productname);
     

    }
}
