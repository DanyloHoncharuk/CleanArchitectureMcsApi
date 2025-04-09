using UserService.Application.Wrappers;
using UserService.Common;

namespace UserService.Application.Common
{
    public class BaseService
    {
        protected async Task<OperationResult<T>> HandleRequestAsync<T>(Func<Task<T>> action, string? successMessage = null)
        {
            try
            {
                var result = await action();
                return OperationResult<T>.SuccessResult(result, successMessage);
            }
            catch (ArgumentException ex)
            {
                return OperationResult<T>.Failture(ex.Message, ErrorCodes.Validation);
            }
            catch (KeyNotFoundException ex)
            {
                return OperationResult<T>.Failture(ex.Message, ErrorCodes.NotFound);
            }
        }

        protected async Task<OperationResult<object?>> HandleRequestAsync(Func<Task> action, string? successMessage = null)
        {
            try
            {
                await action();
                return OperationResult<object?>.SuccessResult(null, successMessage);
            }
            catch (ArgumentException ex)
            {
                return OperationResult<object?>.Failture(ex.Message, ErrorCodes.Validation);
            }
            catch (KeyNotFoundException ex)
            {
                return OperationResult<object?>.Failture(ex.Message, ErrorCodes.NotFound);
            }
        }
    }
}
