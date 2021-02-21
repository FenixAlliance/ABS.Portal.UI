window.blazorUtils = {
    onHold: function () {
    },
    onReady: function () {
    },
    HoldOnClose: function() {
        HoldOn.close();
    },
    HoldOnStart: function () {
        HoldOn.start();
    },
    NProgressStart: function () {
        NProgress.start()
    },
    NProgressSet: function (value) {
        NProgress.set(value)
    },
    NProgressIncrement: function () {
        NProgress.inc()
    },
    NProgressDone: function () {
        NProgress.done()
    },
    finishLoading() {
        window.blazorUtils.HoldOnClose()
        window.blazorUtils.NProgressDone()
    },
    startLoading() {
        window.blazorUtils.HoldOnStart()
        window.blazorUtils.NProgressStart()
    },
    formatCurrency: function (number, currency = "USD", locale = "en-US") {
        return new Intl.NumberFormat(locale, { style: 'currency', currency: currency }).format(number)
    },
    toastNotification: function (notificationType, notificationMessage) {
        // Display an info toast with no title
        switch (notificationType) {
            case "info":
                toastr.info(notificationMessage);
                break;
            case "success":
                toastr.success(notificationMessage);
                break;
            case "warning":
                toastr.warning(notificationMessage);
                break;
            case "error":
                toastr.error(notificationMessage);
                break;
        }
    },
    SelectLang: function (culture) {
        Cookies.set('.AspNetCore.Culture', "c=" + culture + "|uic=" + culture);
        window.location.reload();
    },

}