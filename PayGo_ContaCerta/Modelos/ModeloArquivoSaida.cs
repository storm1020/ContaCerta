using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayGo_ContaCerta
{
    public class ModeloArquivoSaida
    {
        public string Errors { get; set; }
        public string Validation { get; set; }
        public ModeloArquivo ModeloArquivo { get; set; }

        public ModeloArquivoSaida()
        {

        }

        public ModeloArquivoSaida(string validation, string errors, ModeloArquivo modeloArquivo)
        {
            Errors = errors;
            Validation = validation;
        }

        public string GetValidation()
        {
            return Validation;
        }

        public string GetErrors()
        {
            return Errors;
        }

        public ModeloArquivo GetModeloArquivo()
        {
            return ModeloArquivo;
        }
    }
}
