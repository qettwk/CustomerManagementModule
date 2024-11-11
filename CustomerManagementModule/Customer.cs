namespace Domain
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EMail { get; set; }
        public DateOnly DateOfBirth { get; set; }

        public ICollection<Guid>? OrdersId { get; set; } = null;
    }
}
