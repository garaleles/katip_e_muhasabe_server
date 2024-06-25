using eMuhasebeServer.Domain.Abstractions;
using eMuhasebeServer.Domain.Events.ValueObjects;

namespace eMuhasebeServer.Domain.Entities
{
    public sealed class Company : Entity
    {
        public string Name { get; set; } = string.Empty;
        public string FullAddress { get; set; } = string.Empty;
        public string TaxNumber { get; set; } = string.Empty;
        public string TaxOffice { get; set; } = string.Empty;


        public Database Database { get; set; } = new(string.Empty, string.Empty, string.Empty, string.Empty);

    }
}
