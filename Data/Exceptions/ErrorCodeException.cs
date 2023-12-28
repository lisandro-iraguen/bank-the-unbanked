using System;
using Data.Errors;

namespace Data.Exceptions
{
    public class ErrorCodeException : WebWalletException
    {
        public int Code { get; set; }

        public string Info { get; set; }

        public ErrorCodeException()
        {
        }

        public ErrorCodeException(string message)
            : base(message)
        {
        }

        public ErrorCodeException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public ErrorCodeException(InfoCodeError error, string message, Exception inner)
            : base(message, inner)
        {
            Code = error.code;
            Info = error.info;
        }
    }
}