(function() {
    var app = angular.module("app");

    app.controller("app.views.home", [
        "$scope", function ($scope) {
            var vm = this;
        }]);

    app.directive("calendarView", function () {
        return {
            templateUrl: "~/App/Main/views/Calendar/index.cshtml",
            controller: "calendarCtrl"
        };
    });

    app.directive("todoView", function () {
        return {
            templateUrl: "/App/Main/views/todo/index.cshtml",
            controller: "TodoCtrl"
        };
    });

    app.directive("contactView", function () {
        return {
            templateUrl: "/App/Main/views/contacts/index.cshtml",
            controller: "app.views.contacts.index",
            controllerAs : "vm"
        };
    });

    app.directive("notesView", function () {
        return {
            templateUrl: "/App/Main/views/notes/index.cshtml",
            controller: "notesCtrl"
        };
    });

})();