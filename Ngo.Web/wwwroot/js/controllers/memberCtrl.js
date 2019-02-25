(function (app) {

    app.controller('memberCtrl', memberCtrl);

    memberCtrl.$inject = ['$scope', 'apiService', '$window'];

    function memberCtrl($scope, apiService, $window) {
        var self = this;

        init();

        function init() {
            apiService.get('https://localhost:5001/api/members', null, loadMembers, failedMembers);

        }

        function loadMembers(result) {
            self.members = result.data;
            angular.element(document).ready(function () {
                memberTable = $('#member_table');
                memberTable.DataTable();
            });
        
        }

        function failedMembers(reponse) {
            console.log(response);
        }




        self.edit = function (member) {
            $window.location.pathname = 'Members/Edit/' + member.id;
        };

        self.delete = function (member) {
            $window.location.pathname = 'Members/Delete/' + member.id;
        };

        self.detail= function (member) {
            $window.location.pathname = 'Members/Details/' + member.id;
        };

    }

})(angular.module('NgoApp'));