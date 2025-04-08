using UserService.Application.Wrappers;

namespace UserService.Application.Common
{
    public class BaseService
    {
        protected async Task<OperationResult<T>> HandleRequestAsync<T>(Func<Task<T>> action, string? successMessage = null)
        {
            try
            {
                var result = await action();
                return OperationResult<T>.Ok(result, successMessage);
            }
            catch (Exception ex)
            {
                return OperationResult<T>.Fail($"{ex.Message}");
            }
        }

        protected async Task<OperationResult<object?>> HandleRequestAsync(Func<Task> action, string? successMessage = null)
        {
            try
            {
                await action();
                return OperationResult<object?>.Ok(null, successMessage);
            }
            catch (Exception ex)
            {
                return OperationResult<object?>.Fail($"{ex.Message}");
            }
        }
    }
}
