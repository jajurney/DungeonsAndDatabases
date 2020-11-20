﻿using DungeonsAndDatabases.Data;
using DungeonsAndDatabases.Models.Campaign;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDatabases.Services
{
    public class CampaignService
    {
        //GUID
        private readonly Guid _userId;

        public CampaignService(Guid userId)
        {
            _userId = userId;
        }

        //Create a new Campaign
        public async Task<bool> CreateCampaign(CampaignCreate model)
        {
            var entity =
                new Campaign()
                {
                    CampaignName = model.CampaignName,
                    GameSystem = model.GameSystem,
                    DmGuid = model.DmGuid
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Campaigns.Add(entity);
                return await ctx.SaveChangesAsync() == 1;
            }
        }

        //Get all Campaigns
        public async Task<IEnumerable<CampaignListView>> GetCampaigns()
        {
            using (var ctx = new ApplicationDbContext())
            {
                 var query =
                    ctx
                        .Campaigns
                        .Select(
                            e =>
                                new CampaignListView
                                {
                                    CampaignID = e.CampaignID,
                                    CampaignName = e.CampaignName,
                                    GameSystem = e.GameSystem,
                                    DmGuid = e.DmGuid
                                }
                        );
                await query.ToListAsync();
                return query;
            }
            
        }
        //Get a specific Campaign by ID
        public async Task<CampaignDetail> GetCampaignById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = await ctx.Campaigns.FindAsync(id);
                    ctx
                        .Campaigns
                        .Single(e => e.CampaignID == id);
                return
                    new CampaignDetail
                    {
                        CampaignID = entity.CampaignID,
                        CampaignName = entity.CampaignName,
                        GameSystem = entity.GameSystem,
                        DmGuid = entity.DmGuid
                    };
            }
        }
        //Update a campaign 
        public async Task<bool> UpdateCampaign(int id,CampaignUpdate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Campaigns
                        .Single(e => e.CampaignID == id);
                entity.CampaignName = model.CampaignName;
                entity.GameSystem = model.GameSystem;
                entity.DmGuid = model.DmGuid;
                return await ctx.SaveChangesAsync() == 1;
            }
        }
        //Delete a campaign
        public async Task<bool> DeleteCampaign(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Campaigns
                        .Single(e => e.CampaignID == id);
                ctx.Campaigns.Remove(entity);

                return await ctx.SaveChangesAsync() == 1;
            }
        }
    }
}