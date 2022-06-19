using Blockcore.Status.Entities.Indexer;
using BlockcoreStatus.ViewModels.Chains;
using BlockcoreStatus.ViewModels.Indexers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockcoreStatus.ViewModels.Home;
public class HomeViewModel
{
    public IReadOnlyList<string> Organizations { get; set; }
    public IReadOnlyList<ChainsViewModel> Chains { get; set; }
    public IReadOnlyList<BlockcoreIndexers> Indexers { get; set; }


}
