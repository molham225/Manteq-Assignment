using Api.Persistense.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Persistense.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IPatientRepository Patients { get; }
        Task SaveAsync();
    }
}
