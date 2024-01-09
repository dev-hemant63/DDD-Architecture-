using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Infrastructure
{
    public interface IContext
    {
        Task<T> GetAsync<T>(string sp, object parms = null, CommandType commandType = CommandType.Text);
        Task<IEnumerable<T>> GetAllAsync<T>(string sp, object parms = null, CommandType commandType = CommandType.StoredProcedure);
        Task<dynamic> GetMultipleAsync<T1, T2, TReturn>(string sp, object parms, Func<T1, T2, TReturn> p, string splitOn
            , CommandType commandType = CommandType.StoredProcedure);
    }
}
