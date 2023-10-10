using DataAccessLayer;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class LogicSorular
    {
        public static List<EntitySorular> LLSorular()
        {
            return DALSorular.SoruGetir();
        }
    }
}
