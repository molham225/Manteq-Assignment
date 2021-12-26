using Api.Context;
using Api.Context.Interfaces;
using Api.Persistense.Repositories;
using Api.Persistense.Repositories.EF;
using Api.Persistense.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Persistense.UnitOfWork
{
    public class UnitOfWork<T> : IUnitOfWork where T : IDbContext
    {
        private readonly IDbContext _context;
        private readonly IServiceProvider _serviceProvider;
        public UnitOfWork(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
            this._context = serviceProvider.GetRequiredService<IDbContext>();
        }

        private IPatientRepository patients;

        public IPatientRepository Patients 
        { 
            get 
            { 
                if (patients == null) 
                { 
                    patients = ActivatorUtilities.CreateInstance<PatientRepository>(_serviceProvider, (T)_context); ;
                }
                return patients;
            } 
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
