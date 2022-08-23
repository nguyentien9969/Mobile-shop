(function (window, $) {
    window.CheckoutIndex = {
        init: function () {
            this.regisControl();
        },
        regisControl: function () {
            const me = this;

            $('#btnPlaceOrder').off('click').on('click', function (e) {
                e.preventDefault();
                me.submitOrder();
            });
        },
        getOrderFormData: function () {
            var model = {
                FullName: $('#frmOrder [name="FullName"]').val(),
                Address: $('#frmOrder [name="Address"]').val(),
                Email: $('#frmOrder [name="Email"]').val(),
                Phone: $('#frmOrder [name="Phone"]').val(),
                Note: $('#frmOrder [name="Note"]').val(),
            };

            return model;
        },
        orderFormValidate: function () {
            $.validator.addMethod('phoneNumber', function (value) {
                return /(03|05|07|08|09|01[2|6|8|9])+([0-9]{8})\b/.test(value);
            }, 'Yêu cầu nhập đúng định dạng.');

            var frm = $('#frmOrder');
            frm.validate({
                rules: {
                    FullName: {
                        required: true
                    },
                    Address: {
                        required: true
                    },
                    Email: {
                        required: true,
                        email: true
                    },
                    Phone: {
                        required: true,
                        phoneNumber: true
                    }
                },
            });

            return frm.valid();
        },
        submitOrder: function () {
            const me = this;
            if (!me.orderFormValidate()) return;
            var order = me.getOrderFormData();

            $.ajax({
                url: '/Checkout/PlaceOrder',
                data: { order: order},
                type: 'post',
                success: function (res) {
                    if (res.Success) {
                        switch (res.Code) {
                            case 0:
                                toastr["success"]("Thực hiện thành công");
                                setTimeout(function () { location.href = '/'; }, 1000);
                                break;
                            case 1:
                                toastr["success"]("Giỏ hàng không tồn tại");
                                setTimeout(function () { location.href = '/Cart'; }, 1000);
                                break;
                            case 999:
                                toastr["error"]("Thất bại, có lỗi xảy ra");
                                break;
                        }
                    }
                    else {
                        toastr["error"]("Thất bại, có lỗi xảy ra");
                    }
                }
            });
        }
    }
})(window, jQuery);

$(document).ready(function () {
    CheckoutIndex.init();
});