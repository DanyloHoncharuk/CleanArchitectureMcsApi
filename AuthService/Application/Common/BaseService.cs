using AuthService.Application.Wrappers;
using AuthService.Common;
using AuthService.Application.Exceptions;

namespace AuthService.Application.Common
{
    public abstract class BaseService
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
                return OperationResult<T>.Failure(ex.Message, ErrorCodes.Validation);
            }
            catch (KeyNotFoundException ex)
            {
                return OperationResult<T>.Failure(ex.Message, ErrorCodes.NotFound);
            }
            catch (AuthenticationException ex)
            {
                return OperationResult<T>.Failure(ex.Message, ErrorCodes.AuthenticationFailed);
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
                return OperationResult<object?>.Failure(ex.Message, ErrorCodes.Validation);
            }
            catch (KeyNotFoundException ex)
            {
                return OperationResult<object?>.Failure(ex.Message, ErrorCodes.NotFound);
            }
            catch (AuthenticationException ex)
            {
                return OperationResult<object?>.Failure(ex.Message, ErrorCodes.AuthenticationFailed);
            }
        }
    }
}
