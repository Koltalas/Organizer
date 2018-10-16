(function () {
    angular.module("app").controller("app.views.contacts.updateModal", [
        "$scope", "$uibModalInstance", "abp.services.app.contact", "currentContact",
        function ($scope, $uibModalInstance, contactService, currentContact) {
            var vm = this;
            vm.contact = {
                id: currentContact.id,
                fName: currentContact.fName,
                lName: currentContact.lName,
                email: currentContact.email,
                phoneNumber: currentContact.phoneNumber,
                birthday: new Date(currentContact.birthday),
                adress: currentContact.adress,
                userName: currentContact.userName
            };



            vm.save = function () {
                contactService.update(vm.contact)
                    .then(function () {
                        abp.notify.success(abp.localization.localize("UpdatedSuccess", "Organizer"));
                        $uibModalInstance.close(vm.contact);
                    });
            };

            $scope.linkToUser = function () {
                var user = {
                    userName: vm.contact.userName,
                    contactId: vm.contact.id
                };
                contactService.linkUserProfile(user).then(function () {
                    abp.notify.info(abp.localization.localize("LinkSuccess", "Organizer"));
                    $uibModalInstance.close(vm.contact);
                });
            }

            vm.cancel = function () {
                $uibModalInstance.dismiss({});
            };
        }
    ]);
})();