namespace LiveBet.Data
{
    using LiveBet.Data.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;

    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public IDbSet<Sport> Sports { get; set; }
        public IDbSet<Event> Events { get; set; }
        public IDbSet<Match> Matches { get; set; }
        public IDbSet<Bet> Bets { get; set; }
        public IDbSet<Odd> Odds { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
