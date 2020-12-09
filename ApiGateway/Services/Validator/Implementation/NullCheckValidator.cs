using System.Collections.Generic;

namespace ApiGateway.Services.Validator
{
    public class NullCheckValidator<T> : IValidator<T> where T : class, new()
    {
        public bool Validate(T obj)
            => !EqualityComparer<T>.Default.Equals(obj, default(T));
    }
}