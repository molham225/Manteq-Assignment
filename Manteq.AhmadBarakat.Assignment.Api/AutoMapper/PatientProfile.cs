using Api.Enums;
using Api.Model.Entities;
using Api.Model.Models;
using Api.Model.ViewModels;
using AutoMapper;
using System;

namespace Api.AutoMapper
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<Patient, PatientRecordViewModel>()
                .ForMember(destination => destination.Gender,
                 opt => opt.MapFrom(source => (int)source.Gender));
            CreateMap<PatientDto, Patient>()
                .ForMember(destination => destination.Gender,
                 opt => opt.MapFrom(source => Enum.GetName(typeof(GenderEnum), source.Gender)));
            CreateMap<Patient, PatientViewModel>()
                .ForMember(destination => destination.Gender,
                 opt => opt.MapFrom(source => (int)source.Gender));
        }
    }
}
