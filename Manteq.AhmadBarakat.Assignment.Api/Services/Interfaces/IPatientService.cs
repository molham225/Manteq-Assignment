using Api.Model.Common;
using Api.Model.Models;
using Api.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services.Interfaces
{
    public interface IPatientService
    {
        Task<ResultModel<PatientViewModel>> GetPatientByIdAsync(Guid id);
        Task<ResultModel<List<PatientRecordViewModel>>> GetPatientsAsync(PaginationSortAndSearchInfo paginationSortAndSearchInfo);
        Task<ResultModel<Guid>> AddPatientAsync(PatientDto patient);
        Task<ResultModel<bool>> UpdatePatientAsync(Guid id, PatientDto patient);
        Task<ResultModel<bool>> DeletePatientAsync(Guid id);
    }
}
