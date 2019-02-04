using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BasketTest.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BasketTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : Controller
    {
        private readonly IBasketRepository _basketRepository;
        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [HttpGet("GetBasket/{transactionNumber}")]
        public IActionResult GetBasket(Guid transactionNumber)
        {
            if (transactionNumber == null)
            {
                return BadRequest();
            }
            var basket = _basketRepository.GetBasket(transactionNumber);
            if (basket == null)
            {
                var message = ($"Basket with transactionNumber {transactionNumber} not found");
                return NotFound(message);
            }
            return Ok(basket);
        }


        [HttpGet]
        [Route("GetAll/{domain?}")]
        public IActionResult GetAll(int? domain=null)
        {
            var baskets = _basketRepository.GetAllBasket(domain).ToList();
            if(baskets == null)
            {
                var message = ($"No Basket found");
                return NotFound(message);
            }
            var res = baskets.Count();
            return Ok(baskets);
        }
    }
}