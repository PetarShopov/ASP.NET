namespace LiveBet.ViewModels
{
    using AutoMapper;
    using LiveBet.Data.Models;
    using LiveBet.Infrastructure.Mapping;
    using System.Collections.Generic;

    public class EventViewModel : IMapFrom<Event>, IHaveCustomMappings
    {
        public int TempId { get; set; }
        public string Name { get; set; }
        public ICollection<Match> Matches { get; set; }
        public int CategoryID { get; set; }
        public bool IsLive { get; set; }
        public int SportId { get; set; }
        public Sport Sport { get; set; }
        public void CreateMappings(IMapperConfiguration configuration)
        {
        }
    }
}