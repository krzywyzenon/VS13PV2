$(function() {


    $('#username').focus(function() {
        $(this).addClass('username.focused');
        $(this).removeClass('username');
        $(this).val('');
    });

    $('#Password').focus(function() {
        $(this).addClass('password.focused');
        $(this).removeClass('password');
        $(this).prop('type', 'password');
        $(this).val('');
    });

    $('#username').blur(function() {
        if ($(this).val() == '') {
            $(this).addClass('username');
            $(this).removeClass('username.focused');
            $(this).attr('value', 'Username');
        }
    });

    $('#Password').blur(function() {
        if ($(this).val() == '') {
            $(this).attr('value', 'Password');
            $(this).prop('type', 'text');
            $(this).addClass('password');
            $(this).removeClass('password.focused');
        }
    });



});
