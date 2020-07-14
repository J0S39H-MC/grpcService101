using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace gRPCService101.ServiceModel.Types
{
    public class CustomerOrder
    {
        [Column("order_id")]
        [DataMember(Order = 1)]
        public int OrderId { get; set; }

        [Column("order_status")]
        [DataMember(Order = 2)]
        public string OrderStatus { get; set; }

        [Column("customer_id")]
        [DataMember(Order = 3)]
        public int CustomerId { get; set; }
    }
}
