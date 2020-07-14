
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProAgil.Dtos
{
	public class SpeakerDto
	{
		public int Id { get; set; }
		[Required(ErrorMessage ="O nome do Palestrante é obrigatório")]
		public string Name { get; set; }
		public string MiniCurriculum { get; set; }
		public string ImageUrl { get; set; }
		public string Tel { get; set; }
		public string Email { get; set; }
		public List<SocialNetworkDto> SocialNetwork { get; set; }
		public List<EventDto> Events { get; set; }

	}
}
