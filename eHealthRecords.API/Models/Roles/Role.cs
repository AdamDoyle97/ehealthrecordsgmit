namespace eHealthRecords.API.Models.Roles
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public RoleType RoleType { get; set; }
        public string Permissions { get; set; }
    }
}
