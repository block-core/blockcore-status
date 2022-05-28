using blockcore.status.ViewModels.Chains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blockcore.status.ViewModels.Home;
public class HomeViewModel
{
    public IList<string> Organizations { get; set; }
    public IList<ChainsViewModel> Chains { get; set; }

}
