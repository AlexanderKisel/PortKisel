using PortKisel.Api.Models.Enums;

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
        public PostApi Post { get; set; }
    }
}
