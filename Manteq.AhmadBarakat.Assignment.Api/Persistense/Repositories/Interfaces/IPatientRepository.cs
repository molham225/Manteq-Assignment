using Api.Model.Common;
using Api.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Persistense.Repositories.Interfaces
{
    public interface IPatientRepository:IGenericRepository<Patient,Guid>
    {
        Task<List<Patient>> GetSearchResultPagedAsync(PaginationSortAndSearchInfo paginationSortAndSearchInfo);
    }
}
