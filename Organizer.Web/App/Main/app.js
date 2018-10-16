(function () {
    "use strict";

    var app = angular.module("app", [
        "ngAnimate",
        "ngSanitize",

        "ui.router",
        "ui.bootstrap",
        "ui.jq",
        "angularjs-dropdown-multiselect",

        "abp",

        "CalendarApp",
        "TodoApp",
        "NotesApp",
    ]);


    //Configuration for Angular UI routing.
    app.config([
        "$stateProvider", "$urlRouterProvider", "$locationProvider", "$qProvider",
        function ($stateProvider, $urlRouterProvider, $locationProvider, $qProvider) {
            $locationProvider.hashPrefix("");
            $urlRouterProvider.otherwise("/");
            $qProvider.errorOnUnhandledRejections(false);

            if (abp.auth.hasPermission("Pages.Users")) {
                $stateProvider
                    .state("users", {
                        url: "/users",
                        templateUrl: "/App/Main/views/users/index.cshtml",
                        menu: "Users" //Matches to name of 'Users' menu in OrganizerNavigationProvider
                    });
                $urlRouterProvider.otherwise("/users");
            }

            if (abp.auth.hasPermission("Pages.Tenants")) {
                $stateProvider
                    .state("tenants", {
                        url: "/tenants",
                        templateUrl: "/App/Main/views/tenants/index.cshtml",
                        menu: "Tenants" //Matches to name of 'Tenants' menu in OrganizerNavigationProvider
                    });
                $urlRouterProvider.otherwise("/tenants");
            }

            $stateProvider
                .state("userpage", {
                    url: "/userpage",
                    templateUrl: "/App/Main/views/users/userpage.cshtml"
                })
                .state("home", {
                    url: "/",
                    templateUrl: "/App/Main/views/home/home.cshtml",
                    menu: "Home" //Matches to name of 'Home' menu in OrganizerNavigationProvider
                })
                .state("todo", {
                    url: "/todo",
                    templateUrl: "/App/Main/views/todo/todo.cshtml",
                    menu: "ToDo" //Matches to name of 'Contacts' menu in OrganizerNavigationProvider
                }).state("calendar", {
                    url: "/calendar",
                    templateUrl: "/App/Main/views/Calendar/Calendar.cshtml",
                    menu: "Calendar" //Matches to name of 'Contacts' menu in OrganizerNavigationProvider
                }).state("notes", {
                    url: "/notes",
                    templateUrl: "/App/Main/views/notes/notes.cshtml",
                    menu: "Notes" //Matches to name of 'Contacts' menu in OrganizerNavigationProvider
                })
                .state("contacts", {
                    url: "/contacts",
                    templateUrl: "/App/Main/views/contacts/contacts.cshtml",
                    menu: "Contacts" //Matches to name of 'Contacts' menu in OrganizerNavigationProvider
                });

        }
    ]);
})();