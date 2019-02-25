(function (app) {

    app.controller('bookCtrl', bookCtrl);

    bookCtrl.$inject = ['$scope', 'apiService', '$window'];

    function bookCtrl($scope, apiService, $window) {
        var self = this;

        init();

        function init() {
            apiService.get('https://localhost:5001/api/book', null, loadBook, failedBook);

        }

        function loadBook(result) {
            self.books = result.data;
            angular.element(document).ready(function () {
                bookTable = $('#book_table');
                bookTable.DataTable();
            });

        }

        function failedBook(reponse) {
            console.log(response);
        }




        self.edit = function (book) {
            $window.location.pathname = 'Book/Edit/' + book.id;
        };

        self.delete = function (book) {
            $window.location.pathname = 'Book/Delete/' + book.id;
        };

        self.detail = function (book) {
            $window.location.pathname = 'Book/Details/' + book.id;
        };

    }

})(angular.module('NgoApp'));