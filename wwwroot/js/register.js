"use strict"

let connection = new signalR.HubConnectionBuilder().withUrl("/loginHub").build();

connection.on("UserExistsError", function () {
    document.getElementById("takenError").style.display = "block";
});

connection.on("PassMatchError", function () {
    document.getElementById("matchError").style.display = "block";
});

connection.on("UserRegistered", function () {
    alert("You have been successfully registered!");
});

connection.start().then(function () {
    console.log("Connection established");
}).catch(function () {
    return console.error(err.toString());
});

document.getElementById("registerBtn").addEventListener('click', function (e) {
    e.preventDefault();
    let username = document.getElementById("username").value;
    let pass = document.getElementById("pass").value;
    let confirmPass = document.getElementById("confirmPass").value;
    connection.invoke("RegisterUser", username, pass, confirmPass);
});

document.getElementById("resetBtn").addEventListener('click', function () {
    document.getElementById("takenError").style.display = "none";
    document.getElementById("matchError").style.display = "none";
})