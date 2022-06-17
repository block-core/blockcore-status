using BlockcoreStatus.ViewModels.Indexers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockcoreStatus.Services.Contracts;
public interface IBlockcoreIndexersService
{
    Task<bool> PingIndexer(string indexerUrl);
    Task<List<IndexersViewModel>> GetAllIndexers();

}
