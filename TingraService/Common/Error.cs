namespace TingraService.Common
{
    public class Error
    {
        public string Code { get; }
        public string Description { get; }

        public static readonly Error None = new Error(string.Empty, string.Empty);
        public static readonly Error NullValue = new Error("Error.NullValue", "Null value was provided");

        public Error(string code, string description)
        {
            Code = code;
            Description = description;
        }

        public override string ToString() => $"{Code}: {Description}";
        public static implicit operator Result(Error error)
        {
            return Result.Failure(error);
        }

        public Result ToResult()
        {
            return Result.Failure(this);
        }
    }
}
