using PortKisel.Context.Contracts.Enums;

namespace PortKisel.Api.ModelsRequest.Staff
{
    public class CreateStaffRequest
    {
        /// <summary>
        /// ФИО
        /// </summary>
        public string FIO { get; set; } = string.Empty;

        /// <summary>
        /// Должность
        /// </summary>
        public Posts Post { get; set; } = Posts.None;
    }
}
