﻿using Microsoft.AspNetCore.Mvc;
using Services;
using DTO;
using Entities;
using AutoMapper;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IOrderService orderService;
        IMapper mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            this.orderService = orderService;
            this.mapper = mapper;
        }

        // GET: api/<OrderController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public async Task<OrderDTO> Get(int id)
        {
            Order order= await orderService.GetOrderById(id);
            OrderDTO orderDTO = mapper.Map<Order, OrderDTO>(order);

            return orderDTO;
        }

        // POST api/<OrderController>
        [HttpPost]
        public async Task<ActionResult<OrderDTO>> Post([FromBody] OrderPostDTO OrderPostDTO)
        {
            Order newOrder = mapper.Map<OrderPostDTO,Order>(OrderPostDTO);
            var a = await orderService.CreateOrder(newOrder);
            OrderDTO order = mapper.Map<Order, OrderDTO>(a);
            if (a != null)
                return CreatedAtAction(nameof(Get), new { id = newOrder.OrderId }, order);
            return BadRequest();

        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
