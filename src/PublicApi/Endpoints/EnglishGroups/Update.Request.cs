using System.ComponentModel.DataAnnotations;

namespace PublicApi.Endpoints.EnglishGroups
{
    public class UpdateEnglishGroupRequest
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
    }
}