using Domain.Entities;
using Infrastructure.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IUserService
    {
        Task<Response> Create(ApplicationUsers users);
        Task<Response<IEnumerable<ApplicationUsers>>> List(int loginId);
        Task<Response<ApplicationUsers>> GetById(int loginId);
        Task<Response> DeleteUser(int Id);
    }
}
