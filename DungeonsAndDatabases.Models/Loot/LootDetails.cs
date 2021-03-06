﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDatabases.Models.Loot
{
    public class LootDetails
    {
        public int LootID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double ValueInGP { get; set; }
        public int CampaignID { get; set; }
    }
}
