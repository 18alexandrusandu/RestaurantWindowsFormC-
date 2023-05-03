using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp3.Entities;
namespace WindowsFormsApp3.DAL
{
    interface ComandaDao
    {

        bool createComanda(ComandaEntity comanda,List<int> quantities);
        bool updateComanda(ComandaEntity comanda);
        bool updateStatus(long id, string newStatus);
        bool deleteComanda(long id);

        ComandaEntity getComandaById(long id);
        List<ComandaEntity> getAllComenzi();
        List<ComandaEntity> getComandaByInterval(DateTime start, DateTime end);
    }
}
