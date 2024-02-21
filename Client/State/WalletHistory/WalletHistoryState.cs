using AutoMapper;
using CardanoSharp.Koios.Client.Contracts;
using Data.DTOs;
using Data.History;
using Data.Wallet;
using Fluxor;
using Radzen.Blazor.Rendering;

namespace Client.State.WalletHistory
{
    [FeatureState]
    public class WalletHistoryState
    { 
        public List<TxHistoryDto> Transactions { get; }
        public bool IsLoading { get; }
        private WalletHistoryState() { }

        public WalletHistoryState(bool isLoading, TxHistory[]? transactions)
        {
            IsLoading = isLoading;
            Transactions = new List<TxHistoryDto>();
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperConfig>());
            IMapper mapper = config.CreateMapper();
            if (transactions is not null)
            {
                Transactions = mapper.Map<List<TxHistoryDto>>(transactions);

            }
        }

    }
}
