namespace LiveBet.Data.Models
{
    using LiveBet.Data.Models.Models;
    using Newtonsoft.Json;
    public class Odd : BaseModel
    {
        [JsonProperty("@Value")]
        public string Value { get; set; }
        [JsonProperty("@SpecialBetValue")]
        public string SpecialBetValue { get; set; }
        public int BetId { get; set; }

        public virtual Bet Bet { get; set; }
    }
}
