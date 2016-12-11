using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoRepository
{
    class TodoAccessDeniedException : Exception
    {
        public TodoAccessDeniedException()
        {

        }

        public TodoAccessDeniedException(string message)
            : base(message)
        {

        }

        public TodoAccessDeniedException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
