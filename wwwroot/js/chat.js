"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("LoadMessages", function (previousMessages) {
    document.getElementById("messages-spinner").style.display = "none";
    for (let messageDetail of previousMessages) {
        let details = JSON.parse(messageDetail);
        var li = document.createElement("li");
        document.getElementById("messagesList").appendChild(li);
        li.textContent = `${details.sentBy} says ${details.messageText}`;
    }
});

connection.on("ReceiveMessage", function (user, message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.textContent = `${user} says ${message}`;
});

connection.on("ShowEmptyError", function () {
    alert("Fill out both user and message fields.");
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
    connection.invoke("RetrieveData");
    document.getElementById("messages-spinner").style.display = "block";
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});