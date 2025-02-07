using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Interfaces
{
    public interface IPasswordRecoveryService
    {
        Task<bool> SendResetLinkAsync(string email);
        Task<bool> ResetPasswordAsync(string email, string token, string password);


    }
}
