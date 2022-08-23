(function (window, $) {
    window.ProductIndex = {
        init: function () {
            this.regisControl();
            this.loadListProduct();
        },
        regisControl: function () {
            
        },
        initDatatable: function (listProduct) {
            var me = this;

            var html = '';
            if (listProduct && listProduct.length > 0) {
                $.each(listProduct, function (i, product) {
                    html += `
                    <tr id="${product.Id}">
                        <td>${(i + 1)}</td>
                        <td style="padding:5px"><img src="${product.MainImage}" class="rounded" width="40" height="40"/></td>
                        <td>${product.Name}</td>
                        <td>${product.Code}</td>
                        <td>${product.CategoryName}</td>
                        <td>${product.Quantity}</td>
                        <td>${product.Price}</td>
                        <td>${product.IsActive ? 'Có' : 'Không'}</td>
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
                    var btnCreate = $('<button type="button" class="btn btn-outline-secondary" id="btnCreate"></button>').append('<i class="tf-icons bx bx-add-to-queue"></i>');
                    var btnEdit = $('<button type="button" class="btn btn-outline-secondary validate-selected" id="btnUpdate" disabled></button>').append('<i class="tf-icons bx bx-edit"></i>');

                    
                    $('div.eight.column:eq(0)', this.api().table().container()).append($('<div class="btn-group" id="btn-functions" role="group" aria-label="First group"></div>'));
                    $('div.eight.column:eq(0) .btn-group', this.api().table().container()).append(btnCreate);
                    $('div.eight.column:eq(0) .btn-group', this.api().table().container()).append(btnEdit);

                    me.regisBtnFunction();
                }
            });

            me.regisDatatableFunction();
            
        },
        regisBtnFunction: function () {
            const me = this;

            $('#btnCreate').off('click').on('click', function (e) {
                e.preventDefault();
                location.href = '/Product/Create';
            });

            $('#btnUpdate').off('click').on('click', function (e) {
                e.preventDefault();
                location.href = '/Product/Update/' + me._seletedId;
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
        loadListProduct: function () {
            const me = this;

            $.ajax({
                url: '/Product/GetAll',
                success: function (res) {
                    if (res.Success) {
                        me.initDatatable(res.Data);
                    }
                }
            });
        },
        //deleteProduct: function () {
        //    const me = this;
        //    $.ajax({
        //        url: '/Product/Delete',
        //        data: { id: me._seletedId },
        //        type: 'post',
        //        success: function (res) {
        //            _base.handleResponse(res, function () {
        //                if (res.Success) {
        //                    setTimeout(function () { location.href = '/Product/Index' }, 1000);
        //                }
        //            });
        //        }
        //    })
        //}
    }
})(window, jQuery);

$(document).ready(function () {
    ProductIndex.init();
});