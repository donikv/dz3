using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoRepository
{
    public class TodoItem
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Text { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? DateCompleted { get; set; }
        public DateTime DateCreated { get; set; }
        public TodoItem(string text, Guid userId)
        {
            Id = Guid.NewGuid(); // Generates new unique identifier
            UserId = userId;
            Text = text;
            IsCompleted = false;
            DateCreated = DateTime.Now; // Set creation date as current time
            DateCompleted = null;
        }
        public TodoItem()
        {
            // entity framework needs this one
            // not for use :)
        }

        public void MarkAsCompleted()
        {
            if (!IsCompleted)
            {
                IsCompleted = true;
                DateCompleted = DateTime.Now;
            }
        }

    }
}
