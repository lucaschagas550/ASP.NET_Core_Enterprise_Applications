using System;
using FluentValidation.Results;
using MediatR;

namespace NSE.Core.Messages
{
    //CommandHandler não é obrigatorio retornar um objeto
    //mas neste caso ele retornara um ValidationResult
    public abstract class Command : Message, IRequest<ValidationResult>
    {
        public DateTime Timestamp { get; private set; }

        //Do pacote FluentValidation, possui uma lista de erros
        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public virtual bool EhValido()
        {
            throw new NotImplementedException();
        }
    }
}