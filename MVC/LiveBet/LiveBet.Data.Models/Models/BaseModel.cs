namespace LiveBet.Data.Models.Models
{
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    public abstract class BaseModel
    {
        [Key]
        public int Id { get; set; }
        [JsonProperty("@ID")]
        public int TempId { get; set; }
        [JsonProperty("@Name")]
        public string Name { get; set; }
    }
}
