(function (app) {

    app.controller('accountCtrl', accountCtrl);

    accountCtrl.$inject = ['$scope', 'apiService', '$window', '$timeout', '$q', '$log','$http'];

    function accountCtrl($scope, apiService, $window, $timeout, $q, $log,$http) {

        var self = this;

        self.accountNo = null;
        self.status = null;

        self.member = null;
        self.book = null;

        

        init();

        function init() {
            apiService.get('https://localhost:5001/api/members', null, membersLoadComplete, membersLoadFailed);
            apiService.get('https://localhost:5001/api/book', null, booksLoadComplete, booksLoadFailed);
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

        self.edit = function (account) {
            $window.location.pathname = 'Accounts/Edit/' + account.id;
        };

        self.delete = function (account) {
            $window.location.pathname = 'Accounts/Delete/' + account.id;
        };
   
        self.details = function (account) {
            $window.location.pathname = 'Accounts/Details/' + account.id;
        };
        self.showNewDeposit = false;
        self.depositAccountNo = 0;
        self.newDeposit = function (account) {
            debugger;
            self.depositAccountNo = account.id;
            self.showNewDeposit = true;
        };
        self.onEditAccount = function (id) {
            apiService.get('https://localhost:5001/api/accounts/' + id, null, accountEditLoadComplete, accountEditLoadFailed);
        };

        function accountEditLoadComplete(result) {

            self.account = result.data;
            //self.accountNo = self.account.accountNo;
            //self.status = self.account.status;
            self.selectedMemberItem = self.account.member;
            self.selectedBookItem = self.account.book;
            console.log(result);

        }

        function accountEditLoadFailed(error) {
            console.log(error);
        }

        self.simulateQuery = false;
        self.isDisabled = false;
        self.expanded = true;

        self.queryMemberSearch = queryMemberSearch;
        self.selectedMemberItemChange = selectedMemberItemChange;
        self.searchMemberTextChange = searchMemberTextChange;
        //book
        self.queryBookSearch = queryBookSearch;
        self.selectedBookItemChange = selectedBookItemChange;
        self.searchBookTextChange = searchBookTextChange;


        function queryMemberSearch(query) {
            var results = query ? self.members.filter(createFilterFor(query)) : self.members,
                deferred;
            if (self.simulateQuery) {
                deferred = $q.defer();
                $timeout(function () { deferred.resolve(results); }, Math.random() * 1000, false);
                return deferred.promise;
            } else {
                return results;
            }
        }
        function queryBookSearch(query) {
            var results = query ? self.books.filter(createFilterFor(query)) : self.books,
                deferred;
            if (self.simulateQuery) {
                deferred = $q.defer();
                $timeout(function () { deferred.resolve(results); }, Math.random() * 1000, false);
                return deferred.promise;
            } else {
                return results;
            }
        }
       
        function searchMemberTextChange(text) {
            $log.info('Text changed to ' + text);
        }
        
        function selectedMemberItemChange(item) {
            self.member = item;
            $log.info('Item changed to ' + JSON.stringify(item));
        }

        function selectedBookItemChange(item) {
            self.book = item;
            $log.info('Item changed to ' + JSON.stringify(item));
        }

        function searchBookTextChange(item) {
            $log.info('Text changed to ' + text);
        }

        //Create filter function for a query string

        function createFilterFor(query) {
            var lowercaseQuery = query.toLowerCase();

            return function filterFn(item) {
                return (item.value.indexOf(lowercaseQuery) === 0);
            };

        }


    }

})(angular.module('NgoApp'));