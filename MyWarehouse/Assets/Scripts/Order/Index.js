(function (window, $) {
    window.OrderIndex = {
        init: function () {
            this.regisControl();
            this.loadListOrder();
        },
        regisControl: function () {
            //this.initDatatable();
        },
        initDatatable: function (listOrder) {
            var me = this;

            var html = '';
            if (listOrder && listOrder.length > 0) {
                $.each(listOrder, function (i, item) {
                    html += `
                    <tr id="${item.Id}">
                        <td>${(i + 1)}</td>
                        <td>${item.Id}</td>
                        <td>${item.FullName}</td>
                        <td>${item.Email}</td>
                        <td>${item.Phone}</td>
                        <td>${item.DisplayCreatedDate}</td>
                        <td>${item.DisplayState}</td>
                    </tr >`;
                })
            }

            $('#tbIndex tbody').html(html);

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
                    var btnCreate = $('<button type="button" class="btn btn-outline-secondary" id="btnViewDetail"></button>').append('<i class="tf-icons bx bx-show-alt"></i>');

                    
                    $('div.eight.column:eq(0)', this.api().table().container()).append($('<div class="btn-group" id="btn-functions" role="group" aria-label="First group"></div>'));
                    $('div.eight.column:eq(0) .btn-group', this.api().table().container()).append(btnCreate);

                    me.regisBtnFunction();
                }
            });

            me.regisDatatableFunction();
            
        },
        regisBtnFunction: function () {
            const me = this;

            $('#btnViewDetail').off('click').on('click', function (e) {
                e.preventDefault();
                location.href = '/Order/Detail/' + me._seletedId;
            });
        },
        regisDatatableFunction() {
            const me = this;

            $('#tbIndex tbody').on('click', 'tr', function () {
                if ($(this).hasClass('selected')) {
                    $(this).removeClass('selected');
                } else {
                    me._table.$('tr.selected').removeClass('selected');
                    $(this).addClass('selected');
                }
                
                me._seletedId = me._table.row(this).id();
                $('.validate-selected').removeAttr("disabled");
            });
        },
        loadListOrder: function () {
            const me = this;

            $.ajax({
                url: '/Order/GetAll',
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
    OrderIndex.init();
});