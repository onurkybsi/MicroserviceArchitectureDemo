namespace ApiGateway.Services.Validator
{
    public interface IValidator<T> where T : class, new()
    {
        bool Validate(T obj);
    }
}