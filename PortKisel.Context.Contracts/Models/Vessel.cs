﻿namespace PortKisel.Context.Contracts.Models
{
    public class Vessel : BaseAuditEntity
    {
        /// <summary>
        /// Название судна
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Компания перевозчик
        /// </summary>
        public Guid CompanyPerId { get; set; }

        /// <summary>
        /// Компания перевозчик
        /// </summary>
        public CompanyPer? CompanyPer { get; set; }

        /// <summary>
        /// Грузоподъемность
        /// </summary>
        public string LoadCapacity { get; set; } = string.Empty;

        /// <summary>
        /// Список документов
        /// </summary>
        public IEnumerable<Documenti>? Documenti { get; set; }
    }
}
