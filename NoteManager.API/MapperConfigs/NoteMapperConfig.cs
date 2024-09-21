using AutoMapper;
using NoteManager.API.Entities;
using NoteManager.Shared;

namespace NoteManager.API.MapperConfigs
{
    public class NoteMapperConfig : Profile
    {
        public NoteMapperConfig()
        {
            CreateMap<Note, NoteReadDto>();
            CreateMap<NoteCreateDto, Note>();
            CreateMap<NoteUpdateDto, Note>();
        }
    }
}
