﻿var App = App || {};
(function () {

    var appLocalizationSource = abp.localization.getSource("Organizer");
    App.localize = function () {
        return appLocalizationSource.apply(this, arguments);
    };

})(App);