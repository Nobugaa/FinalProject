"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/SignalrServer").build();

connection.on("LoadALL", function () {
    location.href = './Index';
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});
