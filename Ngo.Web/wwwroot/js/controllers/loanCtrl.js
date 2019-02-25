(function (app) {

    app.controller('loanCtrl', loanCtrl);

    loanCtrl.$inject = ['$scope', 'apiService', '$window', '$timeout', '$q', '$log', '$http', '$mdDialog'];

    function loanCtrl($scope, apiService, $window, $timeout, $q, $log, $http, $mdDialog) {

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
                loanTable = $('#loan_table');
                loanTable.DataTable();

            });
            console.log(result);
        }

        function accountsLoadFailed(error) {
            console.log(error);
        }



        //deposit Details 

        self.detail = function (account) {
            $window.location.pathname = 'Loan/Detail/' + account.id;
        };

        self.editloan = null;
        self.newShow = false;
        self.editShow = false;


        self.edit = function (loan) {
            self.newShow = false;
            self.editShow = true;
            self.editloan = loan;
        };

        self.delete = function (loan) {
            $window.location.pathname = 'Loan/Delete/' + loan.id;
        };

        self.onLoadLoanById = function (id) {
            apiService.get('https://localhost:5001/api/loans/' + id, null, LoanLoadComplete, LoanLoadFailed);

        };


        function LoanLoadComplete(result) {

            self.loans = result.data;
            angular.element(document).ready(function () {
                ProvideLoanListtable = $('#ProvideLoanList_table');
                ProvideLoanListtable.DataTable();
            });

        }

        self.name = "Name";

        function LoanLoadFailed(error) {
            console.log(error);
        }

        self.open = function (loan) {
            $mdDialog.show({
                locals: {
                    dataToPass: loan
                },
                controller: mdDialogCtrl,
                templateUrl: '/js/template/loanCollection.template.html',
                controllerAs: 'ctrl',
                clickOutsideToClose: true

            });

        };

        var mdDialogCtrl = function ($scope, apiService, dataToPass) {
            var self = this;
            self.loanId = dataToPass.id;
            self.new = false;
            self.editLoan = false;
            init(self.loanId);
            function init(loanId) {
                apiService.get('https://localhost:5001/api/loancollcetion/' + loanId , null, accountsLoadComplete, accountsLoadFailed);

            }

            self.collectedLoan = {};
            
            self.collectLoan = function () {
                self.collectedLoan.loanId = dataToPass.id;
                apiService.post('https://localhost:5001/api/loancollcetion', self.collectedLoan, collectLoanLoadComplete, accountsLoadFailed);
            };

            function collectLoanLoadComplete(result) {
                if (result.status === 200) {
                    self.loanId = result.data.loanId;
                    init(self.loanId);
                }
            }

            self.edit = function (loan) {
                self.editLoan = true;
                self.new = false;
                self.collectedLoan = loan;
            };
            self.editcollectLoan = function () {
                apiService.post('https://localhost:5001/api/putoancollcetion', self.collectedLoan, collectLoanLoadComplete, accountsLoadFailed);
            };

            function accountsLoadComplete(result) {

                self.loanCollection = result.data;
                angular.element(document).ready(function () {
                    LoanCollectionListtable = $('#LoanCollectionListtable_table');
                    LoanCollectionListtable.DataTable();
                });

            }

            function accountsLoadFailed(error) {
                console.log(error);
            }
        };



    }

})(angular.module('NgoApp'));