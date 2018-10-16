(function() {
    var app = angular.module("TodoApp");

    app.controller("todoListsCtrl", todoListsCtrl);

    todoListsCtrl.$inject = ["$scope", "abp.services.app.toDo", "$uibModalInstance", "abp.services.app.event"];

    function todoListsCtrl($scope, todoService, $uibModalInstance, eventService) {
        $scope.todoLists = [];
        $scope.events = [];
        $scope.selectedEvent = "";

        function loadTodoLists() {
            todoService.getAllLists().then(function (result) {
                $scope.todoLists = result.data.items;
            });
        }

        function loadEvents() {
            eventService.getAllEvents().then(function (result) {
                $scope.events = result.data.items;
            });
        }
        loadEvents();

        $scope.newList = "";
        $scope.sharingPassword = "";
        var lists = $scope.todoLists;

        $scope.addList = function () {
            var newList = {
                title: $scope.newList.trim(),
                sharingPassword: $scope.sharingPassword
            };

            todoService.createList(newList)
                .then(function (result) {
                    if (newList.sharingPassword !== "") {
                        var sharingData = {
                            todoListId: result.data.id,
                            sharingPassword: newList.sharingPassword
                        };
                        todoService.generateSharing(sharingData).then(function (res) {
                            result.data.sharingKey = res.data.sharingKey;
                        });
                    }         
                    $scope.todoLists.push(result.data);
                    $scope.newList = "";
                    $scope.sharingPassword = "";
                    abp.notify.success(abp.localization.localize("CreatedSuccess", "Organizer"));
                });
        };

        $scope.follow = function () {
            var sharingData = {
                sharingKey: $scope.followKey,
                sharingPassword : $scope.followPassword
            };
            todoService.getAccessToList(sharingData).then(function (result) {
                $scope.todoLists.push(result.data);
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

            if (list.title === $scope.originalList.title) {
                $scope.editedList = null;
                return;
            }

            list.editList = false;

            todoService.updateList(list)
                .then(function success() {
                    abp.notify.success(abp.localization.localize("UpdatedSuccess"));
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
            todoService.deleteList({ id: list.id })
                .then(function () {
                    $scope.todoLists.splice($scope.todoLists.indexOf(list), 1);
                    abp.notify.success(abp.localization.localize("DeletedSuccess"));
                });
        };

        $scope.updateList = function (list) {
            todoService.updateList(list);
            abp.notify.success(abp.localization.localize("UpdatedSuccess"));
        };

        $scope.ok = function () {
            $uibModalInstance.close();
        };

        $scope.eventSelected = function (list , selected) {
            if (selected !== "" && list.eventId !== selected.id) {
                var reference = {
                    toDoListId: list.id,
                    eventId: selected.id
                };

                todoService.addReferenceToEvent(reference).then(function (res) {
                    list.title = selected.title;
                    $scope.updateList(list);
                });

            }
        };


        loadTodoLists();
    }
})();