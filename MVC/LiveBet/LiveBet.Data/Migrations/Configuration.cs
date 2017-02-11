namespace LiveBet.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using LiveBet.Common;
    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        private UserManager<User> userManager;
        private IRandomGenerator random;
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.random = new RandomGenerator();
        }

        protected override void Seed(ApplicationDbContext context)
        {
            this.userManager = new UserManager<User>(new UserStore<User>(context));
            //this.SeedRoles(context);
            //this.SeedUsers(context);
            this.SeedSports(context);
        }

        //private void SeedRoles(ApplicationDbContext context)
        //{
        //    context.Roles.AddOrUpdate(x => x.Name, new IdentityRole(GlobalConstants.AdminRole));
        //    context.SaveChanges();
        //}

        //private void SeedUsers(ApplicationDbContext context)
        //{
        //    if (context.Users.Any())
        //    {
        //        return;
        //    }

        //    for (int i = 0; i < 10; i++)
        //    {
        //        var user = new User
        //        {
        //            Email = string.Format("{0}@{1}.com", this.random.RandomString(6, 16), this.random.RandomString(6, 16)),
        //            UserName = this.random.RandomString(6, 16)
        //        };

        //        this.userManager.Create(user, "123456");
        //    }

        //    var adminUser = new User
        //    {
        //        Email = "admin@mysite.com",
        //        UserName = "Administrator"
        //    };

        //    this.userManager.Create(adminUser, "admin123456");

        //    this.userManager.AddToRole(adminUser.Id, GlobalConstants.AdminRole);
        //}
        private void SeedSports(ApplicationDbContext context)
        {
            if (context.Sports.Any())
            {
                return;
            }
            Odd oddPreliminary = new Odd();
            oddPreliminary.Name = "1";
            oddPreliminary.TempId = 44877184;
            oddPreliminary.Value = "1.17";
            context.Odds.Add(oddPreliminary);

            Odd oddPreliminary1 = new Odd();
            oddPreliminary1.Name = "2";
            oddPreliminary1.TempId = 44877185;
            oddPreliminary1.Value = "4.80";
            context.Odds.Add(oddPreliminary1);

            Bet betPreliminary2 = new Bet();
            betPreliminary2.Name = "Match Odds";
            betPreliminary2.TempId = 8069481;
            betPreliminary2.IsLive = false;
            betPreliminary2.Odds.Add(oddPreliminary);
            betPreliminary2.Odds.Add(oddPreliminary1);
            context.Bets.Add(betPreliminary2);

            Match matchPreliminary1 = new Match();
            matchPreliminary1.Name = "Flash Wolves - Machi eSports";
            matchPreliminary1.TempId = 860600;
            matchPreliminary1.StartDate = DateTime.Now;
            matchPreliminary1.MatchType = "PreMatch";
            matchPreliminary1.Bets.Add(betPreliminary2);
            context.Matches.Add(matchPreliminary1);

            Event eventPreliminary1 = new Event();
            eventPreliminary1.Name = "League of Legends, LMS Spring";
            eventPreliminary1.TempId = 29707;
            eventPreliminary1.IsLive = false;
            eventPreliminary1.CategoryID = 3578;
            eventPreliminary1.Matches.Add(matchPreliminary1);
            context.Events.Add(eventPreliminary1);

            Sport sportPreliminary1 = new Sport();
            sportPreliminary1.Name = "eSports";
            sportPreliminary1.TempId = 2357;
            sportPreliminary1.Events.Add(eventPreliminary1);
            context.Sports.Add(sportPreliminary1);


            Bet betPreliminary = new Bet();
            betPreliminary.Name = "Total Games";
            betPreliminary.TempId = 8347991;
            betPreliminary.IsLive = true;
            context.Bets.Add(betPreliminary);

            Bet betPreliminary1 = new Bet();
            betPreliminary1.Name = "Game Handicap";
            betPreliminary1.TempId = 8347990;
            betPreliminary1.IsLive = true;
            context.Bets.Add(betPreliminary1);

            Match matchPreliminary = new Match();
            matchPreliminary.Name = "V. Williams - K. Mladenovic";
            matchPreliminary.TempId = 879107;
            matchPreliminary.StartDate = DateTime.Now;
            matchPreliminary.MatchType = "Live";
            matchPreliminary.Bets.Add(betPreliminary);
            matchPreliminary.Bets.Add(betPreliminary1);
            context.Matches.Add(matchPreliminary);

            Event eventPreliminary = new Event();
            eventPreliminary.Name = "WTA St. Petersburg, R16";
            eventPreliminary.TempId = 25874;
            eventPreliminary.IsLive = false;
            eventPreliminary.CategoryID = 5659;
            eventPreliminary.Matches.Add(matchPreliminary);
            context.Events.Add(eventPreliminary);

            Sport sportPreliminary = new Sport();
            sportPreliminary.Name = "Tennis";
            sportPreliminary.TempId = 1166;
            sportPreliminary.Events.Add(eventPreliminary);
            context.Sports.Add(sportPreliminary);

            context.SaveChanges();
        } 
    }
}