﻿using Application.Features.Clientes.Commands.CreateClienteCommand;
using Application.Features.Clientes.Commands.DeleteClienteCommand;
using Application.Features.Clientes.Commands.UpdateClienteCommand;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ClienteController : BaseApiController
    {
        //POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(CreateClienteCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        //PUT api/<controller>/#
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateClienteCommand command)
        {
            if (id != command.Id)
                return BadRequest();
            return Ok(await Mediator.Send(command));
        }

        //DELETE api/<controller>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteClienteCommand { Id = id }));
        }
    }
}