using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PayGo_ContaCerta
{
    public class EntradaArquivo
    {
        // Leitura de arquivo
        public List<ModeloArquivo> RetornaConteudoArquivo(string pathArquivo, OpenFileDialog objOpenFile)
        {
            List<ModeloArquivo> LstModelArq = new List<ModeloArquivo>();

            try
            {
                using (Stream stream = objOpenFile.OpenFile())
                using (StreamReader arquivo = new StreamReader(stream, Encoding.UTF8, true))
                {
                    while (!arquivo.EndOfStream)
                    {
                        string linha = arquivo.ReadLine();
                        LstModelArq.Add(TrataLinhaEhRetornaModeloArquivoPreenchido(linha));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Falha ao processar arquivo, erro: {0}", ex.Message));
                throw;
            }

            return LstModelArq;
        }

        private ModeloArquivo TrataLinhaEhRetornaModeloArquivoPreenchido(string linhaArquivo)
        {
            ModeloArquivo modeloArquivo = new ModeloArquivo();

            if (!string.IsNullOrWhiteSpace(linhaArquivo))
            {
                string[] linhaSplit = linhaArquivo.Split(';');

                string nome = linhaSplit[0].ToString();
                string cpfCnpj = linhaSplit[1].ToString();
                string bankCode = linhaSplit[2].ToString();
                string agency = linhaSplit[3].ToString();
                string agencyDigit = linhaSplit[4].ToString();
                string account = linhaSplit[5].ToString();
                string accountDigit = linhaSplit[6].ToString();
                string accountType = RetornaTipoConta(linhaSplit[7].ToString());
                string integrationId = linhaSplit[8].ToString();

                modeloArquivo = new ModeloArquivo(nome, cpfCnpj, bankCode, agency, agencyDigit, account, accountDigit,
                                                  accountType, integrationId);
            }

            return modeloArquivo;
        }

        private string RetornaTipoConta(string tpConta)
        {
            string contaTratada = string.Empty;

            switch (tpConta)
            {
                case "1":
                    contaTratada = "CONTA_CORRENTE";
                    break;

                case "2":
                    contaTratada = "CONTA_POUPANCA";
                    break;
            }

            return contaTratada;
        }
    }
}
