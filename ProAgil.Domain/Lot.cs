using System;
using System.Collections.Generic;
using System.Text;

namespace ProAgil.Domain
{
	public class Lot
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public DateTime? BeginDate { get; set; }
		public DateTime? FinalDate { get; set; }
		public int Quantity { get; set; }
		public int EventId { get; set; }
		public Event Event { get;}
	}
}
