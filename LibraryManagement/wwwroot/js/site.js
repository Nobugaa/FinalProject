"use strict";

// Create SignalR connection
var connection = new signalR.HubConnectionBuilder()
    .withUrl("/SignalrServer") // Match the hub route
    .build();

// Reload the page when a new book is added
connection.on("LoadALL", function () {
    console.log("Reloading  list...");
    location.reload();
});

// Start connection
connection.start().catch(function (err) {
    console.error("Error connecting to SignalR:", err.toString());
});
