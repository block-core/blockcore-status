﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockcoreStatus.ViewModels.Indexers;
public class IndexersViewModel
{
    public string Url { get; set; }
    public bool IsActive { get; set; }
    public string IpAddress { get; set; }
    public string Latitude  { get; set; }
    public string Longitude { get; set; }
    public string Country { get; set; }
    public string City { get; set; }


}