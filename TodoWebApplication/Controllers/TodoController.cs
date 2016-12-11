using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoRepository;
using TodoWebApplication.Models;

namespace TodoWebApplication.Controllers
{
    public class TodoController : Controller
    {
        private readonly ITodoRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;

        public TodoController(ITodoRepository repository,
        UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var todo = _repository.GetActive(Guid.Parse(currentUser.Id));
            return View(todo);
        }
        [Authorize]
        public async Task<IActionResult> Add(TodoText todo)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
                TodoItem todoItem = new TodoItem(todo.Text, Guid.Parse(currentUser.Id));
                _repository.Add(todoItem);
                return RedirectToAction("Index");
            }
            return View(todo);
        }
        [Authorize]
        public async Task<IActionResult> SeeCompleted()
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var todoList = _repository.GetCompleted(Guid.Parse(currentUser.Id));
            return View(todoList);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> MarkCompleted(Guid Id)
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
            TodoItem todo = _repository.Get(Id, Guid.Parse(currentUser.Id));
            _repository.MarkAsCompleted(Id, Guid.Parse(currentUser.Id));
            return View(todo);
        }


    }
}