define(['angular', 'jquery-cookie'], function () {
    var DAY = 7;
   
    var subscribedMailController = function ($scope) {
        $scope.submitMail = function () {
            //ajax
            var smail = $("#txtSmail").val();

            //check mailbox
            var myreg = /^([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$/;
            if (!myreg.test(smail)) {
                alert("邮箱格式不正确。Wrong Email Format.")
                return false;
            }

            $.ajax({
                type: 'POST',
                url: "/SubscribedMail",
                data: {
                    mail: smail
                },
                dataType: "json",
                success:function(data)
                {
                    if (data.result == "success") {
                        alert("OK");
                    }
                },
                error:function(errorData)
                {
                }
            });
        };
    };

    return subscribedMailController;
});