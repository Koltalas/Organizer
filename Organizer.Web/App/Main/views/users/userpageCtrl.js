(function () {
    angular.module('app')
        .controller('userpageCtrl', ['$scope', "abp.services.app.user",
            function ($scope, userService) {


                $scope.user = {};

                function getUser() {
                    userService.getDetail().then(function (result) {
                        $scope.user = result.data;
                        $scope.user.creationTime = new Date(result.data.creationTime);

                        $scope.user.birthday = new Date(result.data.birthday);
                    });
                }
                getUser();

                $scope.save = function () {
                    userService.updateUser($scope.user);
                }

            }]);
})();