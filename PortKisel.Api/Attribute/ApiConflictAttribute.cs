using Microsoft.AspNetCore.Mvc;
using PortKisel.Api.Models.Exceptions;

namespace PortKisel.Api.Attribute
{
    /// <summary>
    /// Фильтр, который определяет тип значения и код состояния 409, возвращаемый действием
    /// </summary>
    public class ApiConflictAttribute : ProducesResponseTypeAttribute
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ApiConflictAttribute"/>
        /// </summary>
        public ApiConflictAttribute() : this(typeof(ApiValidationExceptionDetail))
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ApiConflictAttribute"/> со значением поля <see cref="Type"/>
        /// </summary>
        public ApiConflictAttribute(Type type)
            : base(type, StatusCodes.Status409Conflict)
        {
        }
    }
}
