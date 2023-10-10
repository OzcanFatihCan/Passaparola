using EntityLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DALSorular
    {
        public static List<EntitySorular> SoruGetir()
        {
            List<EntitySorular> Sorular=new List<EntitySorular>();
            SqlCommand komut = new SqlCommand("SELECT * FROM TBLSORULAR",Baglanti.conn);
            if (komut.Connection.State != ConnectionState.Open)
            {
                komut.Connection.Open();
            }
            SqlDataReader dr=komut.ExecuteReader();
            while (dr.Read())
            {
                EntitySorular ent=new EntitySorular();
                ent.SORU = dr["SORU"].ToString();
                ent.CEVAP = dr["CEVAP"].ToString();
                Sorular.Add(ent);
            }
            dr.Close();
            return Sorular;
        }
    }
}
