using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gRPCService101.DataAccessor
{
    public class OracleDataAccessor : BaseDataAccessor
    {
        public OracleDataAccessor(IConfiguration configuration) : base(configuration, "OracleConnection") { }
    }
}
