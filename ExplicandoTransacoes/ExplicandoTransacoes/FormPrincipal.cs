using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ExplicandoTransacoes
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
        }
        private Pedido Pedido { get; set; }

        private void btnItem_Click(object sender, EventArgs e)
        {
            FormNovoItem form = new FormNovoItem();
            if (form.ShowDialog() == DialogResult.OK)
            {
                ItemPedido ip = new ItemPedido()
                {
                    IdProduto = Convert.ToInt32(form.cbxProduto.SelectedValue),
                    Quantidade = Convert.ToInt32(form.txtQtde.Text),
                    ValorUnitario = Convert.ToDecimal(form.txtValorUnit.Text)
                };
                Pedido.Itens.Add(ip);
            }


        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            Pedido.Cliente = txtCliente.Text;
            Pedido.DataHora = DateTime.Now;
            Pedido.Status = Status.Pendente;
            Pedido.VatorTotal = Pedido.Itens.Sum(x => x.Subtotal);
            using (SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Vendas.mdf;Integrated Security=True;User Instance=True"))
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("INSERT INTO Pedido (Cliente,DataHora,ValorTotal,Status)");
                sql.Append("VALUES (@Cliente, @DataHora, @ValorTotal, @Status); ");
                sql.Append("Select scope_identity()");

                SqlCommand cmd = new SqlCommand(sql.ToString(), con);
                cmd.Parameters.AddWithValue("@Cliente", Pedido.Cliente);
                cmd.Parameters.AddWithValue("@DataHora", Pedido.DataHora);
                cmd.Parameters.AddWithValue("@ValorTotal", Pedido.VatorTotal);
                cmd.Parameters.AddWithValue("@Status", Pedido.Status);

                SqlTransaction trans = con.BeginTransaction();

                con.Open();
                int idProduto = Convert.ToInt32(cmd.ExecuteScalar());

            }
        }
    }
}

