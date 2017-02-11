namespace LiveBet.Data.Models
{
    using LiveBet.Data.Models.Models;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    public class Bet : BaseModel
    {
        public Bet()
        {
            this.Odds = new HashSet<Odd>();
        }
        [JsonProperty("Odd")]
        public virtual ICollection<Odd> Odds { get; set; }
        [JsonProperty("@IsLive")]
        public bool IsLive { get; set; }

        public int MatchId { get; set; }

        public virtual Match Match { get; set; }
    }
}
