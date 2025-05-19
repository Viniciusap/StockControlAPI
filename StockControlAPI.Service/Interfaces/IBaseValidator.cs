using FluentResults;
namespace StockControlAPI.Service.Interfaces
{
    public interface IBaseValidator<T>
    {
        Result Validate(T entity);
    }
}