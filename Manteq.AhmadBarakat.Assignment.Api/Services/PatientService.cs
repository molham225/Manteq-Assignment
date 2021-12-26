using Api.Enums;
using Api.Model.Common;
using Api.Model.Entities;
using Api.Model.Models;
using Api.Model.ViewModels;
using Api.Persistense.UnitOfWork;
using Api.Services.Interfaces;
using Api.Validators;
using AutoMapper;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services
{
    public class PatientService : IPatientService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public PatientService(IUnitOfWork uow, IMapper mapper) 
        {
            this._uow = uow;
            this._mapper = mapper;
        }
        public async Task<ResultModel<List<PatientRecordViewModel>>> GetPatientsAsync(PaginationSortAndSearchInfo paginationSortAndSearchInfo)
        {
            List<Patient> patients = await _uow.Patients.GetSearchResultPagedAsync(paginationSortAndSearchInfo);

            List<PatientRecordViewModel> result = _mapper.Map<List<PatientRecordViewModel>>(patients);

            return ResultModel<List<PatientRecordViewModel>>.GetSuccessResult(result);
        }

        public async Task<ResultModel<Guid>> AddPatientAsync(PatientDto patient)
        {
            PatientDtoValidator validator = new PatientDtoValidator();
            ValidationResult validationResult = validator.Validate(patient);
            if (!validationResult.IsValid) 
            {
                return ResultModel<Guid>.GetFailureResult(validationResult.Errors);
            }

            Patient entity = _mapper.Map<Patient>(patient);
            entity.RecordCreationDate = DateTime.Now;
            _uow.Patients.Add(entity);
            await _uow.SaveAsync();

            return ResultModel<Guid>.GetSuccessResult(entity.Id);
        }

        public async Task<ResultModel<bool>> UpdatePatientAsync(Guid id, PatientDto patient) 
        {
            Patient entity = _uow.Patients.GetById(id);
            if (entity == null)
            {
                return ResultModel<bool>.GetFailureResult(ResultErrorTypeEnum.NotFoundError);
            }

            PatientDtoValidator validator = new PatientDtoValidator();
            ValidationResult validationResult = validator.Validate(patient);
            if (!validationResult.IsValid)
            {
                return ResultModel<bool>.GetFailureResult(validationResult.Errors);
            }

            _mapper.Map(patient, entity);
            entity.RecordCreationDate = DateTime.Now;
            _uow.Patients.Update(entity);
            await _uow.SaveAsync();

            return ResultModel<bool>.GetSuccessResult(true);
        }

        public async Task<ResultModel<bool>> DeletePatientAsync(Guid id)
        {
            Patient patient = _uow.Patients.GetById(id);

            if (patient == null)
            {
                return ResultModel<bool>.GetFailureResult(ResultErrorTypeEnum.NotFoundError);
            }

            _uow.Patients.Remove(patient);
            await _uow.SaveAsync();

            return ResultModel<bool>.GetSuccessResult(true);
        }

        public async Task<ResultModel<PatientViewModel>> GetPatientByIdAsync(Guid id)
        {
            Patient patient = await _uow.Patients.GetByIdAsync(id);

            if (patient == null)
            {
                return ResultModel<PatientViewModel>.GetFailureResult(ResultErrorTypeEnum.NotFoundError);
            }

            PatientViewModel model = _mapper.Map<PatientViewModel>(patient);

            return ResultModel<PatientViewModel>.GetSuccessResult(model);
        }
    }
}
