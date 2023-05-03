using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp3.DAL;
using WindowsFormsApp3.Entities;
namespace WindowsFormsApp3.BLL
{
    class MenuServiceImpl:MenuService
    {
       public MenuDao menuDao;

        public  MenuItemEntity creazaProdus(string productName, double pret, int stoc)
        {

            MenuItemEntity produs=   new MenuItemEntity(productName, pret, stoc);

            if (!menuDao.createProduct(produs))
            return null;

            return produs;





        }
        MenuItemEntity getMenuItemByName(string productName)
        {

           return menuDao.getProductByName(productName);

        }



        public  bool updateStocProdus(string menuItemname, int newStoc)
        {

            MenuItemEntity produs = new MenuItemEntity(menuItemname, -1, newStoc);
            if (menuDao.updateProduct(produs) != null)
                return true;
                return false; 


        }
      public  bool updateNumeProdus(string menuItemname, string newName)
        {
           return menuDao.changeProductName(menuItemname,newName);
         
        }
       public bool updatePretProdus(string menuItemname, double newPret)
        {
            MenuItemEntity produs = new MenuItemEntity(menuItemname, newPret,-1);
            if (menuDao.updateProduct(produs) != null)
                return true;
            return false;
        }
      public  List<MenuItemEntity> getListaAllProducts()
        {
            return menuDao.getAllProducts();
           
        }
      public   void deleteProduct(string Productname)
        {
            MenuItemEntity m = menuDao.getProductByName(Productname);
            menuDao.deleteProduct(m);


        }
   

        public MenuItemEntity getMenuItemForCommand(string productName,int quantity)
        {
            MenuItemEntity m = menuDao.getProductByName(productName);

            if (m.stoc < quantity)
                return null;
            return m;


        }

        public List<MenuItemEntity> getAllMenu()
        {
            return menuDao.getAllProducts();
        }
    }
}
