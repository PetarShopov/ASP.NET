using AutoMapper;
using LiveBet.Data.Models;
using LiveBet.Infrastructure.Mapping;
using System.Collections.Generic;

namespace LiveBet.ViewModels
{
    public class SportViewModel : IMapFrom<Sport>, IHaveCustomMappings
    {
        public int TempId { get; set; }
        public string Name { get; set; }
        public ICollection<Event> Events { get; set; }
        public void CreateMappings(IMapperConfiguration configuration)
        {
        }
    }
}