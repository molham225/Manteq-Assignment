using Api.Enums;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Model.Common
{
    public class ResultModel<T>
    {
        public string Status { set; get; }
        public ResultStatusEnum ResultStatus { set; get; }
        public int Code { set; get; }
        public T Data { set; get; }

        public List<ResultErrorModel> Errors { set; get; }

        public static ResultModel<T> GetExceptionResult(ResultErrorModel error)
        {
            return new ResultModel<T>()
            {
                ResultStatus = ResultStatusEnum.ServerError,
                Errors = new List<ResultErrorModel>() { error }
            };
        }

        public static ResultModel<T> GetSuccessResult(T Data = default(T))
        {
            return new ResultModel<T>()
            {
                ResultStatus = ResultStatusEnum.Success,
                Data = Data
            };
        }

        public static ResultModel<T> GetFailureResult(List<ResultErrorModel> Errors = null)
        {
            return new ResultModel<T>()
            {
                ResultStatus = ResultStatusEnum.BadRequest,
                Errors = Errors
            };
        }

        public static ResultModel<T> GetFailureResult(List<ValidationFailure> Errors = null)
        {
            return new ResultModel<T>()
            {
                ResultStatus = ResultStatusEnum.BadRequest,
                Errors = Errors.Select(t=> new ResultErrorModel() { 
                    Message = t.ErrorMessage,
                    Name = t.PropertyName,
                }).ToList()
            };
        }

        public static ResultModel<T> GetFailureResult(ResultErrorModel Error = null)
        {
            return new ResultModel<T>()
            {
                ResultStatus = ResultStatusEnum.BadRequest,
                Errors = new List<ResultErrorModel>() { Error }
            };
        }

        public static ResultModel<T> GetFailureResult(ResultErrorTypeEnum errorType)
        {
            return new ResultModel<T>()
            {
                ResultStatus = ResultStatusEnum.BadRequest,
                Errors = new List<ResultErrorModel>() { new ResultErrorModel(errorType) }
            };
        }

    }
}
