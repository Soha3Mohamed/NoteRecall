using System;
using System.Collections.Generic;
using System.Text;

namespace NoteRecall_Core.Common
{
    public class ServiceResult<T>
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get;}
        public T? Data { get; }
        public ServiceResult(bool success, T? data, string? errorMessage)
        {
            Success = success;
            ErrorMessage = errorMessage;
            Data = data;

        }

        public static ServiceResult<T> Ok(T data)
        {
            return new ServiceResult<T>(true, data, null);
        }
        public static ServiceResult<T> Fail(string errorMessage)
        {
            return new ServiceResult<T>(false, default, errorMessage);
        }
    }
}
