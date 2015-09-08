app.factory('secureCartService', function ($http) {
    return {

        GetTaskLogs: function (taskId) {

            return $http.post("/Tasks/GetTaskLogs", {
                taskId: taskId
            });
        }
    } //return exposed APIs
});