using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CRUDClientCadastration
{
    class Connection
    {
        public static string Local()
        {
            return ConfigurationManager.ConnectionStrings["DatabaseString"].ConnectionString;
        }
    }
}
