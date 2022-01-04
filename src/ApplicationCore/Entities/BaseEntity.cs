using System.Text.Json.Serialization;

namespace ApplicationCore.Entities
{
    public abstract class BaseEntity
    {
        [JsonIgnore]
        public virtual int Id { get; set; }
    }
}
