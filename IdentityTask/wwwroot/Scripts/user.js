
$(document).ready(function () {
    console.log("ready!");
    startLoad();
});

var fillUserInfo = function(data) {
    var obj = JSON.parse(data);
    $('.userName').text(obj.Email);
    $('.userCountry').text(obj.Country);
    $('.userAge').text(obj.Age);
    $('.userInfo').css('display', 'block');
    $('.loginForm').css('display', 'none');
}

var startLoad = function () {
    var token = sessionStorage.getItem(tokenKey);
    if (token === null) {
        return;
    }
    $.ajax({
        type: 'GET',
        url: '/api/info',
        beforeSend: function (xhr) {
            //var token = sessionStorage.getItem(tokenKey);
            console.log(token);
            xhr.setRequestHeader("Authorization", "Bearer " + token);
        },
        success: function (data) {
            fillUserInfo(data);
        },
        error: function (data) {
            $('.loginForm').css('display', 'block');
            $('.userInfo').css('display', 'none');
            console.log("fail");
        }
    });
}



var tokenKey = "accessToken";
$('#submitLogin').click(function (e) {
    var email = $('#emailLogin').val();
    var password = $('#passwordLogin').val();

    e.preventDefault();
    var loginData = {
        grant_type: 'password',
        email: email,
        password: password
    };
    $.ajax({
        type: 'POST',
        url: 'api/account/token',
        data: loginData
    }).success(function (data) {
        sessionStorage.setItem(tokenKey, data.access_token);
        startLoad();

        console.log(data);
    }).fail(function (data) {
        console.log("fail");
    });
});

$('#logOut').click(function (e) {
    e.preventDefault();
    $('.loginForm').css('display', 'block');
    $('.userInfo').css('display', 'none');
    sessionStorage.removeItem(tokenKey);
});
