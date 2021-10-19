using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScheduler.BLL.Interfaces
{
    public interface IBaseService<TModel>
    {
        IEnumerable<TModel> GetAll();
        Task<TModel> GetByIdAsync(Guid id);
        Task AddAsync(TModel model);
        Task UpdateAsync(TModel model);
        Task DeleteByIdAsync(Guid id);
    }
}
