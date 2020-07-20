using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProAgil.Dtos
{
	public class LotDto
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; } 
		[Required]
		public decimal Price { get; set; }
		public DateTime BeginDate { get; set; }
		public DateTime FinalDate { get; set; }
		[Range(2, 120000)]
		public int Quantity { get; set; }

	}
}

