using Newtonsoft.Json;

namespace ApplicationCore.Entities
{
    public abstract class BaseEntity
    {
        [JsonIgnore]
        public virtual int Id { get; set; }
    }
}
