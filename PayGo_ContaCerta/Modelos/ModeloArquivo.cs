using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayGo_ContaCerta
{
    public class ModeloArquivo
    {
        public string Nome { get; set; }
        public string Cpf_Cnpj { get; set; }
        public string Bank_Code { get; set; }
        public string Agency { get; set; }
        public string Agency_digit { get; set; }
        public string Account { get; set; }
        public string Account_digit { get; set; }
        public string Account_type { get; set; }
        public string Integration_id { get; set; }

        public ModeloArquivo()
        {

        }

        public ModeloArquivo(string nome, string cpfCnpj, string bankCode, string agency,
                             string agencyDigit, string account, string accountDigit, string accountType, string integrationId)
        {
            Nome = nome;
            Cpf_Cnpj = cpfCnpj;
            Bank_Code = bankCode;
            Agency = agency;
            Agency_digit = agencyDigit;
            Account = account;
            Account_digit = accountDigit;
            Account_type = accountType;
            Integration_id = integrationId;
        }

        public string GetNome()
        {
            return Nome;
        }

        public string GetCpfCnpj()
        {
            return Cpf_Cnpj;
        }

        public string GetBankCode()
        {
            return Bank_Code;
        }

        public string GetAgency()
        {
            return Agency;
        }

        public string GetAgencyDigit()
        {
            return Agency_digit;
        }

        public string GetAccount()
        {
            return Account;
        }

        public string GetAccountDigit()
        {
            return Account_digit;
        }

        public string GetAccountType()
        {
            return Account_type;
        }

        public string GetIntegrationId()
        {
            return Integration_id;
        }
    }
}
