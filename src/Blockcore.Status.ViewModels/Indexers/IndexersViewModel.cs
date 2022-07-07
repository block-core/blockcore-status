using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockcoreStatus.ViewModels.Indexers;

public class IndexersViewModel
{
    public string NameServer { get; set; }
    public List<BlockcoreIndexersViewModel> Indexers { get; set; }

}
