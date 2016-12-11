using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoRepository
{
    public class NoSuchTodoItemException : Exception
    {
        public NoSuchTodoItemException()
        {

        }

        public NoSuchTodoItemException(string message)
            : base(message)
        {

        }

        public NoSuchTodoItemException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
