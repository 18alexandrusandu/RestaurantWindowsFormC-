using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp3.Entities;
namespace WindowsFormsApp3.DAL
{
   public interface MenuDao
    {
        MenuItemEntity updateProduct(MenuItemEntity m);
        bool changeProductName(string oldname, string newName);
        bool createProduct(MenuItemEntity m);
        MenuItemEntity getProductByName(String numePreparat);
        void deleteProduct(MenuItemEntity m);
        List<MenuItemEntity> getAllProducts();

    }
}
