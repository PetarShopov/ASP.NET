using LiveBet.Data.Common.Contracts;
using LiveBet.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace LiveBet.Data.Common
{
    public class Data : IData
    {
        private readonly DbContext dbContext;

        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();
       
        public Data(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IDbRepository<Sport> Sports
        {
            get
            {
                return this.GetRepository<Sport>();
            }
        }

        public IDbRepository<Event> Events
        {
            get
            {
                return this.GetRepository<Event>();
            }
        }

        public IDbRepository<Match> Matches
        {
            get
            {
                return this.GetRepository<Match>();
            }
        }

        public IDbRepository<Bet> Bets
        {
            get
            {
                return this.GetRepository<Bet>();
            }
        }

        public IDbRepository<Odd> Odds
        {
            get
            {
                return this.GetRepository<Odd>();
            }
        }

        public IDbRepository<User> Users
        {
            get
            {
                return this.GetRepository<User>();
            }
        }

        public DbContext Context
        {
            get
            {
                return this.dbContext;
            }
        }


        public int SaveChanges()
        {
            return this.dbContext.SaveChanges();
        }
        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.dbContext != null)
                {
                    this.dbContext.Dispose();
                }
            }
        }
        private IDbRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(DbRepository<T>);

                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.dbContext));
            }

            return (IDbRepository<T>)this.repositories[typeof(T)];
        }
    }
}