﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProAgil.Domain;
using ProAgil.Dtos;
using ProAgil.Repository;

namespace ProAgil.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EventsController : ControllerBase
	{
		private readonly IProAgilRepository _repository;

		public IMapper _mapper { get; }

		public EventsController(IProAgilRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		// GET: api/Events
		[HttpGet]
		public async Task<ActionResult> GetEvents()
		{
			try
			{
				var events = await _repository.GetAllEventAsync(true);
				var results = _mapper.Map<EventDto[]>(events);
				return Ok(results);
			}
			catch (Exception ex)
			{

				return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco Dados Falhou {ex.Message}");
			}

		}
		[HttpPost("upload")]
		public async Task<IActionResult> Upload()
		{
			try
			{
				var file = Request.Form.Files[0];
				var folderName = Path.Combine("Resources", "Images");
				var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

				if (file.Length > 0)
				{
					var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;
					var fullPath = Path.Combine(pathToSave, filename.Replace("\"", " ").Trim());

					using (var stream = new FileStream(fullPath, FileMode.Create))
					{
						file.CopyTo(stream);
					}
				}

				return Ok();
			}
			catch (System.Exception ex)
			{
				return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco Dados Falhou {ex.Message}");
			}

			return BadRequest("Erro ao tentar realizar upload");
		}

		// GET: api/Events/5
		[HttpGet("{EventId}")]
		public async Task<ActionResult> GetEvent(int EventId)
		{
			try
			{

				var Event = await _repository.GetEventAsyncById(EventId, true);
				var result = _mapper.Map<EventDto>(Event);


				return Ok(result);
			}
			catch (Exception)
			{

				return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
			}
		}
		// GET: api/Events/5u
		[HttpGet("getByTheme/{theme}")]
		public async Task<ActionResult> GetEventsByTheme(string theme)
		{
			try
			{
				var events = await _repository.GetAllEventAsyncByTema(theme, true);
				var results = _mapper.Map<EventDto[]>(events);
				return Ok(results);
			}
			catch (Exception)
			{

				return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
			}
		}

		// PUT: api/Events/5
		[HttpPut("{EventId}")]
		public async Task<IActionResult> Put(int EventId, EventDto model)
		{
			try
			{

				var eventModel = await _repository.GetEventAsyncById(EventId, false);
				if (eventModel == null)
				{
					return NotFound();
				}
				var idLots = new List<int>();
				var idSocialNetWorks = new List<int>();

				model.Lots.ForEach(item => idLots.Add(item.Id));
				model.SocialNetworks.ForEach(item => idSocialNetWorks.Add(item.Id));

				var lots = eventModel.Lots.Where(
				   lot => !idLots.Contains(lot.Id)
			   ).ToArray();

				var SocialNetWorks = eventModel.SocialNetworks.Where(
			  network => !idSocialNetWorks.Contains(network.Id)
				).ToArray();

				if (lots.Length > 0) _repository.DeleteRange(lots);
				if (SocialNetWorks.Length > 0) _repository.DeleteRange(SocialNetWorks);

				_mapper.Map(model, eventModel);
				_repository.Update(eventModel);

				if (await _repository.SaveChangesAsync())
				{
					return Created($"api/events/{model.Id}", _mapper.Map<EventDto>(eventModel));

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
		public async Task<ActionResult> PostEvent(EventDto model)
		{
			try
			{
				var eventModel = _mapper.Map<Event>(model);
				_repository.Add(eventModel);
				if (await _repository.SaveChangesAsync())
				{
					return Created($"api/evento/{model.Id}", _mapper.Map<EventDto>(eventModel));
				}

			}
			catch (System.Exception ex)
			{

				return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco Dados Falhou {ex.Message}");
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
