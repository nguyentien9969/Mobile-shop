using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Entity.Shared
{
    public enum CartItemState
    {
        Success = 1,
        CartNotFound = 2,
        ExceedQuantity = 3,
        ProductNotFound = 4
    }

    public enum PlaceOrderState
    {
        Success = 0,
        CartNotFound = 1,
        Failure = 999
    }

    public enum OrderState
    {
        Pending = 0,
        InProcess = 1,
        InDelivery = 2,
        Success = 3,
        Failure = 4
    }
}
