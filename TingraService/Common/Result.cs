﻿namespace TingraService.Common
{
    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Error Error { get; }

        protected Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != null)
                throw new ArgumentException("Success result cannot have an error.");
            if (!isSuccess && error == null) 
                throw new ArgumentException("Failure result must have an error.");

            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new Result(true, null);
        public static Result Failure(Error error) => new Result(false, error);
        public static Result<T> Success<T>(T value) => new Result<T>(value, true, null);
        public static Result<T> Failure<T>(Error error) => new Result<T>(default, false, error);

    }

    public class Result<T> : Result
    {
        public T Value { get; }

        internal Result(T value, bool isSuccess, Error error) : base(isSuccess, error)
        {
            Value = value;
        }
    }
}
