using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQDemo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangePublisher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RabbitMQController : ControllerBase
    {
        private IMQClient _qClient;

        public RabbitMQController(IMQClient client)
        {
            _qClient = client;
        }

        /// <summary>
        /// Sends a message on the Q
        /// </summary>
        /// <param name="message">The message to send on the Q or Exchange</param>
        /// <returns>Returns a string to tell you if it is good or bad</returns>
        /// <response code="200">Returned if it was good</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<String> Send(String message)
        {
            string queueName = "testQ";
            _qClient.SendMessage(message);
            return new OkObjectResult(String.Format("Your message with content \"{0}\" has been sent.", message));
        }
    }
}