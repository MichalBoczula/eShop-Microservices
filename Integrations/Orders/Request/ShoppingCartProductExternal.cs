using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrations.Orders.Request
{
    public class ShoppingCartProductExternal
    {
        public Guid ProductIntegrationId { get; set; }
        public int Quantity { get; set; }
        public int Total { get; set; }
    }
}
