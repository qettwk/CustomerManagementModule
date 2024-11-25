using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.ViewModels
{
    public class CheckedCustomerIDDTO
    {
        public bool Excepted { get; set; }
        public DateOnly dateOfBirth { get; set; }
    }
}
