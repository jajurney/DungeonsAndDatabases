﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDatabases.Models.Campaign
{
    public class CampaignUpdate
    {
        public string CampaignName { get; set; }
        public string GameSystem { get; set; }
        public Guid DmGuid { get; set; }
    }
}