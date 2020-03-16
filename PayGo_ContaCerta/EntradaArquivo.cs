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

                string nome = TrataItemArquivoEntrada(linhaSplit[0].ToString()).Trim();
                string cpfCnpj = TrataItemArquivoEntrada(linhaSplit[1].ToString()).Trim();
                string bankCode = TrataItemArquivoEntrada(linhaSplit[2].ToString()).Trim();
                string agency = TrataItemArquivoEntrada(linhaSplit[3].ToString()).Trim();
                string agencyDigit = TrataItemArquivoEntrada(linhaSplit[4].ToString()).Trim();
                string account = TrataItemArquivoEntrada(linhaSplit[5].ToString()).Trim();
                string accountDigit = TrataItemArquivoEntrada(linhaSplit[6].ToString()).Trim();
                string accountType = TrataItemArquivoEntrada(RetornaTipoConta(linhaSplit[7].ToString())).Trim();
                string integrationId = TrataItemArquivoEntrada(linhaSplit[8].ToString()).Trim();

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

        private string TrataItemArquivoEntrada(string conteudo)
        {
            string retorno = string.Empty;

            retorno = conteudo.Replace("\r", "");

            retorno = retorno.Replace("\n", "");

            retorno = retorno.Replace("{", "");

            retorno = retorno.Replace("}", "");

            retorno = retorno.Replace("[", "");

            retorno = retorno.Replace("]", "");

            retorno = retorno.Replace("\"", "");

            retorno = retorno.Replace(".", "");

            retorno = retorno.Replace("-", "");

            retorno = retorno.Replace(":", "");

            retorno = retorno.Replace(",", "");

            return retorno;
        }
    }
}
