using Data.Oracle;

namespace Api.Services.Oracle
{
    public interface IPriceServices
    {
        Task<CriptoDTO> DollarApiCall();
    }
}
