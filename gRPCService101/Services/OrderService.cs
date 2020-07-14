using Grpc.Core;
using gRPCService101.DataAccessor;
using gRPCService101.ServiceModel;
using gRPCService101.ServiceModel.Types;
using gRPCService101.Protos;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace gRPCService101.Services
{
    public class OrderService : GrpcServices.GrpcServicesBase
    {
        private readonly OracleDataAccessor dataAccessor;
        public OrderService(BaseDataAccessor baseDataAccessor)
        {
            this.dataAccessor = baseDataAccessor as OracleDataAccessor;
        }
        public override async Task<Protos.OrderResponse> GetOrders(Protos.OrderRequest request, ServerCallContext context)
        {
            var param1 = new OracleParameter("status", OracleDbType.Varchar2, request.OrderStatus, ParameterDirection.Input);
            var param2 = new OracleParameter("cust_cursor", OracleDbType.RefCursor, ParameterDirection.Output);

            var customerOrdersResults = await this.dataAccessor.ExecuteCommandQuery<ServiceModel.Types.CustomerOrder>("NEW_ORDERS_PKG.SELECT_CUSTOMER_ORDER", @params: new List<System.Data.Common.DbParameter>() { param1, param2 });

            var response = new Protos.OrderResponse();
            response.Results.AddRange(customerOrdersResults.Select(x => new Protos.CustomerOrder() { CustomerId = x.CustomerId, OrderId = x.OrderId, OrderStatus = x.OrderStatus }));

            return response;
        }
    }
}