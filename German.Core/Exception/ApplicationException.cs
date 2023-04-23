using System;
using System.Collections.Generic;
using System.Text;


namespace German.Core.Exception
{
    public class ApplicationException : SystemException

    {
        public ApplicationException() { }
        public ApplicationException(string message):base(message) { }




    }
}
