(function () {
    var app = angular.module("CalendarApp", ["ui.calendar", "ui.bootstrap"]);

    app.controller("calendarCtrl", calendarCtrl);

    calendarCtrl.$inject = ["$scope", "uiCalendarConfig", "$uibModal", "abp.services.app.event", `$compile`];

    function calendarCtrl($scope, uiCalendarConfig, $uibModal, eventService, $compile) {
        $scope.isSelectedEvent = false;

        $scope.eventLists = [];
        $scope.NewEvent = {};
        $scope.SelectedEvent = {};
        $scope.selectedListIndex = {};
        $scope.selectedEventList = {};
        $scope.selectedEventLists = [];
        $scope.index = 0;
        $scope.lists = [];

        function loadEventLists() {
            clearCalendar();
            $scope.lists = [];
            eventService.getAllList().then(function (result) {
                $scope.eventLists = result.data.items;
                $scope.eventLists.forEach(function (val) {
                    val.label = val.title;
                });
            });
        }

        $scope.refreshEvents = function () {
            loadEventLists();
        };

        $scope.multiselectSettings = {
            displayProp: 'title',
            smartButtonMaxItems: 3,
            smartButtonTextConverter: function (itemText, originalItem)
            {
                return itemText;
            },
            enableSearch: true,
            showSelectAll: true,
            keyboardControls: true 
        };

        $scope.multiselectEvents = {
            onItemSelect: function (selected) {
                clearCalendar();
                $scope.lists.push(selected);
                $scope.selectedListIndex = $scope.eventLists.indexOf(selected);
                $scope.selectedEventList = selected;
                angular.forEach($scope.lists, function (value) {
                    uiCalendarConfig.calendars.myCalendar.fullCalendar('addEventSource', value.events);
                });
            },
            onItemDeselect: function (selected) {
                clearCalendar();
                $scope.lists.splice($scope.lists.indexOf(selected), 1);
                if ($scope.lists.indexOf(selected) !== 0) {
                    $scope.selectedListIndex = $scope.lists[$scope.lists.indexOf(selected) - 1];
                } else {
                    $scope.selectedListIndex = $scope.lists[$scope.lists.indexOf(selected) + 1];
                }
                angular.forEach($scope.lists, function (value) {
                    uiCalendarConfig.calendars.myCalendar.fullCalendar('addEventSource', value.events);
                });
            }
        };


        function formatDate(date) {
            var hours = date.getHours();
            var minutes = date.getMinutes();
            var ampm = hours >= 12 ? "pm" : "am";
            hours = hours % 12;
            hours = hours ? hours : 12;
            minutes = minutes < 10 ? "0" + minutes : minutes;
            var strTime = hours + ":" + minutes + " " + ampm;
            return date.getMonth() + 1 + "/" + date.getDate() + "/" + date.getFullYear() + "  " + strTime;
        }

        function clearCalendar() {
            if (uiCalendarConfig.calendars.myCalendar != null) {
                uiCalendarConfig.calendars.myCalendar.fullCalendar("removeEvents");
                uiCalendarConfig.calendars.myCalendar.fullCalendar("unselect");
            }
        }

        $scope.eventRender = function (event, element) {
            element.attr({
                'uib-tooltip-html': `\'<p>${event.description}</p>\'`,
                'uib-tooltip-append-to-body': true
            });
            $compile(element)($scope);
        };

        $scope.fullUiConfig = {
            calendar: {
                //height: 'parent',
                editable: true,
                displayEventTime: false,
                header: {
                    left: "month,agendaWeek,agendaDay,timeline",
                    center: "title",
                    right: "today prev,next"
                },
                timeFormat: {
                    month: "H(:mm)",
                    agenda: "h:mm t"
                },
                selectable: true,
                selectHelper: true,
                dayClick: function () {
                    $scope.SelectedEvent = null;
                    $scope.isSelectedEvent = false;
                },
                eventDrop: function (event, delta, revertFunc) {
                    $scope.NewEvent = {
                        id: event.id,
                        start: event.start,
                        end: event.end,
                        allDay: false,
                        title: event.title,
                        description: event.description,
                        FirtsTime: false,
                        eventListId: $scope.selectedEventList.id
                    };

                    if (!confirm("Are you sure about this change?")) {
                        revertFunc();
                    }
                    else {
                        eventService.updateEvent($scope.NewEvent);
                    }

                },
                select: function (startt, endd) {
                    var fromDate = formatDate(new Date(startt));
                    var endDate = formatDate(new Date(endd));
                    $scope.NewEvent = {
                        start: fromDate,
                        end: endDate,
                        allDay: false,
                        title: "",
                        description: "",
                        FirstTime: true,
                        eventListId: $scope.selectedEventList.id
                    };
                    $scope.ShowModal();
                },
                eventRender: $scope.eventRender,
                eventClick: function (event) {
                    var fromDate = formatDate(new Date(event.start));
                    var endDate = formatDate(new Date(event.end));
                    $scope.isSelectedEvent = true;
                    $scope.SelectedEvent = {
                        id: event.id,
                        start: fromDate,
                        end: endDate,
                        title: event.title,
                        description: event.description
                    };
                    $scope.NewEvent = {
                        id: event.id,
                        start: fromDate,
                        end: endDate,
                        allDay: false,
                        title: event.title,
                        description: event.description,
                        FirtsTime: false,
                        eventListId: event.eventListId,
                        owner: event.owner
                    };
                    $scope.ShowModal();
                }
            }
        };


        $scope.visualUiConfig = {
            calendar: {
                height: 450,
                editable: false,
                displayEventTime: false,
                header: {
                    left: "month,agendaWeek,agendaDay,timeline",
                    center: "title",
                    right: "today prev,next"
                },
                timeFormat: {
                    month: "H(:mm)",
                    agenda: "h:mm t"
                },
                selectable: true,
                selectHelper: true,
                dayClick: function () {
                    $scope.SelectedEvent = null;
                    $scope.isSelectedEvent = false;
                },
                eventRender: $scope.eventRender,
                eventClick: function (event) {
                    var fromDate = formatDate(new Date(event.start));
                    var endDate = formatDate(new Date(event.end));
                    $scope.isSelectedEvent = true;
                    $scope.SelectedEvent = {
                        id: event.id,
                        start: fromDate,
                        end: endDate,
                        title: event.title,
                        description: event.description
                    };
                }
            }
        };

        function findIndexOfEvent(events, id) {
            for (let i = 0; i < events.length; i++) {
                if (events[i].id.toString() === id.toString()) {
                    return i;
                }
            }
            return -1;
        }



        $scope.ShowModal = function () {
            $scope.option = {
                templateUrl: "modalContent.html",
                controller: "modalCalendarController",
                backdrop: "static",
                resolve: {
                    NewEvent: function () {
                        return $scope.NewEvent;
                    }
                }
            };
            var modal = $uibModal.open($scope.option);
            modal.result.then(function (data) {
                $scope.NewEvent = data.event;
                switch (data.operation) {
                    case "Save":
                        eventService.createEvent($scope.NewEvent).then(function (result) {
                            uiCalendarConfig.calendars.myCalendar.fullCalendar('removeEventSource', $scope.eventLists[$scope.selectedListIndex].events);
                            $scope.eventLists[$scope.selectedListIndex].events.push(result.data);
                            uiCalendarConfig.calendars.myCalendar.fullCalendar('addEventSource', $scope.eventLists[$scope.selectedListIndex]);
                            abp.notify.success(abp.localization.localize("CreatedSuccess", "Organizer"));
                        });
                        break;
                    case "Delete":
                        var id = { id: $scope.NewEvent.id };
                        eventService.deleteEvent(id).then(function () {
                            uiCalendarConfig.calendars.myCalendar.fullCalendar('removeEventSource', $scope.eventLists[$scope.selectedListIndex].events);
                            $scope.eventLists[$scope.selectedListIndex].events.splice(findIndexOfEvent($scope.eventLists[$scope.selectedListIndex].events,$scope.NewEvent.id),1);
                            uiCalendarConfig.calendars.myCalendar.fullCalendar('addEventSource', $scope.eventLists[$scope.selectedListIndex].events);
                            abp.notify.success(abp.localization.localize("DeletedSuccess", "Organizer"));
                        });
                        break;
                    case "Update":
                        eventService.updateEvent($scope.NewEvent).then(function () {
                            loadEventLists();
                            abp.notify.success(abp.localization.localize("UpdatedSuccess", "Organizer"));
                        });
                        break;
                    default:
                        break;
                }
            }, function () {
            });
        };

        $scope.showEventListManage = function () {
            $scope.manageOption = {
                templateUrl: "modalEventLists.html",
                controller: "eventListsCtrl",
                backdrop: "static"
            };
            var modalManage = $uibModal.open($scope.manageOption);
            modalManage.result.then(function () {
                angular.forEach($scope.lists, function (value) {
                    uiCalendarConfig.calendars.myCalendar.fullCalendar('removeEventSource', value);
                });
                loadEventLists();
            },
                function () {
          //          console.log("Modal dialog closed");
                });
        };


        loadEventLists();

    };

    app.controller("modalCalendarController", ["$scope", "$uibModalInstance", "NewEvent", function ($scope, $uibModalInstance, newEvent) {
        $scope.NewEvent = newEvent;
        $scope.Message = "";
        $scope.ok = function () {
            if ($scope.NewEvent.owner === false) {
                $scope.Message = "Its not your list!";
            }
            else {
                if ($scope.NewEvent.title.trim() !== "") {
                    $uibModalInstance.close({ event: $scope.NewEvent, operation: "Save" });
                }
                else {
                    $scope.Message = "Event title required!";
                }
            }
        };
        $scope.update = function () {
            $uibModalInstance.close({ event: $scope.NewEvent, operation: "Update" });
        };
        $scope.delete = function () {
            $uibModalInstance.close({ event: $scope.NewEvent, operation: "Delete" });
        };
        $scope.cancel = function () {
            $uibModalInstance.dismiss("cancel");
        };
    }]);

    

})();