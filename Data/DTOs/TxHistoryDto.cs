namespace Data.DTOs
{
    public class TxHistoryDto
    {
        public string To { get; set; }
        public string Hash { get; set; }
        public ulong Balance { get; set; }
        public string Link { get; set; }
    }
}
