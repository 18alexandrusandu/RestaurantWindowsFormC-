using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3.Entities
{
   public class MenuItemEntity :IComparable
    {
        public String numePreparat;
        public  Double pret;
        public  int stoc;
        public  long id;
        private string menuItemname;
        private object p;
        private int newStoc;

        public MenuItemEntity(string menuItemname)
        {
        }

        public MenuItemEntity(string menuItemname, object p, int newStoc)
        {
            this.menuItemname = menuItemname;
            this.pret = (Double)p;
            this.newStoc = newStoc;
        }

        public MenuItemEntity(String numePreparat,Double pret, int stoc,long id=1)
        {
            this.numePreparat = numePreparat;
            this.pret = pret;
            this.stoc = stoc;
            this.id = id;
        }

        public int CompareTo(object obj)
        {
            if (this.numePreparat == ((MenuItemEntity)obj).numePreparat
                && this.id == ((MenuItemEntity)obj).id
                && this.pret == ((MenuItemEntity)obj).pret
                && this.stoc == ((MenuItemEntity)obj).stoc)
                return 0;
            return -1;

        }

        override public  string ToString()
        {
            return numePreparat;
        }

    }
}
