(function () {
	"user strict";

    angular
        .module("common.services", ["ngResource"])
        .constant("appSettings",
        {
        	serverPath: "http://localhost:8845"
        });

}());