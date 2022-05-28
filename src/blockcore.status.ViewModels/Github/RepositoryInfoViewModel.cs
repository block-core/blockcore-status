﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blockcore.status.ViewModels.Github;
 public class RepositoryInfoViewModel
{
    public string Name { get; set; }
    public string LastVersion { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime PushedAt { get; set; }
    public string RepositoryURL { get; set; }
    public int OpenIssuesCount { get; set; }
    public bool IsPreRelease { get; set; } 

}


