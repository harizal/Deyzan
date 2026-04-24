function LoadingButton(isLoading, buttonElement, buttonText) {
    $("#" + buttonElement).prop('disabled', isLoading);
    if (isLoading) {
        $("#" + buttonElement).empty().append('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...');
    }
    else {
        $("#" + buttonElement).prop('disabled', false);
        $("#" + buttonElement).text(buttonText);
    }
}

function ShowAlert(alertType, title, message) {

    $("html, body").animate({ scrollTop: 0 }, "slow");

    const alertId = 'dynamicGlobalAlert';

    // Remove existing alert if any
    $("#" + alertId).empty();
    let alertHtml = `        
        <div class="alert alert-${alertType} alert-dismissible fade show" role="alert">
            <strong>${title}</strong> ${message}.
            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
       </div>
    `;

    $("#" + alertId).append(alertHtml);

    // Auto close after 1 second
    setTimeout(function () {
        $("#" + alertId).find(".alert")
            .fadeOut(4000, function () {
                $("#" + alertId).empty();
            });
    }, 1500);
}