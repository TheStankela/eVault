using System.ComponentModel;

namespace eVault.Domain.Enums
{
    public enum eUserRole
    {
        [Description("Administrator")]
        Administrator = 1252020,

        [Description("User")]
        User = 1252021,

        [Description("Finance")]
        Finance = 1252022,

        [Description("Manager")]
        Manager = 1252023,
    }
}
