using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PayGo_ContaCerta.Forms
{
    public partial class frm_interacaoAPI : Form
    {
        public frm_interacaoAPI()
        {
            InitializeComponent();
        }

        #region Expiração key
        //private System.Windows.Forms.Timer timer1;
        //private int counter = 1800000; //1800 segundos
        //int counter = 1800000;
        //Timer timer = new Timer();
        //timer.Tick += new EventHandler(GerarNovaKey);
        //timer.Interval = 1000; //1 segundo
        //timer.Start();
        #endregion

        private async void btn_Post_Click(object sender, EventArgs e)
        {
            //Gerar Key antes de consumir métodos.
            var response = string.Empty;
            response = await RestHelper.Post_Authentication("client_credentials", "2b49c7d3-6b3f-44d4-9630-885d8ffde831", "5738a69a-8da5-44a9-b40d-95eb747a703a0604fc78-4c0f-4301-a8ee-fb449c85e7a7");

            //Micro depósito teste
            //response = await RestHelper.Post_MicroDeposito("Iago Rocha Leite", "45161928883", "341", "0068", "", "07734", "6", "CONTA_CORRENTE", "");
            //txt_PostResponse.Text = RestHelper.BeautifyJson(response);

            response = await RestHelper.Post_MultContaBasica("Iago Rocha Leite", "45161928883", "341", "0068", "", "07734", "6", "CONTA_CORRENTE", "");
            txt_PostResponse.Text = RestHelper.BeautifyJson(response);

        }

        private async void btn_import_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            EntradaArquivo etd = new EntradaArquivo();
            List<ModeloArquivo> lstModeloArquivo = new List<ModeloArquivo>();
            List<ModeloArquivoSaida> mdlArqSaida = new List<ModeloArquivoSaida>();
            SaidaArquivo saidArq = new SaidaArquivo();

            //Gerar Key antes de consumir outros metodos.
            var response = string.Empty;
            response = await RestHelper.Post_Authentication("client_credentials", "2b49c7d3-6b3f-44d4-9630-885d8ffde831", "5738a69a-8da5-44a9-b40d-95eb747a703a0604fc78-4c0f-4301-a8ee-fb449c85e7a7");

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                lstModeloArquivo = etd.RetornaConteudoArquivo(openFile.FileName, openFile);

                foreach (var item in lstModeloArquivo)
                {
                    response = await RestHelper.Post_MultContaBasica(item.GetNome(), item.GetCpfCnpj(), item.GetBankCode(), item.GetAgency(),
                                                                         item.GetAgencyDigit(), item.GetAccount(), item.GetAccountDigit(),
                                                                         item.GetAccountType(), item.GetIntegrationId());

                    //string tst = RestHelper.BeautifyJson(response);

                    saidArq.CriarArquivoDeRetorno(response);
                }
            }
        }
    }
}
