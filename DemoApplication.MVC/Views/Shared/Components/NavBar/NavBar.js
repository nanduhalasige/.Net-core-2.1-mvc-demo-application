toggleNav = () => {
    if ($('#sideNav').hasClass('expand')) {
        $('#sideNav').removeClass('expand');
        $('.fa-times').addClass('hidden');
    } else {
        $('#sideNav').addClass('expand');
        $('.fa-times').removeClass('hidden');
    }
};