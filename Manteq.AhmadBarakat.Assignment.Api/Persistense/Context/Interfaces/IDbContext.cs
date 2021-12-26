using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Context.Interfaces
{
    public interface IDbContext : IDisposable
    {
        public Task SaveChangesAsync();
    }
}
