using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Interface
{
    public interface IJwtAuxValidator
    {
        //Task attachAccountToContext();
        void ValidarToken(IConfiguration configuration, string token);
    }
}
