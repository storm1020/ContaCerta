using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PayGo_ContaCerta
{
    public class SaidaArquivo
    {
        private string diretorio = @"C:\ResultadoTransfeera\";

        // Escrita de arquivo
        public bool CriarArquivoDeRetorno(List<ModeloArquivoSaida> LstMdlArqSaida)
        {
            bool rtrn = true;
            string msg = string.Empty;

            if (!Directory.Exists(diretorio))
            {
                Directory.CreateDirectory(diretorio);
                MessageBox.Show("Pasta de resultado criada no repositório: C:/ResultadoTransfeera");
            }

            string fileName = diretorio + "result" + GetDataEhConcatena() + ".csv";
            string titles = "NOME;CPFCNPJ;CODIGO_BANCO;AGENCIA;DIGITO_AGENCIA;CONTA;DIGITO_CONTA;TIPO_CONTA;ERROS;CONTA_VALIDA?";

            FileStream stream = new FileStream(fileName, FileMode.Create);
            try
            {
                using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
                {
                    writer.WriteLine(titles);

                    foreach (var item in LstMdlArqSaida)
                    {
                        writer.WriteLine(
                                         item.GetModeloArquivo().GetNome().Replace("\"", "").Replace("name:", "").Trim() + ";" +
                                         item.GetModeloArquivo().GetCpfCnpj().Replace("\"", "").Replace("cpf_cnpj:", "").Trim() + ";" +
                                         item.GetModeloArquivo().GetBankCode().Replace("\"", "").Replace("bank_code:", "").Trim() + ";" +
                                         item.GetModeloArquivo().GetAgency().Replace("\"", "").Replace("agency:", "").Trim() + ";" +
                                         item.GetModeloArquivo().GetAgencyDigit().Replace("\"", "").Replace("agency_digit:", "").Trim() + ";" +
                                         item.GetModeloArquivo().GetAccount().Replace("\"", "").Replace("account:", "").Trim() + ";" +
                                         item.GetModeloArquivo().GetAccountDigit().Replace("\"", "").Replace("account_digit:", "").Trim() + ";" +
                                         item.GetModeloArquivo().GetAccountType().Replace("\"", "").Replace("account_type:", "").Trim() + ";" +
                                         item.GetErrors().Replace("\"", "").Replace("errors:", "").Trim() + ";" +
                                         TrataDescricaoContaValida(item.GetValidation())
                                         );
                    }
                }
            }
            catch (Exception ex)
            {
                rtrn = false;
                MessageBox.Show(string.Format("Falha ao criar arquivo de resultado(s), erro(s): {0}", ex.Message));
                throw;
            }

            return rtrn;
        }

        private string TrataDescricaoContaValida(string desc)
        {
            string descTratada = string.Empty;

            if (desc.Contains("true"))
            {
                descTratada = "Conta Válida!";
            }
            else
            {
                descTratada = "Conta Invalida!";
            }

            return descTratada;
        }

        private string GetDataEhConcatena()
        {
            string dia = DateTime.Now.Day.ToString();
            string mes = DateTime.Now.Month.ToString();
            string ano = DateTime.Now.Year.ToString();
            string hora = DateTime.Now.Hour.ToString();
            string minuto = DateTime.Now.Minute.ToString();
            string segundo = DateTime.Now.Second.ToString();
            string concatDate = Convert.ToString(dia + mes + ano + hora + minuto + segundo);

            return concatDate;
        }

        public ModeloArquivoSaida PreencherModeloDeArquivoSaida(string resposta)
        {
            ModeloArquivoSaida mdsd = new ModeloArquivoSaida();
            mdsd.ModeloArquivo = new ModeloArquivo();
            string rspTratada = string.Empty;

            if (!string.IsNullOrEmpty(resposta))
            {
                rspTratada = TrataLinhaDeArquivoRetorno(resposta);
            }

            string[] rspSplt = rspTratada.Split(',');

            string nome = rspSplt[0];
            string cpfcnpj = rspSplt[1];
            string bankcode = rspSplt[2];
            string agency = rspSplt[3];
            string agencyDigit = rspSplt[4];
            string account = rspSplt[5];
            string accountDigit = rspSplt[6];
            string accountType = rspSplt[7];
            string integrationId = rspSplt[8];
            string errors = rspSplt[10];
            string validation = rspSplt[9];

            mdsd.ModeloArquivo.Nome = nome;
            mdsd.ModeloArquivo.Cpf_Cnpj = cpfcnpj;
            mdsd.ModeloArquivo.Bank_Code = bankcode;
            mdsd.ModeloArquivo.Agency = agency;
            mdsd.ModeloArquivo.Agency_digit = agencyDigit;
            mdsd.ModeloArquivo.Account = account;
            mdsd.ModeloArquivo.Account_digit = accountDigit;
            mdsd.ModeloArquivo.Account_type = accountType;
            mdsd.ModeloArquivo.Integration_id = integrationId;
            mdsd.Errors = errors;
            mdsd.Validation = validation;

            return mdsd;
        }

        public string TrataLinhaDeArquivoRetorno(string conteudo)
        {
            string retorno = string.Empty;

            retorno = conteudo.Replace("\r", "");

            retorno = retorno.Replace("\n", "");

            retorno = retorno.Replace("{", "");

            retorno = retorno.Replace("}", "");

            retorno = retorno.Replace("[", "");

            retorno = retorno.Replace("]", "");

            return retorno;
        }
    }
}
