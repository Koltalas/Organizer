(function () {
    var app = angular.module("CalendarApp");

    app.controller("eventListsCtrl", eventListsCtrl);

    eventListsCtrl.$inject = ["$scope", "abp.services.app.event", "$uibModalInstance"];

    function eventListsCtrl($scope, eventService, $uibModalInstance) {
        $scope.eventLists = [];

        function loadLists() {
            eventService.getAllList().then(function (result) {
                $scope.eventLists = result.data.items;
            });
        }

        $scope.newList = "";
        $scope.sharingPassword = "";

        var lists = $scope.eventLists;

        $scope.addList = function () {
            var newList = {
                title: $scope.newList.trim(),
                sharingPassword: $scope.sharingPassword
            };

            eventService.createList(newList)
                .then(function (result) {
                    if (newList.sharingPassword !== "") {
                        var sharingData = {
                            eventListId: result.data.id,
                            sharingPassword: newList.sharingPassword
                        };
                        eventService.generateSharing(sharingData).then(function (res) {
                            result.data.sharingKey = res.data.sharingKey;
                        });
                    }
                    $scope.eventLists.push(result.data);
                    $scope.newList = "";
                    $scope.sharingPassword = "";
                    abp.notify.success(abp.localization.localize("CreatedSuccess", "Organizer"));
                });
        };


        $scope.follow = function () {
            var sharingData = {
                sharingKey: $scope.followKey,
                sharingPassword: $scope.followPassword
            };
            eventService.getAccessToList(sharingData).then(function (result) {
                $scope.eventLists.push(result.data);
                abp.notify.success(abp.localization.localize("FollowSuccess", "Organizer"));
            });
        };

        $scope.editList = function (list) {
            $scope.editedList = list;
            $scope.originalList = angular.extend({}, list);
            list.editList = true;
        };


        $scope.saveEditsList = function (list, event) {
            if (event === "blur" && $scope.saveEventList === "submit") {
                $scope.saveEventList = null;
                return;
            }

            $scope.saveEventList = event;

            if ($scope.revertedList) {
                $scope.revertedList = null;
                return;
            }

            list.title = list.title.trim();

            if (list.title === $scope.originalList.title && list.color === $scope.originalList.color) {
                $scope.editedList = null;
                return;
            }

            list.editList = false;

            eventService.updateList(list)
                .then(function success() {
                    abp.notify.success(abp.localization.localize("UpdatedSuccess", "Organizer"));
                }, function error() {
                    list.title = $scope.originalList.title;
                })
                .finally(function () {
                    $scope.editedList = null;
                });
        };

        $scope.revertEditsList = function (list) {
            lists[lists.indexOf(list)] = $scope.originalList;
            $scope.editedList = null;
            $scope.originalList = null;
            $scope.revertedList = true;
        };

        $scope.removeList = function (list) {
            eventService.deleteList({ id: list.id })
                .then(function () {
                    $scope.eventLists.splice($scope.eventLists.indexOf(list), 1);
                    abp.notify.success(abp.localization.localize("DeletedSuccess", "Organizer"));
                });
        };

        $scope.updateList = function (list) {
            eventService.updateList(list);
            abp.notify.success(abp.localization.localize("UpdatedSuccess","Organizer"));
        };

        $scope.ok = function () {
            $uibModalInstance.close();
        };


        loadLists();
    }
})();