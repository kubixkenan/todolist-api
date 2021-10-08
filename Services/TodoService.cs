using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TodoService : ITodoService
    {

        protected ApplicationDbContext _context;
        public TodoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TodoModel> Add(TodoModel model)
        {
            var todoModel = await _context.AddAsync(model);
            await _context.SaveChangesAsync();
            return todoModel.Entity;
        }

        public async Task<List<TodoModel>> ListAll(Guid userId)
        {
            var entity = _context.Set<TodoModel>();
            var todoList = await entity.Where(x => x.UserId == userId).ToListAsync();
            return todoList;

        }

        public async Task SetStatus(int id, bool completed)
        {
            var entity = _context.Set<TodoModel>();
            var todoEntity = entity.Where(x => x.Id == id).FirstOrDefault();
            if(todoEntity == null)
            {
                throw new Exception("Data not found!");
            }

            todoEntity.Done = completed;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = _context.Set<TodoModel>();
            var todoEntity = entity.Where(x => x.Id == id).FirstOrDefault();
            _context.Remove(todoEntity);
            await _context.SaveChangesAsync();
        }
    }
}
