(function (window, $) {
    window.LoginRegister = {
        init: function () {
            this.regisControl();
        },
        regisControl: function () {
            const me = this;

            $('#btnLogin').off('click').on('click', function (e) {
                e.preventDefault();
                me.login();
            });

            $('#btnRegister').off('click').on('click', function (e) {
                e.preventDefault();
                me.register();
            });
        },
        getLoginFormData: function () {
            var model = {
                Email: $('#frmLogin [name="Email"]').val(),
                Password: $('#frmLogin [name="Password"]').val(),
                ReturnUrl: $('[name="ReturnUrl"]').val(),
            };

            return model;
        },
        loginFormValidate: function () {
            var frm = $('#frmLogin');
            frm.validate({
                rules: {
                    Email: {
                        required: true,
                        email: true
                    },
                    Password: {
                        required: true
                    }
                },
            });

            return frm.valid();
        },
        login: function () {
            const me = this;
            if (!me.loginFormValidate()) return;
            var model = me.getLoginFormData();

            $.ajax({
                url: '/Account/Login',
                data: { model: model },
                type: 'post',
                success: function (res) {
                    if (res.Success) {
                        location.href = model.ReturnUrl || '/';
                    }
                    else {
                        toastr["error"]("Tài khoản hoặc mật khẩu không chính xác");
                    }
                }
            });
        },
        getRegisterFormData: function () {
            var model = {
                FullName: $('#frmRegister [name="FullName"]').val(),
                Email: $('#frmRegister [name="Email"]').val(),
                Password: $('#frmRegister [name="Password"]').val(),
                ConfirmPassword: $('#frmRegister [name="ConfirmPassword"]').val()

            };

            return model;
        },
        registerFormValidate: function () {
            var frm = $('#frmRegister');
            frm.validate({
                rules: {
                    FullName: {
                        required: true
                    },
                    Email: {
                        required: true,
                        email: true
                    },
                    Password: {
                        required: true
                    },
                    ConfirmPassword: {
                        required: true,
                        equalTo: '#frmRegister [name="Password"]'
                    }
                },
            });

            return frm.valid();
        },
        register: function () {
            const me = this;
            if (!me.registerFormValidate()) return;
            var model = me.getRegisterFormData();

            $.ajax({
                url: '/Account/Register',
                data: { model: model },
                type: 'post',
                success: function (res) {
                    _base.handleResponse(res, function () {
                        if (res.Success) {
                            setTimeout(function () { location.href = '/Home' }, 1000);
                        }
                    });
                }
            });
        }
    }
})(window, jQuery);

$(document).ready(function () {
    LoginRegister.init();
});