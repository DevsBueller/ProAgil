﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProAgil.Model
{
	public class Event
	{
		public int EventId { get; set; }
		public string Local { get; set; }
		public string EventDate { get; set; }
		public string Theme { get; set; }
		public int QtPeoples { get; set; }
		public string Lot { get; set; }

	}
}