(function () {
    angular.module("app").controller("app.views.contacts.index", [
        "$scope", "$uibModal", "abp.services.app.contact", "abp.services.app.user",
        function ($scope, $uibModal, contactService, userService) {
            var vm = this;

            vm.contacts = [];

            function getContacts() {
                contactService.getList().then(function (result) {
                    vm.contacts = result.data.items;
                });
            }

            $scope.refreshContacts = function () {
                getContacts();
            }

            vm.openContactUpdateModal = function (contact) {
                var modalInstance = $uibModal.open({
                    templateUrl: "/App/Main/views/contacts/updateModal.cshtml",
                    controller: "app.views.contacts.updateModal as vm",
                    backdrop: "static",
                    resolve: {
                        currentContact: function () {
                            return contact;
                        }
                    }
                });

                modalInstance.result.then(function (result) {
                    result.fullName = result.fName + ((result.lName === null) ? "" : " " + result.lName);
                    vm.contacts[vm.contacts.indexOf(contact)] = result;
                });
            };

            vm.openContactCreationModal = function () {
                var modalInstance = $uibModal.open({
                    templateUrl: "/App/Main/views/contacts/createModal.cshtml",
                    controller: "app.views.contacts.createModal as vm",
                    backdrop: "static"
                });

                modalInstance.result.then(function (result) {
                    vm.contacts.push(result);
                });
            };

            vm.deleteCurrentContact = function (contact) {
                contactService.delete({ id: contact.id }).then(
                    function () {
                        vm.contacts.splice(vm.contacts.indexOf(contact), 1);
                        abp.notify.success(abp.localization.localize("DeletedSuccess", "Organizer"));
                    });
            };

            getContacts();

        }
    ]);
})();