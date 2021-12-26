using Api.Context;
using Api.Model.Common;
using Api.Model.Entities;
using Api.Persistense.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Api.Persistense.Repositories.EF
{
    public class PatientRepository:GenericRepository<Patient,Guid>,IPatientRepository
    {
        #region column Names
        const string Name = "Name";
        const string FileNo = "FileNo";
        const string PhoneNumber = "PhoneNumber";
        #endregion

        public PatientRepository(ApplicationDbContext context):base(context)
        {
        }

        public async Task<List<Patient>> GetSearchResultPagedAsync(PaginationSortAndSearchInfo paginationSortAndSearchInfo) 
        {
            IQueryable<Patient> patients = _context.Patients;

            if (paginationSortAndSearchInfo.ColumnSearch != null)
            {
                string name = "";
                if (paginationSortAndSearchInfo.ColumnSearch.TryGetValue(Name, out name)) 
                {
                    patients = patients.Where(t => t.Name.Contains(name ?? string.Empty));
                }


                string fileNoTxt = "";
                if (paginationSortAndSearchInfo.ColumnSearch.TryGetValue(FileNo, out fileNoTxt))
                {
                    int fileNo = 0;
                    if (int.TryParse(fileNoTxt, out fileNo))
                    {
                        patients = patients.Where(t => t.FileNo == fileNo);
                    }
                }

                string phoneNumber = "";
                if (paginationSortAndSearchInfo.ColumnSearch.TryGetValue(PhoneNumber, out phoneNumber))
                {
                    patients = patients.Where(t => t.PhoneNumber.Contains(phoneNumber ?? string.Empty));
                }
            }

            patients = patients.Skip(paginationSortAndSearchInfo.Skip).Take(paginationSortAndSearchInfo.PageSize);

            return await patients.ToListAsync();
        }
    }
}
