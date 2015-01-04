using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace UsandoThreads
{
    public partial class FormPrincipal : Form
    {
        static readonly object trava = new object();
        static string texto = "";

        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void btnExecutar_Click(object sender, EventArgs e)
        {
            ECData dados1 = new ECData()
            {
                Caractere = '1',
                Quantidade = 10000,
                Saida = txtSaida
            };

            Thread t1 = new Thread(EscreverCaracteres);
            t1.Priority = ThreadPriority.Normal;
            t1.Start(dados1);

            ECData dados2 = new ECData()
            {
                Caractere = '2',
                Quantidade = 10000,
                Saida = txtSaida
            };

            Thread t2 = new Thread(EscreverCaracteres);
            t2.Priority = ThreadPriority.Normal;
            t2.Start(dados2);
        }

        private static void EscreverCaracteres(object data)
        {
            ECData dados = (ECData)data;
            for (int i = 0; i < dados.Quantidade; i++)
            {
                texto += dados.Caractere;
                dados.Saida.Text = texto;
                Application.DoEvents();
            }            
        }
    }
}