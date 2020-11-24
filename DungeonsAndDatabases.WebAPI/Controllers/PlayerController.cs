﻿using DungeonsAndDatabases.Models.PlayerModels;
using DungeonsAndDatabases.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace DungeonsAndDatabases.WebAPI.Controllers
{
    public class PlayerController : ApiController
    {
        private PlayerService CreatePlayerService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var playerService = new PlayerService(userId);
            return playerService;
        }
        // Create 
        public async Task<IHttpActionResult> CreatePlayer(PlayerCreate player)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreatePlayerService();
            if (await service.CreatePlayer(player) == false)
                return InternalServerError();
            return Ok();
        }
        //Read
        [HttpGet]
        public async Task<IHttpActionResult> GetPlayers()
        {
            PlayerService playerService = CreatePlayerService();
            var players = await playerService.GetPlayers();
            return Ok(players);
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetPlayerByID(Guid id)
        {
            PlayerService playerService = CreatePlayerService();
            var player = await playerService.GetPlayerById(id);
            if (player == null)
                return NotFound();
            return Ok(player);
        }



        //Updated
        [HttpPut]
        public async Task<IHttpActionResult> UpdatePlayer([FromUri] Guid id, [FromBody] PlayerEdit player)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreatePlayerService();
            var result = await service.UpdatePlayer(id, player);
            if (result == false)
                return InternalServerError();
            return Ok();
        }
        [HttpDelete]
        public async Task<IHttpActionResult> DeletePlayer(Guid id)
        {
            var service = CreatePlayerService();
            var campaign = await service.DeletePlayer(id);
            if (campaign == false)
                return InternalServerError();
            return Ok();
        }
    }
}
