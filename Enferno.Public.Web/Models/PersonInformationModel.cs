using System;

namespace Enferno.Public.Web.Models
{
    [Serializable]
    public class PersonInformationModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string CellPhone { get; set; }
    }
}