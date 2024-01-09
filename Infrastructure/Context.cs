using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Dapper;
using System.Linq;
using Infrastructure.Model;

namespace Infrastructure
{
    public class Context : IContext
    {
        private readonly string Connectionstring = "SqlConnection";
        public Context(ConnectionHelper connection)
        {
            Connectionstring = connection.connectionString;
        }
        public async Task<T> GetAsync<T>(string sp, object parms = null, CommandType commandType = CommandType.Text)
        {
            T result;
            try
            {
                using (IDbConnection db = new SqlConnection(Connectionstring))
                {
                    var response = await db.QueryAsync<T>(sp, parms, commandType: commandType);
                    result = response.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public async Task<IEnumerable<T>> GetAllAsync<T>(string sp, object parms = null, CommandType commandType = CommandType.StoredProcedure)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(Connectionstring))
                {
                    var result = await db.QueryAsync<T>(sp, parms, commandType: commandType);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }
        public async Task<dynamic> GetMultipleAsync<T1, T2, TReturn>(string sp, object parms, Func<T1, T2, TReturn> p, string splitOn
            , CommandType commandType = CommandType.StoredProcedure)
        {
            parms = prepareParam((JSONAOData)parms);
            var res = new JDataTable<TReturn>
            {
                Data = new List<TReturn>(),
            };
            try
            {
                using (IDbConnection db = new SqlConnection(Connectionstring))
                {
                    using (var reader = await db.QueryMultipleAsync(sp, param: parms, commandType: commandType))
                    {
                        var pgstng = reader.Read<JDataTable>();
                        var stuff = reader.Read<T1, T2, TReturn>(p, splitOn: splitOn).ToList();
                        var ps = pgstng?.FirstOrDefault() ?? new JDataTable();
                        res = new JDataTable<TReturn>
                        {
                            Data = stuff,
                            CurrentPage = ps.CurrentPage,
                            RecordsTotal = ps.RecordsTotal,
                            Draw = ps.Draw,
                            RecordsFiltered = ps.RecordsFiltered
                        };
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return res;
        }
        private object prepareParam(JSONAOData param)
        {
            DynamicParameters p = new DynamicParameters();
            var _additional = new Dictionary<string, dynamic>();
            try
            {
                _additional = param.param?.ToDictionary() ?? _additional;
                foreach (var item in _additional)
                {
                    p.Add(item.Key, item.Value);
                }
                p.Add(nameof(param.start), param.start);
                p.Add(nameof(param.length), param.length);
                if (param.search != null && !string.IsNullOrEmpty(param.search.value))
                    p.Add("searchText", param.search.value);
            }
            catch (Exception ex)
            {

            }
            return p;
        }
    }
}
