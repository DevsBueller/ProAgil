using AutoMapper;
using ProAgil.Domain;
using ProAgil.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProAgil.Helpers
{
	public class AutoMapperProfiles : Profile
	{
		public AutoMapperProfiles()
		{
			CreateMap<Event, EventDto>()
				.ForMember(dest => dest.Speakers, opt =>
				{
					opt.MapFrom(src => src.SpeakersEvents.Select(x => x.Speaker).ToList());
				}).ReverseMap();
		
			CreateMap<Speaker, SpeakerDto>()
				.ForMember(dest => dest.Events, opt =>
				{
					opt.MapFrom(src => src.SpeakersEvents.Select(x => x.Event).ToList());
				}).ReverseMap();
			CreateMap<Lot, LotDto>().ReverseMap();
	
			CreateMap<SocialNetwork, SocialNetworkDto>().ReverseMap();
		

		}
	}
}
