(function (window, $) {
    window.ProductCategoryUpdate = {
        init: function () {
            this.regisControl();
        },
        regisControl: function () {
            const me = this;

            $('#btnUpdate').off('click').on('click', function (e) {
                e.preventDefault();
                me.submitProductCategory();
            });
        },
        getFormData: function () {
            var model = {
                Id: $('#frmCategory [name="Id"]').val(),
                Name: $('#frmCategory [name="Name"]').val(),
                Description: $('#frmCategory [name="Description"]').val(),
                Sort: $('#frmCategory [name="Sort"]').val(),
                IsHomePage: $('#frmCategory [name="IsHomePage"]').is(':checked'),
            };

            return model;
        },
        formValidate: function () {
            var frm = $('#frmCategory');
            frm.validate({
                rules: {
                    Name: {
                        required: true
                    },
                    Sort: {
                        required: true,
                        digits: true
                    },
                },
                errorPlacement: function (error, element) {
                    error.appendTo(element.closest(".error-container"));
                },
            });

            return frm.valid();
        },
        submitProductCategory: function () {
            const me = this;

            if (!me.formValidate()) return;

            var model = me.getFormData();

            $.ajax({
                url: '/ProductCategory/Update',
                data: { productCategory: model },
                type: 'post',
                success: function (res) {
                    _base.handleResponse(res, function () {
                        if (res.Success) {
                            setTimeout(function () { location.href = '/ProductCategory/Index' }, 1000);
                        }
                    });
                }
            });
        }
    }
})(window, jQuery);

$(document).ready(function () {
    ProductCategoryUpdate.init();
});