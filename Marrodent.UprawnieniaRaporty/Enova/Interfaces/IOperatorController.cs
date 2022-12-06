using System.Collections.Generic;
using Soneta.Business.App;

namespace Marrodent.UprawnieniaRaporty.Enova.Interfaces
{
    public interface IOperatorController
    {
        ICollection<Operator> GetOperators { get; }
    }
}