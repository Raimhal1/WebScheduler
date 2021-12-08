using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WebScheduler.BLL.Interfaces
{
    public interface IBaseService<TFModelDto, TModelDto>
    {
        Task<List<TModelDto>> GetAll();
        Task<TModelDto> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(TFModelDto model, CancellationToken cancellationToken);
        Task UpdateAsync(Guid id, TFModelDto model, CancellationToken cancellationToken);
        Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    }
}
