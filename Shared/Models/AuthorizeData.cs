using WebAPI.Models.APIModels;

namespace Shared.Models
{
    public class AuthorizeData
    {
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public int FamilyId { get; set; }
        public Family Family { get; set; }
    }
}
