using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Interface
{
    public interface IFunctionalityValidator
    {
        Task<string> ProcessInfo();
    }
}
