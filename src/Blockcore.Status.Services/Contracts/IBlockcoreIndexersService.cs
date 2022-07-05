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
    Task<IndexerLocationViewModel> GetIndexerLocation(string indexerUrl);

    //DB
    Task AddOrUpdateIndexerToDB();
    Task<List<IndexersViewModel>> GetAllIndexerFromDB();



}
