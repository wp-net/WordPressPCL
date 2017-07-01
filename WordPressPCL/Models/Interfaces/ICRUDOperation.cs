using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WordPressPCL.Utility;

namespace WordPressPCL.Models.Interfaces
{
    interface ICRUDOperation<TClass>
    {
        Task<TClass> GetByID(int ID, bool embed=false);
        IEnumerable<TClass> GetBy(Func<TClass, bool> predicate, bool embed=false);
        //Task<IEnumerable<TClass>> GetBy(QueryBuilder builder);
        Task<IEnumerable<TClass>> GetAll(bool embed=false);
        Task<TClass> Create(TClass Entity);
        Task<TClass> Update(TClass Entity);
        Task<HttpResponseMessage> Delete(int ID);
    }
}
