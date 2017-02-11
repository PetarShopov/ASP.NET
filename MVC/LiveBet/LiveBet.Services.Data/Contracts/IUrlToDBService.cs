using LiveBet.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveBet.Services.Data.Contracts
{
    public interface IUrlToDBService
    {
        ICollection<Sport> GetData();
    }
}
