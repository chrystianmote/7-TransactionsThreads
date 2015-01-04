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
    public partial class FormPrincipal : Form
    {
        private Pedido Pedido { get; set; }
        public FormPrincipal()
        {
            InitializeComponent();
            Pedido = new Pedido();
            Pedido.Itens = new List<ItemPedido>();
        }

        private void btnNovoItem_Click(object sender, EventArgs e)
        {
            FormNovoItem form = new FormNovoItem();
            if (form.ShowDialog() == DialogResult.OK)
            {
                ItemPedido ip = new ItemPedido()
                {
                    IdProduto = Convert.ToInt32(form.cbxProduto.SelectedValue),
                    Quantidade = Convert.ToInt32(form.txtQtde.Text),
                    ValorUnitario = Convert.ToDecimal(form.txtValorUnit.Text),
                    Produto = form.cbxProduto.SelectedItem as Produto
                };
                Pedido.Itens.Add(ip);
                dgvItens.DataSource = new BindingSource(Pedido.Itens, null);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Pedido.Cliente = txtCliente.Text;
            Pedido.DataHora = DateTime.Now;
            Pedido.ValorTotal = Pedido.Itens.Sum(x => x.SubTotal);
            Pedido.Status = "Aguardando pagamento";
            using (SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Vendas.mdf;Integrated Security=True;User Instance=True"))
            {
                //SELECT SCOPE_IDENTITY(); - Pega o último ID auto incremento inserido na conexão em questão
                //SELECT IDENT_CURRENT("Nome da Tabela"); - Pega o último ID auto incremento inserido em qualquer conexão
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Pedido VALUES (@DataHora, @Cliente, @ValorTotal, @Status); SELECT SCOPE_IDENTITY();", conn, trans);
                    cmd.Parameters.AddWithValue("@Cliente", Pedido.Cliente);
                    cmd.Parameters.AddWithValue("@DataHora", Pedido.DataHora);
                    cmd.Parameters.AddWithValue("@ValorTotal", Pedido.ValorTotal);
                    cmd.Parameters.AddWithValue("@Status", Pedido.Status);
                    int IdPedido = Convert.ToInt32(cmd.ExecuteScalar());

                    SqlCommand cmdItem = new SqlCommand("INSERT INTO ItemPedido VALUES(@IdPedido, @IdProduto, @Quantidade, @ValorUnitario)", conn, trans);
                    foreach (var item in Pedido.Itens)
                    {
                        cmdItem.Parameters.Clear();
                        cmdItem.Parameters.AddWithValue("@IdPedido", IdPedido);
                        cmdItem.Parameters.AddWithValue("@IdProduto", item.IdProduto);
                        cmdItem.Parameters.AddWithValue("@Quantidade", item.Quantidade);
                        cmdItem.Parameters.AddWithValue("@ValorUnitario", item.ValorUnitario);
                        cmdItem.ExecuteNonQuery();
                    }
                    trans.Commit();
                    conn.Close();
                }
                catch
                {
                    trans.Rollback();
                    MessageBox.Show("ERRO");
                }
            }
        }
    }
}
