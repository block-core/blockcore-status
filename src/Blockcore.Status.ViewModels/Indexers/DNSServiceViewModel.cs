﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockcore.Status.ViewModels.Indexers;
public class DNSServiceViewModel
{
    [JsonProperty("url")]
    public string Url { get; set; }
}