using PublicApi.Models;

namespace PublicApi.Endpoints.EnglishGroups
{
    public class EnglishGroupFilterRequest : BaseFilterDto
    {
        public string Name { get; set; }
    }
}