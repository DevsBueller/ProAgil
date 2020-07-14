using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProAgil.Dtos
{
	public class SocialNetworkDto
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "O campo {0} é obrigatório")]
		public string Name { get; set; }
		[Required(ErrorMessage ="O campo {0} é obrigatório")]
		public string URL { get; set; }
		
	
	
	}
}
