namespace LiveBet.Data.Common.Contracts
{
    using LiveBet.Data.Models;
    using System.Data.Entity;
    public interface IData
    {
        DbContext Context { get; }
        IDbRepository<Sport> Sports { get; }
        IDbRepository<Event> Events { get; }
        IDbRepository<Match> Matches { get; }
        IDbRepository<Bet> Bets { get; }
        IDbRepository<Odd> Odds { get; }
        IDbRepository<User> Users { get; }
        void Dispose();
        int SaveChanges();
    }
}
