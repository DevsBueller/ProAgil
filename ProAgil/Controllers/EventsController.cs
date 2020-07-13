using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EventsController : ControllerBase
	{
		private readonly IProAgilRepository _repository;

		public EventsController(IProAgilRepository repository)
		{
			_repository = repository;
		}

		// GET: api/Events
		[HttpGet]
		public async Task<ActionResult> GetEvents()
		{
			try
			{
				var result = await _repository.GetAllEventAsync(true);
				return Ok(result);
			}
			catch (Exception)
			{

				return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
			}

		}

		// GET: api/Events/5
		[HttpGet("{id}")]
		public async Task<ActionResult> GetEvent(int id)
		{
			try
			{
				var result = await _repository.GetEventAsyncById(id, true);
				return Ok(result);
			}
			catch (Exception)
			{

				return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
			}
		}
		// GET: api/Events/5
		[HttpGet("getByTheme/{theme}")]
		public async Task<ActionResult> GetEventsByTheme(string theme)
		{
			try
			{
				var result = await _repository.GetAllEventAsyncByTema(theme, true);
				return Ok(result);
			}
			catch (Exception)
			{

				return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
			}
		}

		// PUT: api/Events/5
		[HttpPut("{EventId}")]
		public async Task<IActionResult> PutEvent(int EventId, Event model)
		{
			try
			{
				var result = await _repository.GetEventAsyncById(EventId, false);
				if (result == null)
				{
					return NotFound();
				}
				_repository.Update(model);
				if (await _repository.SaveChangesAsync())
				{
					return Created($"api/events/{model.Id}", model);

				}

			}
			catch (System.Exception)
			{

				return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
			}



			return BadRequest();
		}

		// POST: api/Events
		[HttpPost]
		public async Task<ActionResult> PostEvent(Event model)
		{
			try
			{
				_repository.Add(model);
				if (await _repository.SaveChangesAsync())
				{
					return Created($"api/evento/{model.Id}", model);
				}

			}
			catch (System.Exception)
			{

				return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
			}



			return BadRequest();
		}

		// DELETE: api/Events/5
		[HttpDelete("{Eventid}")]
		public async Task<ActionResult<Event>> DeleteEvent(int id)
		{
			try
			{
				var result = await _repository.GetEventAsyncById(id, false);
				if (result == null)
				{
					return NotFound();
				}
				_repository.Delete(result);
				if (await _repository.SaveChangesAsync())
				{
					return Ok();
				}

			}
			catch (System.Exception)
			{

				return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
			}



			return BadRequest();
		}

		private async Task<bool> EventExists(int id)
		{
			return (await _repository.GetAllEventAsync(false)).ToList().Any(e => e.Id == id);
			
		}
	}
}
