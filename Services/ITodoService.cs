using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ITodoService
    {
        Task<TodoModel> Add(TodoModel model);
        Task<List<TodoModel>> ListAll(Guid userId);
        Task SetStatus(int id, bool completed);
        Task Delete(int id);
    }
}
