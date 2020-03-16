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
            List<ModeloArquivoSaida> lstMdlArqSaida = new List<ModeloArquivoSaida>();
            SaidaArquivo saidArq = new SaidaArquivo();
            ModeloArquivoSaida mdlArqSaida = new ModeloArquivoSaida();


            //Gerar Key antes de consumir outros metodos.
            var response = string.Empty;

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                lstModeloArquivo = etd.RetornaConteudoArquivo(openFile.FileName, openFile);

                foreach (var item in lstModeloArquivo)
                {
                    response = await RestHelper.Post_Authentication("client_credentials", "9b01ca70-ec3c-4db7-ac1e-ff251e91f884", "b9eaa6f9-ee96-4a20-b358-51079a3b38ef5e3390a0-c373-41b3-91b6-e636c979396a");

                    response = await RestHelper.Post_MultContaBasica(item.GetNome(), item.GetCpfCnpj(), item.GetBankCode(), item.GetAgency(),
                                                                         item.GetAgencyDigit(), item.GetAccount(), item.GetAccountDigit(),
                                                                         item.GetAccountType(), item.GetIntegrationId());
                    string trataRsp = RestHelper.BeautifyJson(response);
                    mdlArqSaida = saidArq.PreencherModeloDeArquivoSaida(trataRsp); // Preencher objetos antes de gravar.
                    lstMdlArqSaida.Add(mdlArqSaida); // Add objetos preenchidos para gravar.

                }

                bool processou = saidArq.CriarArquivoDeRetorno(lstMdlArqSaida);

                if (processou)
                {
                    MessageBox.Show("Resultado criado! Verifique o caminho: C:/ResultadoTransfeera");
                }
                else
                {
                    MessageBox.Show("Houve algum problema ao escrever o arquivo de resultado.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um arquivo.");
            }
        }
    }
}
