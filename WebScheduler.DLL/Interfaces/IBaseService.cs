using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebScheduler.BLL.Interfaces
{
    public interface IBaseService<TModel, TModelDto, TModelListVm>
    {
        Task<TModelListVm> GetAll();
        Task<TModel> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(TModelDto model, CancellationToken cancellationToken);
        Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    }
}
