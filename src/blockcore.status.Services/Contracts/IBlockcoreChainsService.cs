using blockcore.status.ViewModels.Chains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blockcore.status.Services.Contracts;
public interface IBlockcoreChainsService
{
    Task<IReadOnlyList<ChainsViewModel>> GetAllChains();
}
