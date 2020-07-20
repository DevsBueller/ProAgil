
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProAgil.Dtos
{
	public class EventDto
	{
		public int Id { get; set; }
		[Required(ErrorMessage ="Campo Obrigatório")]
		[StringLength(100, MinimumLength =3, ErrorMessage ="Local é entre 3 e 100 caracteres")]
		public string Local { get; set; }
		public DateTime EventDate { get; set; }
		[Required (ErrorMessage ="O tema é de preenchimento obrigatório")]
		public string Theme { get; set; }
		[Range(5, 120000, ErrorMessage ="A quantidade de pessoas deve ser entre 5 e 120.000")]
		public int QtPeoples { get; set; }
		public string ImagemUrl { get; set; }
		[Phone]
		public string Tel { get; set; }
		[EmailAddress]
		public string Email { get; set; }
		public List<LotDto> Lots { get; set; }
		public List<SocialNetworkDto> SocialNetworks { get; set; }
		public List<SpeakerDto> Speakers { get; set; }
	}
}
