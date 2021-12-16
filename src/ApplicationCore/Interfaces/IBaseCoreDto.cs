using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public enum TypeOperation
    {
        None,
        Create,
        Update
    }

    public interface IBaseCoreDto
    {
        TypeOperation Type { get; set; }
    }
}
