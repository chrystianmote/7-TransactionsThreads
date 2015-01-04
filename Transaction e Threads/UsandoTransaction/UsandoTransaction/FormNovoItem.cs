using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace UsandoTransaction
{
    public partial class FormNovoItem : Form
    {
        public FormNovoItem()
        {
            InitializeComponent();
        }

        private void FormNovoItem_Load(object sender, EventArgs e)
        {
            List<Produto> lista = new List<Produto>();
            using (SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Vendas.mdf;Integrated Security=True;User Instance=True"))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Produto", conn);
                conn.Open();
                SqlDataReader leitor = cmd.ExecuteReader();
                if (leitor.HasRows)
                {
                    while (leitor.Read())
                    {
                        Produto p = new Produto()
                        {
                            Nome = leitor["Nome"].ToString(),
                            IdProduto = Convert.ToInt32(leitor["IdProduto"]),
                            Preco = Convert.ToDecimal(leitor["Preco"]),
                            Estoque = Convert.ToInt32(leitor["Estoque"])
                        };
                        lista.Add(p);
                    }
                }
                cbxProduto.DataSource = new BindingSource(lista, null);
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {

        }
    }
}
