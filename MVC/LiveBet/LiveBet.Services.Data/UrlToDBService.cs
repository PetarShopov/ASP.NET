using LiveBet.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using System.IO;
using LiveBet.Services.Data.Contracts;
using Newtonsoft.Json.Linq;
using System.Web;
using LiveBet.Data.Common.Contracts;

namespace LiveBet.Services.Data
{
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
            //var data = JObject.Parse(jsonText)["XmlSports"]["Sport"][0]["Event"][0]["Match"][0]["Bet"];
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
                        Event eventToAdd = new Event();
                        eventToAdd.Name = sportData[i]["Event"][j]["@Name"].ToString();
                        eventToAdd.TempId = int.Parse(sportData[i]["Event"][j]["@ID"].ToString());
                        eventToAdd.IsLive = Convert.ToBoolean(sportData[i]["Event"][j]["@IsLive"].ToString());
                        eventToAdd.CategoryID = int.Parse(sportData[i]["Event"][j]["@CategoryID"].ToString());
                        if (sportData[i]["Event"][j]["Match"] != null && sportData[i]["Event"][j]["Match"] is JArray)
                        {
                            for (int k = 0; k < sportData[i]["Event"][j]["Match"].Count(); k++)
                            {
                                Match matchToAdd = new Match();
                                matchToAdd.Name = sportData[i]["Event"][j]["Match"][k]["@Name"].ToString();
                                matchToAdd.TempId = int.Parse(sportData[i]["Event"][j]["Match"][k]["@ID"].ToString());
                                matchToAdd.StartDate = Convert.ToDateTime(sportData[i]["Event"][j]["Match"][k]["@StartDate"].ToString());
                                matchToAdd.MatchType = sportData[i]["Event"][j]["Match"][k]["@MatchType"].ToString();
                                if (sportData[i]["Event"][j]["Match"][k]["Bet"] != null && sportData[i]["Event"][j]["Match"][k]["Bet"] is JArray)
                                {
                                    var typeOfBB = (sportData[i]["Event"][j]["Match"][k]["Bet"]).GetType();
                                    var badBedC = sportData[i]["Event"][j]["Match"][k]["Bet"].Count();
                                    for (int l = 0; l < sportData[i]["Event"][j]["Match"][k]["Bet"].Count(); l++)
                                    {
                                        var badBed = sportData[i]["Event"][j]["Match"][k];
                                        
                                        
                                        Bet betToAdd = new Bet();
                                        
                                            if (sportData[i]["Event"][j]["Match"][k]["Bet"].ElementAtOrDefault(l) != null)
                                            {
                                                betToAdd.Name = sportData[i]["Event"][j]["Match"][k]["Bet"][l]["@Name"].ToString();
                                            }
                                            betToAdd.TempId = int.Parse(sportData[i]["Event"][j]["Match"][k]["Bet"][l]["@ID"].ToString());
                                            betToAdd.IsLive = Convert.ToBoolean(sportData[i]["Event"][j]["Match"][k]["Bet"][l]["@IsLive"].ToString());
                                            if (sportData[i]["Event"][j]["Match"][k]["Bet"][l]["Odd"] != null
                                                && sportData[i]["Event"][j]["Match"][k]["Bet"][l]["Odd"] is JArray)
                                            {
                                                for (int z = 0; z < sportData[i]["Event"][j]["Match"][k]["Bet"][l]["Odd"].Count(); z++)
                                                {
                                                    Odd oddToAdd = new Odd();
                                                    oddToAdd.Name = sportData[i]["Event"][j]["Match"][k]["Bet"][l]["Odd"][z]["@Name"].ToString();
                                                    oddToAdd.TempId = int.Parse(sportData[i]["Event"][j]["Match"][k]["Bet"][l]["Odd"][z]["@ID"].ToString());
                                                    oddToAdd.Value = sportData[i]["Event"][j]["Match"][k]["Bet"][l]["Odd"][z]["@Value"].ToString();
                                                    if (sportData[i]["Event"][j]["Match"][k]["Bet"][l]["Odd"][z]["@SpecialBetValue"] != null)
                                                    {
                                                        oddToAdd.SpecialBetValue = sportData[i]["Event"][j]["Match"][k]["Bet"][l]["Odd"][z]["@SpecialBetValue"].ToString();
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
            //File.WriteAllText("d:\\json1.xml", sportData[0].ToString());

            //Save in Database
            foreach (var sp in sports)
            {
                this.data.Sports.Add(sp);
            }

            this.data.SaveChanges();

            //just to return
            return sports;
        }
    }
}
