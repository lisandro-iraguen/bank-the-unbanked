using Data.Oracle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.Oracle
{
    public interface IPriceServices
    {
        Task<CriptoDTO> DollarApiCall();
    }
}
