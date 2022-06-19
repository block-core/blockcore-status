using Blockcore.Status.Entities.Indexer;
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
    Task<IndexerLocationViewModel> GetIndexerLocation(string indexerUrl);
    Task<List<IndexersViewModel>> GetIndexers(int page = 1, int pageSize = 15);

    //DB
    Task AddOrUpdateIndexerToDB();
    Task<List<BlockcoreIndexers>> GetAllIndexerFromDB();
    Task<List<BlockcoreIndexers>> GetIndexerFromDB(int page = 1, int pageSize = 50);


}
