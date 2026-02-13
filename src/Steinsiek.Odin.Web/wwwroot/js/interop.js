window.bootstrapInterop = {
    showModal: function (modalId) {
        var element = document.getElementById(modalId);
        if (element) {
            var modal = bootstrap.Modal.getOrCreateInstance(element);
            modal.show();
        }
    },
    hideModal: function (modalId) {
        var element = document.getElementById(modalId);
        if (element) {
            var modal = bootstrap.Modal.getInstance(element);
            if (modal) {
                modal.hide();
            }
        }
    },
    setTheme: function (theme) {
        document.documentElement.setAttribute("data-theme", theme);
        document.documentElement.setAttribute("data-bs-theme", theme);
    }
};
