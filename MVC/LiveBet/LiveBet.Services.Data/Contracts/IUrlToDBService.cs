namespace LiveBet.Services.Data.Contracts
{
    using LiveBet.Data.Models;
    using System.Collections.Generic;
    public interface IUrlToDBService
    {
        ICollection<Sport> GetData();
        void InitialRequest();
        void UpdateDatabase();
        void ClearFinishedEventsAndMatches();
    }
}
