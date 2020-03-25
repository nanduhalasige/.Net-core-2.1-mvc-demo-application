function CommonDialog(size, title, loadUrl) {
    $('#dialog').dialog({
        width: size === 'sm' ? 500 : size === 'lg' ? 800 : 1000,
        modal: true,
        title: title,
        open: function () {
            $(this).load(loadUrl);
        }
    });
    $('#dialog').dialog('open');
}

function CloseDialog() {
    $('#dialog').dialog('close');
}