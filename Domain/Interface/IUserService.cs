using Domain.Entities;
using Infrastructure.Model;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IUserService
    {
        Task<Response> Create(ApplicationUsers users);
        Task<Response> DeleteUser(int Id);
    }
}
