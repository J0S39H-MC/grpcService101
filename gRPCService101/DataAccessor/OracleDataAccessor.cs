using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace gRPCService101.DataAccessor
{
    public class OracleDataAccessor : BaseDataAccessor
    {
        public OracleDataAccessor(IConfiguration configuration) : base(() => { return new OracleConnection(configuration?.GetConnectionString("OracleConnection")); }, () => { return new OracleCommand(); })
        //: base(configuration, "OracleConnection", new OracleCommand())
        {

        }
    }
}
