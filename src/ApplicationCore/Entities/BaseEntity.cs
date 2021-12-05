using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public abstract class BaseEntity<T> where T : IBaseCoreDto
    {
        public virtual int Id { get; protected set; }

        public abstract void Update(T entityDto);

        protected abstract void SetProperties(T entityDto);
    }
}
