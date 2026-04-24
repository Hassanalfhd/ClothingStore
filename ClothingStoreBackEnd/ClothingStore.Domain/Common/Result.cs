

namespace ClothingStore.Domain.Common
{
    public class Result
    {
        public bool IsSuccess { get; }
        public string Error { get; }
        public bool IsFailure => !IsSuccess;

        protected Result(bool isSuccess, string error)
        {
            if (isSuccess && error != null)
                throw new InvalidOperationException("Success result cannot have an error message.");
            if (!isSuccess && error == null)
                throw new InvalidOperationException("Failure result must have an error message.");

            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new(true, null);
        public static Result Failure(string error) => new(false, error);
    }

    public class Result<T> : Result
    {
        private readonly T _value;

        public T GetValueOrDefault() => IsSuccess ? _value : default!;

        protected Result(bool isSuccess, T value, string error)
            : base(isSuccess, error)
        {
            if(value != null)_value = value;
        }

        public static Result<T> Success(T value) => new(true, value, null);
        public static new Result<T> Failure(string error) => new(false, default, error);
    }


}
