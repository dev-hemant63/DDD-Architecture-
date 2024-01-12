namespace Domain.Entities
{
    public class ApplicationUsers
    {
        public string FirstName { get; set; }
        public bool IsActive { get; set; }
        public string GAuthPin { get; set; }
        public string Gender { get; set; }
        public string MeddleName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string AlternateNumber { get; set; }
        public string WhatsAppNumber { get; set; }
        public string Address { get; set; }
        public int PostalCode { get; set; }
        public ApplicationRoles? RoleId { get; set; }
    }
}
