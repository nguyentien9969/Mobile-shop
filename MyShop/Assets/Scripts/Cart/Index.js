(function (window, $) {
    window.CartIndex = {
        init: function () {
            this.regisControl();
            this.initForm();
        },
        regisControl: function () {
            const me = this;

            $('#btnGoToCart').off('click').on('click', function (e) {
                e.preventDefault();
                location.href = '/Cart';
            });

            $('.btnAddToCart').off('click').on('click', function (e) {
                var $btn = $(this);
                e.preventDefault();
                me.addToCart($btn);
            });

            $('#shopping-cart-box').off('click', '.btnRemove').on('click', '.btnRemove', function (e) {
                e.preventDefault();
                me.removeItem($(this).closest('tr').data('productid'));
            });

            $('#shopping-cart-box').off('click', '.btnRefresh').on('click', '.btnRefresh', function (e) {
                e.preventDefault();
                var $tr = $(this).closest('tr');

                me.updateItem($tr.data('productid'), $tr.find('[name="Quantity"]').val());
            });
        },
        getCartFormData: function ($btn) {
            var model = {
                productId: $btn.closest('.frmSingleProduct').data('productid'),
                quantity: $('.frmSingleProduct [name="Quantity"]').val() || 1,
            };

            return model;
        },
        addToCart: function ($btn) {
            const me = this;
            var model = me.getCartFormData($btn);

            $.ajax({
                url: '/Cart/Add',
                data: { productId: model.productId, quantity: model.quantity },
                type: 'post',
                success: function (res) {
                    _base.handleResponse(res, function () {
                        $('#btnGoToCart .item-text').html(res.Data || 0);
                    });
                }
            });
        },
        removeItem: function (productId) {
            if (productId > 0) {
                $.ajax({
                    url: '/Cart/Delete',
                    data: { productId: productId },
                    type: 'post',
                    success: function (res) {
                        _base.handleResponse(res, function () {
                            $('#shopping-cart-box').html(res.Data || '');
                        });
                    }
                });
            }
        },
        updateItem: function (productId, quantity) {
            if (productId > 0 && quantity > 0) {
                $.ajax({
                    url: '/Cart/UpdateQuantity',
                    data: { productId: productId, quantity: quantity},
                    type: 'post',
                    success: function (res) {
                        _base.handleResponse(res, function () {
                            $('#shopping-cart-box').html(res.Data || '');
                        });
                    }
                });
            }
        },
        initForm: function () {
            $.ajax({
                url: '/Cart/Count',
                success: function (res) {
                    $('#btnGoToCart .item-text').html(res.Data || 0);
                }
            });
        }
    }
})(window, jQuery);

$(document).ready(function () {
    CartIndex.init();
});