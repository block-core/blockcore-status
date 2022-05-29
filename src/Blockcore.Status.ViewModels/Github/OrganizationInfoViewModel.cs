using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockcoreStatus.ViewModels.Github;
 public class OrganizationInfoViewModel
{
    public string Name { get; set; }
  
    public string Description { get; set; }
   
    public string Blog { get; set; }

    public string AvatarUrl { get; set; }

    public string Apiurl { get; set; }

    public string Login { get; set; }

    public List<RepositoryInfoViewModel> Repositories { get; set; }
}


