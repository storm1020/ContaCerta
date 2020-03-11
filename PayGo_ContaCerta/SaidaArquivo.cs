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
        public void CriarArquivoDeRetorno(string resposta)
        {
            ModeloArquivoSaida modeloArq = new ModeloArquivoSaida();

            if (!Directory.Exists(diretorio))
            {
                Directory.CreateDirectory(diretorio);
            }

            string fileName = diretorio + "result" + GetDataEhConcatena() + ".csv";

            FileStream stream = new FileStream(fileName, FileMode.OpenOrCreate);

            try
            {
                using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8 ))
                {
                    modeloArq = PreencherModeloDeArquivoSaida(resposta);

                    writer.WriteLine(modeloArq.GetModeloArquivo().GetNome() );
                    writer.WriteLine(modeloArq.GetModeloArquivo().GetCpfCnpj() );
                    writer.WriteLine(modeloArq.GetModeloArquivo().GetBankCode() );
                    writer.WriteLine(modeloArq.GetModeloArquivo().GetAgency() );
                    writer.WriteLine(modeloArq.GetModeloArquivo().GetAgencyDigit() );
                    writer.WriteLine(modeloArq.GetModeloArquivo().GetAccount() );
                    writer.WriteLine(modeloArq.GetModeloArquivo().GetAccountDigit() );
                    writer.WriteLine(modeloArq.GetModeloArquivo().GetAccountType() );
                    writer.WriteLine(modeloArq.GetErrors() );
                    writer.WriteLine(TrataDescricaoContaValida(modeloArq.GetValidation()) );
                    writer.WriteLine("-------------------------------------------------------");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Falha ao criar arquivo de resultado(s), erro: {0}", ex.Message));
                throw;
            }

            //return LstModelArq;
        }

        private string TrataDescricaoContaValida(string desc)
        {
            string descTratada = string.Empty;

            if (desc.Contains("true"))
            {
                descTratada = "Validação: Conta Válida!";
            }
            else
            {
                descTratada = "Validação: Conta Invalida!";
            }

            return descTratada;
        }

        private string GetDataEhConcatena()
        {
            string dia = DateTime.Now.Day.ToString();
            string mes = DateTime.Now.Month.ToString();
            string ano = DateTime.Now.Year.ToString();
            string concatDate = Convert.ToString(dia + mes + ano);

            return concatDate;
        }

        private ModeloArquivoSaida PreencherModeloDeArquivoSaida(string resposta)
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

        private string TrataLinhaDeArquivoRetorno(string conteudo)
        {
            string retorno = string.Empty;

            retorno = conteudo.Replace("\r", "");

            retorno = retorno.Replace("\n", "");

            retorno = retorno.Replace("{", "");

            return retorno;
        }
    }
}
