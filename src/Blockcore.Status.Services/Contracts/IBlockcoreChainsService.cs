using BlockcoreStatus.ViewModels.Chains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockcoreStatus.Services.Contracts;
public interface IBlockcoreChainsService
{
    Task<IReadOnlyList<ChainsViewModel>> GetAllChains();
}
