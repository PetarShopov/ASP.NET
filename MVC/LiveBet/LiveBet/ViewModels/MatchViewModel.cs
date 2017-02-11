using AutoMapper;
using LiveBet.Data.Models;
using LiveBet.Infrastructure.Mapping;
using System;
using System.Collections.Generic;

namespace LiveBet.ViewModels
{
    public class MatchViewModel : IMapFrom<Match>, IHaveCustomMappings
    {
        public int TempId { get; set; }
        public string Name { get; set; }
        public ICollection<Bet> Bets { get; set; }
        public DateTime StartDate { get; set; }
        public string MatchType { get; set; }

        public int EventId { get; set; }

        public Event Event { get; set; }
        public void CreateMappings(IMapperConfiguration configuration)
        {
        }
    }
}