namespace LiveBet.Data.Models
{
    using LiveBet.Data.Models.Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    public class Match : BaseModel
    {
        public Match()
        {
            this.Bets = new HashSet<Bet>();
        }
        [JsonProperty("@Bet")]
        public virtual ICollection<Bet> Bets { get; set; }
        [JsonProperty("@StartDate")]
        public DateTime StartDate { get; set; }
        [JsonProperty("@MatchType")]
        public string MatchType { get; set; }
        public int EventId { get; set; }
        public virtual Event Event { get; set; }
    }
}
