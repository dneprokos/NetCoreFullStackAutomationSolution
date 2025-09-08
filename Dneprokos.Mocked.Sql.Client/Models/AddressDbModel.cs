namespace Dneprokos.Mocked.Sql.Client.Models
{
    public class AddressDbModel
    {
        public int? Id { get; set; }

        public int? PersonId { get; set; } // Foreign key to PersonDbModel

        public string? Street { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? PostalCode { get; set; }

        public string? Country { get; set; }

        public bool? IsPrimary { get; set; }
    }
}
