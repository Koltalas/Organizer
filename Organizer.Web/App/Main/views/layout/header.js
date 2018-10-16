﻿(function () {
    var controllerId = "app.views.layout.header";
    angular.module("app").controller(controllerId, [
        "$rootScope", "$state", "appSession", "$scope",
        function ($rootScope, $state, appSession, $scope) {
            var vm = this;

            vm.languages = abp.localization.languages;
            console.log(abp.localization.languages);
            vm.currentLanguage = abp.localization.currentLanguage;

            vm.menu = abp.nav.menus.MainMenu;
            vm.currentMenuName = $state.current.menu;
            vm.isUserSignedIn = function () { return !!appSession.user; };

            $rootScope.$on("$stateChangeSuccess", function (event, toState, toParams, fromState, fromParams) {
                vm.currentMenuName = toState.menu;
            });

            vm.getShownUserName = function () {
                if (!abp.multiTenancy.isEnabled) {
                    return appSession.user.userName;
                } else {
                    if (appSession.tenant) {
                        return appSession.tenant.tenancyName + "\\" + appSession.user.userName;
                    } else {
                        return ".\\" + appSession.user.userName;
                    }
                }
            };

        }
    ]);
})();