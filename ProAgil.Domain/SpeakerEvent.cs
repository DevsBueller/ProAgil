using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ProAgil.Domain
{
	public class SpeakerEvent
	{
		public int SpeakerId { get; set; }
		public Speaker Speaker { get; set; }
		public Event Event { get; set; }
		public int EventId { get; set; }
	}
	// PaltestranteId === EventoId
	//	1						1
	//	1						2
	//	1						3
	//	2						1
}
