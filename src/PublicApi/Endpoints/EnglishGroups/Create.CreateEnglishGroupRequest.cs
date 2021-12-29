using System.ComponentModel.DataAnnotations;

namespace PublicApi.Endpoints.EnglishGroups
{
    public class CreateEnglishGroupRequest
    {
        [Required()]
        public string Name { get; set; }
    }
}