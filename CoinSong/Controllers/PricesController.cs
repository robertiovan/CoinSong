﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoinSong.Domain.Models;
using CoinSong.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Sulmo;

namespace CoinSong.Controllers
{
    [Route("api/[controller]")]
    public class PricesController : Controller
    {
        private readonly ICoinRepository _coinRepository;
        private readonly IPriceRepository _priceRepository;

        public PricesController(ICoinRepository coinRepository,
            IPriceRepository priceRepository)
        {
            _coinRepository = coinRepository;
            _priceRepository = priceRepository;
        }
        
        [HttpGet]
        public async Task<IEnumerable<SnapshotPrice>> Get()
        {
            var prices = await _coinRepository.GetPricesAsync();

            return prices;
        }

        [HttpGet("{id}")]
        public async Task<SnapshotPrice> Get(string id)
        {
            var price = await _coinRepository.GetPriceAsync(id.To<Coin>());
            return price;
        }

        // POST api/values
        [HttpPost]
        public async Task Post()
        {
            var prices = await _coinRepository.GetPricesAsync();
            foreach (var price in prices)
            {
                await _priceRepository.SaveAsync(price);
            }
        }
    }
}