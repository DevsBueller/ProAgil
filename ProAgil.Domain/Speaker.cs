using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace ProAgil.Domain
{
	public class Speaker
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string MiniCurriculum{get; set;}
		public string ImageUrl { get; set; }
		public string Tel { get; set; }
		public string Email { get; set; }
		public List<SocialNetwork> SocialNetwork { get; set; }
		public List<SpeakerEvent> SpeakersEvents { get; set; }


	}
}
