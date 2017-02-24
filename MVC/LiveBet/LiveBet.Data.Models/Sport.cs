namespace LiveBet.Data.Models
{
    using LiveBet.Data.Models.Models;
    using System.Collections.Generic;
    public class Sport : BaseModel
    {
        public Sport()
        {
            this.Events = new HashSet<Event>();
        }
        public virtual ICollection<Event> Events { get; set; }
    }
}
