(function () {

    var app = angular.module("NotesApp", []);

    app.controller("notesCtrl", ["$scope", "abp.services.app.note", function ($scope, noteService) {

        $scope.notes = [];
        $scope.newNote = {};


        function loadNotes() {
            noteService.getList().then(function (result) {
                $scope.notes = result.data.items;
            });
        }

        $scope.refreshNotes = function () {
            loadNotes();
        }

        $scope.addNote = function () {
            $scope.newNote.content = " ";
            var newNote = {
                title: $scope.newNote.title,
                content: $scope.newNote.content
            };

            noteService.create(newNote)
                .then(function (result) {
                    $scope.notes.push(result.data);
                    $scope.newNote = {};
                    abp.notify.success(abp.localization.localize("CreatedSuccess", "Organizer"));
                });
        };


        $scope.delete = function (note) {
            noteService.delete({ id: note.id }).then(
                function () {
                    $scope.notes.splice($scope.notes.indexOf(note), 1);
                    abp.notify.success(abp.localization.localize("DeletedSuccess", "Organizer"));
                });
        };

        $scope.noteFocus = function (note) {
            $scope.originalTitle = note.title;
            $scope.originalContent = note.content;
            note.edit = true;
        };

        $scope.update = function (note) {
            note.edit = false;

            if ($scope.originalContent === note.content && $scope.originalTitle === note.title)
                return;

            noteService.update(note).then(
                function () {
                    abp.notify.success(abp.localization.localize("UpdatedSuccess", "Organizer"));
                });
        };

        loadNotes();

    }]);

})();