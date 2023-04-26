using System;
using AutoMapper;
using German.Core.Entities;
using German.Core.DTOs;
namespace German.API
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Author, Authordto>();
			CreateMap<Authordto, Author>();
			CreateMap<Author, AuthorProfileDto>();
			CreateMap<AuthorProfileDto, Author>();
		}
	}
}

