(function (window, $) {
    window.CustomerIndex = {
        init: function () {
            this.regisControl();
            this.loadListCustomer();
        },
        regisControl: function () {
        },
        initDatatable: function (listCustomer) {
            var me = this;
            // Gen Danh sách KH ra HTML
            var html = '';
            if (listCustomer && listCustomer.length > 0) {
                $.each(listCustomer, function (i, item) {
                    html += `
                    <tr id="${item.Id}">
                        <td>${(i + 1)}</td>
                        <td>${item.Email}</td>
                        <td>${item.FullName}</td>
                    </tr >`;
                })
            }

            // ĐỌc HTML danh sách khachsh hàng
            $('#tbIndex tbody').html(html);


            // add class datatable  
            me._table = $('#tbIndex').DataTable({
                scrollY: '55vh',
                language: {
                    url: '/Assets/Libs/DataTables/vi.json'
                },
                scrollCollapse: true,
                select: true,
                rowId: 'Id',
                pageLength: 20,
                lengthChange: false,
                initComplete: function (settings, json) {
                }
            });
        },
        loadListCustomer: function () {
            const me = this;

            $.ajax({
                url: '/Customer/GetAll',
                success: function (res) {
                    if (res.Success) {
                        me.initDatatable(res.Data);
                    }
                }
            });
        }
    }
})(window, jQuery);

$(document).ready(function () {
    CustomerIndex.init();
});