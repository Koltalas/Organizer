(function () {
    var app = angular.module("TodoApp", []);

    app.controller("TodoCtrl", todoCtrl);

    todoCtrl.$inject = ["$scope", "abp.services.app.toDo", "$uibModal"];

    function todoCtrl($scope, todoService, $uibModal) {
        "use strict";

        $scope.todoLists = [];
        $scope.todos = [];
        $scope.SelectedTodoList = [];
        $scope.SelectedTodoList.elems = []

        function loadTodoLists() {
            todoService.getAllLists().then(function (result) {
                $scope.todoLists = result.data.items;
            });
        }

        $scope.refreshTodo = function () {
            loadTodoLists();
        }

        $scope.changeList = function () {
            $scope.todos = $scope.SelectedTodoList.elems;
        };

        $scope.ShowTodoManage = function () {
            $scope.todoManageOption = {
                templateUrl: "modalTodoLists.html",
                controller: "todoListsCtrl",
                backdrop: "static"
            };
            var todoManageModal = $uibModal.open($scope.todoManageOption);
            todoManageModal.result.then(function () {
                loadTodoLists();
            }, function () {
              //  console.log("Modal dialog closed");
                });

        };

        $scope.newTodo = "";
        $scope.editedTodo = null;
        var todos = $scope.todos;

        $scope.addTodo = function () {
            var newTodo = {
                isCompleted: false,
                title: $scope.newTodo.trim(),
                ToDoListId: $scope.SelectedTodoList.id
            };

            todoService.createElem(newTodo)
                .then(function (result) {
                    $scope.todos.push(result.data);
                    $scope.newTodo = "";
                    abp.notify.success(abp.localization.localize("CreatedSuccess", "Organizer"));
                });
        };


        $scope.editTodo = function (todo) {
            $scope.editedTodo = todo;
            $scope.originalTodo = angular.extend({}, todo);
            todo.edit = true;
        };


        $scope.saveEdits = function (todo, event) {
            if (event === "blur" && $scope.saveEvent === "submit") {
                $scope.saveEvent = null;
                return;
            }

            $scope.saveEvent = event;

            if ($scope.reverted) {
                $scope.reverted = null;
                return;
            }

            todo.title = todo.title.trim();

            if (todo.title === $scope.originalTodo.title) {
                $scope.editedTodo = null;
                return;
            }

            todo.edit = false;

            todoService.updateElem(todo)
                .then(function success() {
                }, function error() {
                    todo.title = $scope.originalTodo.title;
                })
                .finally(function () {
                    $scope.editedTodo = null;
                    abp.notify.success(abp.localization.localize("UpdatedSuccess", "Organizer"));
                });  
        };

        $scope.revertEdits = function (todo) {
            todos[todos.indexOf(todo)] = $scope.originalTodo;
            $scope.editedTodo = null;
            $scope.originalTodo = null;
            $scope.reverted = true;
        };

        $scope.removeTodo = function (todo) {
            todoService.deleteElem({ id: todo.id })
                .then(function () {
                    $scope.todos.splice($scope.todos.indexOf(todo), 1);
                    abp.notify.success(abp.localization.localize("DeletedSuccess", "Organizer"));
                });
        };

        $scope.updateTodo = function (todo) {
            todoService.updateElem(todo).then(function success() { }, function error() {
                todo.completed = !todo.completed;
                abp.notify.success(abp.localization.localize("UpdatedSuccess", "Organizer"));
            });
         
        };

        $scope.toggleCompleted = function (todo, completed) {
            if (angular.isDefined(completed)) {
                todo.completed = completed;
            }
            todoService.updateElem(todo).then(function success() { }, function error() {
                todo.completed = !todo.completed;
            });
        };

        $scope.clearCompletedTodos = function () {
            $scope.todos.forEach(function (todo) {
                if (todo.isCompleted)
                    $scope.removeTodo(todo);
                abp.notify.success(abp.localization.localize("ClearedSuccess", "Organizer"));
            });
        };

        $scope.getTotalTodos = function () {
            var num = 0;
            angular.forEach($scope.todos, function(value){
                if (!value.isCompleted)
                    num++;
            });
            return num;
        };       

        loadTodoLists();
    }

})();