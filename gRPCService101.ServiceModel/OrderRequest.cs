using gRPCService101.ServiceModel.Types;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace gRPCService101.ServiceModel
{
    /**
  * gRPC uses protobuf-net which requires [DataContract] / [DataMember(Order=N)] attributes on all DTOs
  * https://github.com/protobuf-net/protobuf-net/wiki/Attributes
  *
  //* Request DTOs should implement IReturn<T> or IReturnVoid
  * 
  * ServiceStack's Structured Error Responses requires a ResponseStatus property in Response DTOs
  * and throws WebServiceException in GrpcServiceClient
  */

    [DataContract]
    public class OrderRequest 
    {
        [DataMember(Order = 1)]
        public string OrderStatus { get; set; }
    }

    [DataContract]
    public class OrderResponse
    {
        [DataMember(Order = 1)]
        public List<CustomerOrder> Results { get; set; }
    }
}
