(function () {
    angular.module("app").controller("app.views.contacts.createModal", [
        "$scope", "$uibModalInstance", "abp.services.app.contact",
        function ($scope, $uibModalInstance, contactService) {
            var vm = this;
            vm.contact = {
            };

            vm.save = function () {
                contactService.create(vm.contact)
                    .then(function (result) {
                        abp.notify.success(abp.localization.localize("CreatedSuccess", "Organizer"));
                        $uibModalInstance.close(result.data);
                    });
            };
            
            vm.cancel = function () {
                $uibModalInstance.dismiss({});
            };
        }
    ]);
})();