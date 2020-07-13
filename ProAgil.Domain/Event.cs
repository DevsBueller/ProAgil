using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProAgil.Domain
{
	public class Event
	{
		public int Id { get; set; }
		public string Local { get; set; }
		public DateTime EventDate { get; set; }
		public string Theme { get; set; }
		public int QtPeoples { get; set; }		
		public string ImagemUrl { get; set; }
		public string Tel { get; set; }
		public string Email { get; set; }
		public List<Lot> Lots { get; set; }
		public List<SocialNetwork> SocialNetworks { get; set; }
		public List<SpeakerEvent> SpeakersEvents { get; set; }

	}
}
