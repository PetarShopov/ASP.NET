using AutoMapper;
using LiveBet.Data.Models;
using LiveBet.Infrastructure.Mapping;

namespace LiveBet.ViewModels
{
    public class OddViewModel : IMapFrom<Odd>, IHaveCustomMappings
    {
        public int TempId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string SpecialBetValue { get; set; }
        public int BetId { get; set; }
        public Bet Bet { get; set; }
        public void CreateMappings(IMapperConfiguration configuration)
        {
        }
    }
}