(function () {
    var controllerId = "app.views.layout";
    angular.module("app").controller(controllerId, [
        "$scope", function ($scope) {
            var vm = this;

            var sidebarOpen = false;

            $scope.openNav = function () {
                if (!sidebarOpen) {
                    angular.element(document.getElementById("mySidenav").style.width = "250px");
                    angular.element(document.getElementById("main").style.marginLeft = "250px");
                    sidebarOpen = true;
                } else {
                    angular.element(document.getElementById("mySidenav").style.width = "0");
                    angular.element(document.getElementById("main").style.marginLeft = "90px");
                    sidebarOpen = false;
                }
            }

            abp.event.on("abp.notifications.received", function (userNotification) {

             //   console.log(userNotification);
                abp.notify.info(userNotification.notification.data.scaryMessage, userNotification.notification.data.senderUserName);

            });

        }]);
})();