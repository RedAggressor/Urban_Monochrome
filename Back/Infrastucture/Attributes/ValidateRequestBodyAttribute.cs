using Infrastucture.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Infrastucture.Attributes
{
    public class ValidateRequestBodyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var request = context.HttpContext.Request;

            if (request.Body == null || request.ContentLength == 0)
            {
                context.Result = new OkObjectResult(new BaseResponse
                {
                    ErrorMessage = "Request body is null or empty!"
                });
                return;
            }

            var body = context.ActionArguments.Values.FirstOrDefault();
            if (body == null)
            {
                context.Result = new OkObjectResult(new BaseResponse
                {
                    ErrorMessage = "Request body cannot be null"
                });
                return;
            }

            var (hasNull, errorMessage) = HasNullProperties(body);
            if (hasNull)
            {
                context.Result = new OkObjectResult(new BaseResponse
                {
                    ErrorMessage = errorMessage
                });
                return;
            }

            base.OnActionExecuting(context);
        }

        private (bool HasNull, string ErrorMessage) HasNullProperties(object obj, string parentName = "")
        {
            foreach (var property in obj.GetType().GetProperties())
            {
                try
                {
                    if (property.GetIndexParameters().Length > 0)
                    {
                        continue;
                    }

                    var value = property.GetValue(obj, null);
                    var isRequired = property.GetCustomAttributes(typeof(RequiredAttribute), false).Length > 0;

                    if (isRequired && value == null)
                    {
                        return (true, $"Field {parentName}{property.Name} cannot be null or empty");
                    }

                    if (value != null)
                    {
                        if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType) && property.PropertyType != typeof(string))
                        {
                            foreach (var item in (IEnumerable)value)
                            {
                                if (item == null)
                                {
                                    return (true, $"Field {parentName}{property.Name}[] cannot contain null elements");
                                }

                                var (hasNull, errorMessage) = HasNullProperties(item, parentName + property.Name + "[]");
                                if (hasNull)
                                {
                                    return (true, errorMessage);
                                }
                            }
                        }
                        else if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                        {
                            var (hasNull, errorMessage) = HasNullProperties(value, parentName + property.Name + ".");
                            if (hasNull)
                            {
                                return (true, errorMessage);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Помилка у властивості {parentName}{property.Name}: {ex.Message}", ex);
                }
            }
                return (false, string.Empty);
            
        }
    }
}
