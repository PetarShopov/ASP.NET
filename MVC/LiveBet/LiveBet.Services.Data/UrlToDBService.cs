namespace LiveBet.Services.Data
{
    using LiveBet.Data.Common.Contracts;
    using LiveBet.Data.Models;
    using LiveBet.Services.Data.Contracts;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;
    public class UrlToDBService : IUrlToDBService
    {
        private readonly IData data;

        public UrlToDBService(IData data)
        {
            this.data = data;
        }

        public ICollection<Sport> GetData()
        {
            XmlDocument document = new XmlDocument();
            document.Load("http://vitalbet.net/sportxml");
            string jsonText = JsonConvert.SerializeXmlNode(document);
            var sportData = JObject.Parse(jsonText)["XmlSports"]["Sport"];
            ICollection<Sport> sports = new List<Sport>();

            for (int i = 0; i < sportData.Count(); i++)
            {
                Sport sportToAdd = new Sport();

                if (sportData[i]["@Name"] != null)
                {
                    sportToAdd.Name = sportData[i]["@Name"].ToString();
                }
                sportToAdd.TempId = int.Parse(sportData[i]["@ID"].ToString());

                if (sportData[i]["Event"] != null && sportData[i]["Event"] is JArray)
                {
                    for (int j = 0; j < sportData[i]["Event"].Count(); j++)
                    {
                        var eventInfo = sportData[i]["Event"][j];
                        Event eventToAdd = new Event();
                        eventToAdd.Name = eventInfo["@Name"].ToString();
                        eventToAdd.TempId = int.Parse(eventInfo["@ID"].ToString());
                        eventToAdd.IsLive = Convert.ToBoolean(eventInfo["@IsLive"].ToString());
                        eventToAdd.CategoryID = int.Parse(eventInfo["@CategoryID"].ToString());

                        if (eventInfo["Match"] != null && eventInfo["Match"] is JArray)
                        {
                            for (int k = 0; k < sportData[i]["Event"][j]["Match"].Count(); k++)
                            {
                                var matchInfo = sportData[i]["Event"][j]["Match"][k];
                                Match matchToAdd = new Match();
                                matchToAdd.Name = matchInfo["@Name"].ToString();
                                matchToAdd.TempId = int.Parse(matchInfo["@ID"].ToString());
                                matchToAdd.StartDate = Convert.ToDateTime(matchInfo["@StartDate"].ToString());
                                matchToAdd.MatchType = matchInfo["@MatchType"].ToString();

                                if (matchInfo["Bet"] != null && matchInfo["Bet"] is JArray)
                                {
                                    var typeOfBB = (matchInfo["Bet"]).GetType();
                                    var badBedC = matchInfo["Bet"].Count();
                                    for (int l = 0; l < matchInfo["Bet"].Count(); l++)
                                    {
                                        var bedInfo = sportData[i]["Event"][j]["Match"][k]["Bet"][l];
                                        Bet betToAdd = new Bet();

                                        if (sportData[i]["Event"][j]["Match"][k]["Bet"].ElementAtOrDefault(l) != null)
                                        {
                                            betToAdd.Name = bedInfo["@Name"].ToString();
                                        }
                                        betToAdd.TempId = int.Parse(bedInfo["@ID"].ToString());
                                        betToAdd.IsLive = Convert.ToBoolean(bedInfo["@IsLive"].ToString());

                                        if (bedInfo["Odd"] != null
                                            && bedInfo["Odd"] is JArray)
                                        {
                                            for (int z = 0; z < bedInfo["Odd"].Count(); z++)
                                            {
                                                var oddInfo = sportData[i]["Event"][j]["Match"][k]["Bet"][l]["Odd"][z];
                                                Odd oddToAdd = new Odd();
                                                oddToAdd.Name = oddInfo["@Name"].ToString();
                                                oddToAdd.TempId = int.Parse(oddInfo["@ID"].ToString());
                                                oddToAdd.Value = oddInfo["@Value"].ToString();

                                                if (oddInfo["@SpecialBetValue"] != null)
                                                {
                                                    oddToAdd.SpecialBetValue = oddInfo["@SpecialBetValue"].ToString();
                                                }
                                                betToAdd.Odds.Add(oddToAdd);
                                            }
                                        }
                                        matchToAdd.Bets.Add(betToAdd);
                                    }
                                }
                                eventToAdd.Matches.Add(matchToAdd);
                            }
                        }
                        sportToAdd.Events.Add(eventToAdd);
                    }
                }
                sports.Add(sportToAdd);
            }
            return sports;
        }
        public void InitialRequest()
        {
            ICollection<Sport> initialSports = GetData();
            foreach (var sport in initialSports)
            {
                this.data.Sports.Add(sport);
            }

            this.data.SaveChanges();
        }
        public void UpdateDatabase()
        {
            ICollection<Sport> sportsFromUrl = GetData();
            IQueryable<Sport> sportsFromDB = this.data.Sports.All();
            IQueryable<Event> eventsFromDB = this.data.Events.All();
            IQueryable<Match> matchesFromDB = this.data.Matches.All();
            IQueryable<Bet> betsFromDB = this.data.Bets.All();
            IQueryable<Odd> oddsFromDB = this.data.Odds.All();

            foreach (Sport sport in sportsFromUrl)
            {
                Sport sportToModify = sportsFromDB.Where(m => m.TempId == sport.TempId).FirstOrDefault();

                if (sportToModify == null)
                {
                    this.data.Sports.Add(sport);
                }
                else
                {
                    foreach (Event sportEvent in sport.Events)
                    {
                        Event eventToModify = eventsFromDB.Where(e => e.TempId == sportEvent.TempId).FirstOrDefault();

                        if (eventToModify == null)
                        {
                            sportToModify.Events.Add(sportEvent);
                        }
                        else
                        {
                            eventToModify.IsLive = sportEvent.IsLive;

                            foreach (Match match in sportEvent.Matches)
                            {
                                Match matchToModify = matchesFromDB.Where(e => e.TempId == match.TempId).FirstOrDefault();

                                if (matchToModify == null)
                                {
                                    eventToModify.Matches.Add(match);
                                }
                                else
                                {
                                    matchToModify.StartDate = match.StartDate;
                                    matchToModify.MatchType = match.MatchType;

                                    foreach (Bet bet in match.Bets)
                                    {
                                        Bet betToModify = betsFromDB.Where(e => e.TempId == bet.TempId).FirstOrDefault();

                                        if (betToModify == null)
                                        {
                                            matchToModify.Bets.Add(bet);
                                        }
                                        else
                                        {
                                            betToModify.IsLive = bet.IsLive;

                                            foreach (Odd odd in bet.Odds)
                                            {
                                                Odd oddToModify = oddsFromDB.Where(e => e.TempId == odd.TempId).FirstOrDefault();

                                                if (oddToModify == null)
                                                {
                                                    betToModify.Odds.Add(odd);
                                                }
                                                else
                                                {
                                                    oddToModify.Value = odd.Value;
                                                    oddToModify.SpecialBetValue = odd.SpecialBetValue;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            this.data.SaveChanges();
        }

        public void ClearFinishedEventsAndMatches()
        {

        }
    }
}
