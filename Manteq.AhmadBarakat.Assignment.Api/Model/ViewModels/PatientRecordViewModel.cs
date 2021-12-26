using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Model.ViewModels
{
    public class PatientRecordViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int FileNo { get; set; }
        public DateTime BirthDate { get; set; }
        public short Gender { get; set; }
    }
}
