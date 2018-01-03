using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind2
{
    public class Contexte
    {
        public static IList<string> GetPaysFournisseurs()
        {
            var list = new List<string>();

            var cmd = new SqlCommand();
            cmd.CommandText = @"select distinct A.Country from Address A 
            inner join Supplier S on S.AddressId=A.AddressId
            order by 1";  //requete pour afficher liste des pays dans SSMS jointure

            using (var cnx = new SqlConnection(Settings1.Default.Northwind2Connect))
            {
                cmd.Connection = cnx;
                cnx.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add((string)reader["Country"]);
                    }
                }

            }
            return list;
        }

        public static List<Fournisseur> GetFournisseurs(string pays)
        {

            var listFournisseurs = new List<Fournisseur>();
            var cmd = new SqlCommand();
            cmd.CommandText = @"select A.Country, S.SupplierId,S.CompanyName  
            from Supplier S 
            inner join Address A on S.AddressId=A.AddressId
            where A.Country = @pays";

            var param = new SqlParameter
            {
                SqlDbType = SqlDbType.NVarChar,
                ParameterName = "@pays",
                Value = pays
            };
            cmd.Parameters.Add(param);

            using (var cnx = new SqlConnection(Settings1.Default.Northwind2Connect))
            {
                cmd.Connection = cnx;
                cnx.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                    while (sdr.Read())
                    {
                        var fou = new Fournisseur();
                        fou.Id = (int)sdr["SupplierId"];
                        fou.CompanyName = (string)sdr["CompanyName"];

                        listFournisseurs.Add(fou);
                    }

            }
            return listFournisseurs;
        }
        public static int GetNbProduits(string pays)
        {
            var cmd = new SqlCommand();
            cmd.CommandText = @"select count (*) 
        from Product P
        join Supplier S on (S.SupplierId=P.SupplierId) 
        join Address A on (A.AddressId=S.AddressId)
        where A.Country = @pays";

            using (var cnx = new SqlConnection(Settings1.Default.Northwind2Connect))
            {
                cmd.Connection = cnx;
                cnx.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

    }
}

