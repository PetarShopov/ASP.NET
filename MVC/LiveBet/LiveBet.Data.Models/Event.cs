namespace LiveBet.Data.Models
{
    using System.Collections.Generic;
    using LiveBet.Data.Models.Models;
    using Newtonsoft.Json;

    public class Event : BaseModel
    {
        public Event()
        {
            this.Matches = new HashSet<Match>();
        }
        [JsonProperty("Match")]
        public virtual ICollection<Match> Matches { get; set; }
        [JsonProperty("CategoryID")]
        public int CategoryID { get; set; }
        [JsonProperty("IsLive")]
        public bool IsLive { get; set; }
        public int SportId { get; set; }

        public virtual Sport Sport { get; set; }
    }
}
