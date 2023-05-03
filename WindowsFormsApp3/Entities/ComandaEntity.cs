using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3.Entities
{
   public class ComandaEntity
    {

        public List<MenuItemEntity> produseList;
        public List<int> quantities;
        public Double pretTotal;
        public String status;
        public long id;
        public DateTime dateAndHour;

        public ComandaEntity(double pretTotal, string status, long id, DateTime dateAndHour)
        {
            this.pretTotal = pretTotal;
            this.status = status;
            this.id = id;
            this.dateAndHour = dateAndHour;
        }

        public ComandaEntity(List<MenuItemEntity> produseList, double pretTotal, string status, DateTime dateAndHour)
        {
            this.produseList = produseList;
            this.pretTotal = pretTotal;
            this.status = status;
            this.dateAndHour = dateAndHour;
        }
    
    }
}
