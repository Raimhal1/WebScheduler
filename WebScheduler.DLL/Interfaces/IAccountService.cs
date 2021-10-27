using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebScheduler.BLL.DtoModels;

namespace WebScheduler.BLL.Interfaces
{
    public interface IAccountService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model, string ip, CancellationToken cancellationToken);
        Task<AuthenticateResponse> RefreshToken(string token, string ip, CancellationToken cancellationToken);
        Task<bool> RevokeToken(string token, string ip, CancellationToken cancellationToken);
    }
}
