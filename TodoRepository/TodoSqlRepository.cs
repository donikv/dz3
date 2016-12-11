using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoRepository
{
    public class TodoSqlRepository : ITodoRepository
    {
        private readonly TodoDbContext _context;
        public TodoSqlRepository(TodoDbContext context)
        {
            _context = context;
        }

        public TodoItem Get(Guid todoId, Guid userId)
        {
            if (todoId == null)
            {
                throw new ArgumentNullException();
            }
            TodoItem item = _context.TodoItems.FirstOrDefault(a => a.Id.Equals(todoId));
            if (item == null)
            {
                return null;
                return null;
            }
            if (item.UserId != userId)
            {
                throw new TodoAccessDeniedException("Access denied.");
            }
            return item;
        }

        public void Add(TodoItem todoItem)
        {
            if (todoItem == null)
            {
                throw new ArgumentNullException();
            }
            else if (_context.TodoItems.FirstOrDefault(a => a.Id.Equals(todoItem.Id))==null)
            {
                _context.TodoItems.Add(todoItem);
                _context.SaveChanges();
            }
            else throw new DuplicateTodoItemException("Duplicate id: "+todoItem.Id);
        }

        public bool Remove(Guid todoId, Guid userId)
        {
            if (_context.TodoItems.FirstOrDefault(a => a.Id.Equals(todoId)) != null)
            {
                TodoItem item = _context.TodoItems.FirstOrDefault(a => a.Id == todoId);
                if (item.UserId != userId)
                {
                    throw new TodoAccessDeniedException("Access denied.");
                }
                _context.TodoItems.Remove(_context.TodoItems.FirstOrDefault(a => a.Id.Equals(todoId)));
                _context.SaveChanges();
                return true;
            }
            else return false;
        }

        public void Update(TodoItem todoItem, Guid userId)
        {
            if (todoItem == null)
            {
                throw new ArgumentNullException();
            }
            if (todoItem.UserId != userId)
            {
                throw new TodoAccessDeniedException("Access denied.");
            }

            if (_context.TodoItems.FirstOrDefault(a => a.Id.Equals(todoItem.Id)) != null)
            {
                Remove(todoItem.Id, userId);

            }
            Add(todoItem);
        }

        public bool MarkAsCompleted(Guid todoId, Guid userId)
        {
            TodoItem item = _context.TodoItems.FirstOrDefault(a => a.Id == todoId);
            if ( item != null)
            {
                if (item.UserId != userId)
                {
                    throw new TodoAccessDeniedException("Access denied.");
                }
                item.MarkAsCompleted();
                this.Update(item, userId);
                return true;
            }
            return false;
        }

        public List<TodoItem> GetAll(Guid userId)
        {
            if (!_context.TodoItems.Any(a => a.UserId==userId)) return null;
            return _context.TodoItems.Where(a=>a.UserId==userId).OrderBy(i => i.DateCreated).ToList();
        }

        public List<TodoItem> GetActive(Guid userId)
        {
            return GetFiltered(a => a.IsCompleted == false, userId);
        }

        public List<TodoItem> GetCompleted(Guid userId)
        {
            return GetFiltered(a => a.IsCompleted == true, userId);
        }

        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction, Guid userId)
        {
            List<TodoItem> retList = GetAll(userId);
            if (retList == null) return null;
            retList = retList.Where(filterFunction).ToList();
            if (retList.Count == 0) return null;
            return retList;
        }
    }
}
