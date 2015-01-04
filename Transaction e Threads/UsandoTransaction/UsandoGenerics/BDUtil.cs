using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Reflection;

namespace UsandoGenerics
{
    class BDUtil<T>
    {
        public void Adicionar(T obj)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Vendas.mdf;Integrated Security=True;User Instance=True");
            Type tipoObj = typeof(T);
            var props = tipoObj.GetProperties();
            string sql = "INSERT INTO " + tipoObj.Name + " VALUES({0})";
            string propsText = "";
            foreach (var prop in props)
            {
                if (prop.Name != "Id" + tipoObj.Name)
                {
                    propsText += "@" + prop.Name + ",";
                    prop.GetValue(obj, null);
                }
            }
            propsText = propsText.Remove(propsText.LastIndexOf(","), 1);
            
            sql = sql.Replace("{0}", propsText);

            SqlCommand cmd = new SqlCommand(sql, conn);

            foreach (var prop in props)
            {
                if (prop.Name != "Id" + tipoObj.Name)
                {
                    cmd.Parameters.AddWithValue("@" + prop.Name, prop.GetValue(obj, null));
                }
            }
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}