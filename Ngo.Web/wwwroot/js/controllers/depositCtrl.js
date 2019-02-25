(function (app) {

    app.controller('depositCtrl', depositCtrl);

    depositCtrl.$inject = ['$scope', 'apiService', '$window', '$timeout', '$q', '$log', '$http'];

    function depositCtrl($scope, apiService, $window, $timeout, $q, $log, $http) {

        var self = this;

        self.accountNo = null;
        self.status = null;

        self.member = null;
        self.book = null;



        init();

        function init() {
            apiService.get('https://localhost:5001/api/members', null, membersLoadComplete, membersLoadFailed);
            apiService.get('https://localhost:5001/api/books', null, booksLoadComplete, booksLoadFailed);
            apiService.get('https://localhost:5001/api/accounts', null, accountsLoadComplete, accountsLoadFailed);
        }



        function booksLoadComplete(result) {
            self.books = result.data;
            console.log(result);
        }

        function booksLoadFailed(response) {
            console.log(response);
        }


        function membersLoadComplete(result) {
            self.members = result.data;
            console.log(result);
        }

        function membersLoadFailed(response) {
            console.log(response);
        }

        function accountsLoadComplete(result) {
            self.accounts = result.data;
            angular.element(document).ready(function () {
                accountTable = $('#account_table');
                accountTable.DataTable();

            });
            console.log(result);
        }

        function accountsLoadFailed(error) {
            console.log(error);
        }



        //deposit Details 

        self.detail = function (account) {
            $window.location.pathname = 'Deposits/Details/' + account.id;
        };

        self.editdeposit = null;
        self.newShow = false;
        self.editShow = false;


        self.edit = function (deposit) {
            self.newShow = false;
            self.editShow = true;
            self.editdeposit = deposit;
        };

        self.delete = function (deposit) {
            $window.location.pathname = 'Deposits/Delete/' + deposit.id;
        };

        self.onLoadDepositById = function (id) {
            apiService.get('https://localhost:5001/api/deposits/' + id, null, dailyDepositLoadComplete, dailyDepositLoadFailed);

        };


        function dailyDepositLoadComplete(result) {

            self.dailyDeposits = result.data;
            angular.element(document).ready(function () {
                depositTable = $('#deposit_table');
                depositTable.DataTable();
            });

        }

        function dailyDepositLoadFailed(error) {
            console.log(error);
        }





    }

})(angular.module('NgoApp'));