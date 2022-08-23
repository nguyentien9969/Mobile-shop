(function (window, $) {
    window.OrderDetail = {
        init: function () {
            this.regisControl();
            this.initForm();
        },
        regisControl: function () {
            const me = this;

            $('#btnUpdate').off('click').on('click', function (e) {
                e.preventDefault();
                me.submitOrder();
            });
        },
        getFormData: function () {
            var model = {
                Id: $('#frmOrder [name="Id"]').val(),
                State: $('#frmOrder [name="State"]').val()
            };

            return model;
        },
        submitOrder: function () {
            const me = this;
            var model = me.getFormData();

            $.ajax({
                url: '/Order/Update',
                data: { order: model },
                type: 'post',
                success: function (res) {
                    _base.handleResponse(res);
                }
            });
        },
        initForm: function () {
            const me = this;
            me.loadOrder();
        },
        loadOrder: function () {
            const me = this;
            $.ajax({
                url: '/Order/GetDetail/' + $('#frmOrder [name="Id"]').val(),
                success: function (res) {
                    if (res.Success) {
                        var order = res.Data;
                        $('#frmOrder [name="FullName"]').val(order.FullName);
                        $('#frmOrder [name="Email"]').val(order.Email);
                        $('#frmOrder [name="Phone"]').val(order.Phone);
                        $('#frmOrder [name="Address"]').val(order.Address);
                        $('#frmOrder [name="Note"]').val(order.Note);
                        $('#frmOrder [name="State"]').val(order.State);

                        var html = '';

                        $.each(order.OrderItems, function (i, product) {
                            html += `
                                <tr id="${product.Id}">
                                    <td>${(i + 1)}</td>
                                    <td>${product.Name}</td>
                                    <td>${product.Price}</td>
                                    <td>${product.Quantity}</td>
                                    <td>${product.Price * product.Quantity}</td>
                                </tr >`;
                        })

                        $('#tbIndex #totalAmount').html(order.OrderItems.reduce((a, b) => a + (b['Quantity'] || 0) * (b['Price'] || 0), 0));
                        $('#tbIndex tbody').html(html);

                        me._table = $('#tbIndex').DataTable({
                            language: {
                                url: '/Assets/Libs/DataTables/vi.json'
                            },
                            scrollCollapse: true,
                            select: true,
                            rowId: 'Id',
                            pageLength: 20,
                            lengthChange: false,
                            searching: false,
                            paging: false,
                            bInfo: false
                        });
                    }
                }
            });
        }
    }
})(window, jQuery);

$(document).ready(function () {
    OrderDetail.init();
});