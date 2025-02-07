using EStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.IRepositories
{
    public interface ILoginRepository
    {
        Task<User> GetUserByCredentails(string email,string password);
    }
}
