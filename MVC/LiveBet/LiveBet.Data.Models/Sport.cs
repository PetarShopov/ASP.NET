namespace LiveBet.Data.Models
{
    using System.Collections.Generic;
    using LiveBet.Data.Models.Models;
    using Newtonsoft.Json;
    public class Sport : BaseModel
    {
        public Sport()
        {
            this.Events = new HashSet<Event>();
        }
        //[JsonProperty("Event")]
        public virtual ICollection<Event> Events { get; set; }
    }
}
