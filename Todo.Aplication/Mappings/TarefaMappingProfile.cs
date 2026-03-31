using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Aplication.DTOs;
using Todo.Domain.Entities;

namespace Todo.Aplication.Mappings
{
    public class TarefaMappingProfile : Profile
    {
        public TarefaMappingProfile()
        {
            CreateMap<Tarefa, TarefaDto>().ReverseMap();
            CreateMap<CreateTarefaDto, Tarefa>();
            CreateMap<UpdateTarefaDto, Tarefa>();
        }
    }
}
