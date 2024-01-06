

using Data.Exceptions;
using System;

namespace Data.Errors
{
	public class DataSignException : ErrorCodeException
	{
		public DataSignException()
		{
		}

		public DataSignException(string message)
			: base(message)
		{
		}

		public DataSignException(string message, Exception inner)
			: base(message, inner)
		{
		}

		public DataSignException(InfoCodeError error, string message, Exception inner)
			: base(error, message, inner)
		{
		}
	}
}