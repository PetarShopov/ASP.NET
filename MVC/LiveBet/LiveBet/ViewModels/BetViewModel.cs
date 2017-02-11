using AutoMapper;
using LiveBet.Data.Models;
using LiveBet.Infrastructure.Mapping;
using System;
using System.Collections.Generic;

namespace LiveBet.ViewModels
{
    public class BetViewModel : IMapFrom<Bet>, IHaveCustomMappings
    {
        public int TempId { get; set; }
        public string Name { get; set; }
        public ICollection<Odd> Odds { get; set; }
        public bool IsLive { get; set; }

        public int MatchId { get; set; }

        public Match Match { get; set; }
        public void CreateMappings(IMapperConfiguration configuration)
        {
        }
    }
}