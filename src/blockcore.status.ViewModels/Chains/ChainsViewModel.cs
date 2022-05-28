using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blockcore.status.ViewModels.Chains;
 public class ChainsViewModel
{
    public string name { get; set; }
    public string symbol { get; set; }
    public string icon { get; set; }
    public bool? filter { get; set; }
}