using Newbee.Common.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.Common
{
    public class Result<T>
    {
        public bool IsSuccess {  get; set; }
        public T Data { get; set; }
        public IList<Error> Errors { get; set; }
        public string Message { get; set; }
        public static Result<T> Success(T data,string message)
        {
            return new Result<T> { IsSuccess = true, Data = data , Errors = null ,Message = message};
        }

        public static Result<T> Failure(List<Error> errors)
        {
            return new Result<T> { IsSuccess = false, Data = default, Errors = errors, Message = null};
        }
    }
}
