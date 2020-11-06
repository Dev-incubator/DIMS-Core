
function showSpinner() {
    $('#global-spinner').show();
}

function hideSpinner() {
    $('#global-spinner').hide();
}

$(document).on("change", ".checkbox-list input[type='checkbox']", function () {
    if ($(this).is(':checked')) {
        $(this).parent().siblings('input:hidden').prop('disabled', false);
    } else {
        $(this).parent().siblings('input:hidden').prop('disabled', true);
    }
});

$(window).on('load', function () {
    $('.checkbox-list input:checked').parent().siblings('input:hidden').prop('disabled', false);
});