using FluentValidation.Results;

namespace NSE.Core.Messages.Integration
{
    //Resposta da Api
    public class ResponseMessage : Message
    {
        //Já possui metodos de validação
        public ValidationResult ValidationResult { get; set; }

        public ResponseMessage(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }
    }
}
