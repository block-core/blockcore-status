using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockcoreStatus.ViewModels.Indexers;
public class IndexersViewModel
{
    public string Url { get; set; }
    public bool IsActive { get; set; }
    public IndexerLocationViewModel Location { get; set; }
}
