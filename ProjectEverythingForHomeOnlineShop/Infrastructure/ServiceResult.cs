namespace ProjectEverythingForHomeOnlineShop.Infrastructure
{
    public class ServiceResult
    {
        public bool Success { get; set; } 
        public string Message { get; set; } 

        
        public static ServiceResult SuccessResult(string message = "Operation successful")
        {
            return new ServiceResult
            {
                Success = true,
                Message = message,
            };
        }

        public static ServiceResult ErrorResult(string message)
        {
            return new ServiceResult
            {
                Success = false,
                Message = message,
            };
        }
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T Data { get; set; }

        public static ServiceResult<T> SuccessResult(T data, string message = "Operation successful")
        {
            return new ServiceResult<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static ServiceResult<T> SuccessResult(string message = "Operation successful")
        {
            return new ServiceResult<T>
            {
                Success = true,
                Message = message,
                Data = default
            };
        }

        public static new ServiceResult<T> ErrorResult(string message)
        {
            return new ServiceResult<T>
            {
                Success = false,
                Message = message,
                Data = default
            };
        }

        public static ServiceResult<T> ErrorResult(T data, string message)
        {
            return new ServiceResult<T>
            {
                Success = false,
                Message = message,
                Data = data
            };
        }
    }
}
