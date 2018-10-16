$(function () {
    $("#ForgotButton").click(function (e) {
        e.preventDefault();
        abp.ui.setBusy(
            $("#LoginArea"),
            abp.ajax({
                url: abp.appPath + "Account/ForgotPassword",
                type: "POST",
                data: JSON.stringify({
                    email: $("#Email").val()
                })
            }).done(function (data) {
                console.log(data);
                abp.message.info(data);
            })
        );
    });

    $("#LoginForm input:first-child").focus();
});