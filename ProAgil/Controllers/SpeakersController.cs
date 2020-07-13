using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeakersController : ControllerBase
    {
        private readonly IProAgilRepository _repository;

        public SpeakersController(IProAgilRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Speakers
        [HttpGet("getByName/{name}")]
        public async Task<ActionResult> GetSpeakersByName(string name)
        {
            try
            {
                var result = await _repository.GetAllSpeakersByNameAsync(name, true);
                return Ok(result);
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
        }

        // GET: api/Speakers/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetSpeaker(int id)
        {
            

            try
            {
                var speaker = await _repository.GetSpeakerAsync(id, true);
                return Ok(speaker);

            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }


           
        }

        // PUT: api/Speakers/5
        // PUT: api/Events/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpeaker(int id, Speaker model)
        {
            try
            {
                var result = await _repository.GetSpeakerAsync(id, false);
                if (result == null)
                {
                    return NotFound();
                }
                _repository.Update(model);
                if (await _repository.SaveChangesAsync())
                {
                    Created($"api/speaker/{model.Id}", model);
                }

            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }



            return BadRequest();
        }

        // POST: api/Speakers
        [HttpPost]
        public async Task<ActionResult> PostSpeaker(Speaker speaker)
        {
            try
            {
                _repository.Add(speaker);
                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/speaker/{speaker.Id}", speaker);
                }

            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }



            return BadRequest();
        }

        // DELETE: api/Speakers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSpeaker(int id)
        {
            try
            {
                var result = await _repository.GetSpeakerAsync(id, false);
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

        private async Task<bool> SpeakerExists(int id)
        {
            return (await _repository.GetAllSpeakersByNameAsync("", false)).ToList().Any(e => e.Id == id);
        }
    }
}
