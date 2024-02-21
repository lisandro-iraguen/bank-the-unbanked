using AutoMapper;
using CardanoSharp.Koios.Client.Contracts;
using Data.DTOs;
using Data.History;

namespace AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<TxHistory, TxHistoryDto>();
        }
    }
}
