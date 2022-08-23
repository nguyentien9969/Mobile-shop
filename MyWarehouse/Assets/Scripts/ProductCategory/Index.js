(function (window, $) {
    window.ProductCategoryIndex = {
        init: function () {
            this.regisControl();
            this.loadListCategory();
        },
        regisControl: function () {
            //this.initDatatable();
        },
        initDatatable: function (listCategory) {
            var me = this;

            var html = '';
            if (listCategory && listCategory.length > 0) {
                $.each(listCategory, function (i, cate) {
                    html += `
                    <tr id="${cate.Id}">
                        <td>${(i + 1)}</td>
                        <td>${cate.Name}</td>
                        <td>${cate.Sort}</td>
                        <td>${cate.IsHomePage ? 'Có' : 'Không'}</td>
                        <td>${cate.Description || ''}</td>
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
                    var btnDelete = $('<button type="button" class="btn btn-outline-secondary validate-selected" id="btnDelete" disabled></button>').append('<i class="tf-icons bx bx-trash"></i>');

                    
                    $('div.eight.column:eq(0)', this.api().table().container()).append($('<div class="btn-group" id="btn-functions" role="group" aria-label="First group"></div>'));
                    $('div.eight.column:eq(0) .btn-group', this.api().table().container()).append(btnCreate);
                    $('div.eight.column:eq(0) .btn-group', this.api().table().container()).append(btnEdit);
                    $('div.eight.column:eq(0) .btn-group', this.api().table().container()).append(btnDelete);

                    me.regisBtnFunction();
                }
            });

            me.regisDatatableFunction();
            
        },
        regisBtnFunction: function () {
            const me = this;

            $('#btnCreate').off('click').on('click', function (e) {
                e.preventDefault();
                location.href = '/ProductCategory/Create';
            });

            $('#btnUpdate').off('click').on('click', function (e) {
                e.preventDefault();
                location.href = '/ProductCategory/Update/' + me._seletedId;
            });

            $('#btnDelete').off('click').on('click', function (e) {
                e.preventDefault();
                me.deleteCategory();
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
        loadListCategory: function () {
            const me = this;

            $.ajax({
                url: '/ProductCategory/GetAll',
                success: function (res) {
                    if (res.Success) {
                        me.initDatatable(res.Data);
                    }
                }
            });
        },
        deleteCategory: function () {
            const me = this;
            $.ajax({
                url: '/ProductCategory/Delete',
                data: { id: me._seletedId },
                type: 'post',
                success: function (res) {
                    _base.handleResponse(res, function () {
                        if (res.Success) {
                            setTimeout(function () { location.href = '/ProductCategory/Index' }, 1000);
                        }
                    });
                }
            })
        }
    }
})(window, jQuery);

$(document).ready(function () {
    ProductCategoryIndex.init();
});