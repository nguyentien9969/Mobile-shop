(function (window, $) {
    window._base = {
        handleResponse: function (res, callback) {
            if (res.Success) {
                toastr["success"]("Thực hiện thành công");
            }
            else {
                toastr["error"]("Thất bại, có lỗi xảy ra");
            }

            if (callback) {
                callback();
            }
        }
    }
})(window, jQuery);
