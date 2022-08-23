using Base.Entity.MappedEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Entity.ViewModels
{
    [NotMapped]
    public class OrderViewModel : Order
    {
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }

        public string DisplayCreatedDate
        {
            get
            {
                return CreatedDate.ToString("dd/MM/yyyy");
            }
        }

        public List<ProductViewModel> OrderItems { get; set; }

        public string DisplayState
        {
            get
            {
                var displayState = String.Empty;

                switch (State)
                {
                    case Shared.OrderState.Pending:
                        displayState = "Đang chờ";
                        break;
                    case Shared.OrderState.InProcess:
                        displayState = "Đang xử lý";
                        break;
                    case Shared.OrderState.InDelivery:
                        displayState = "Đang giao hàng";
                        break;
                    case Shared.OrderState.Success:
                        displayState = "Hoàn thành";
                        break;
                    case Shared.OrderState.Failure:
                        displayState = "Hủy";
                        break;
                    default:
                        break;
                }

                return displayState;
            }
        }
    }
}
