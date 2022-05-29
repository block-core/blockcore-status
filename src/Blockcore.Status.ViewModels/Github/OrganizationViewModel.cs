using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockcoreStatus.ViewModels.Github;
public class OrganizationViewModel
{
    [HiddenInput]
    public int OrganizationId { get; set; }
    public string Login { get; set; }
}
