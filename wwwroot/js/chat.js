﻿class Messagess {
    constructor(username, text, when) {
        this.userName = username;
        this.text = text;
        this.when = when;
    }
}

// userName is declared in razor page.
const username = userName;
const textInput = document.getElementById('messageText');
const whenInput = document.getElementById('when');
const chat = document.getElementById('site');
const messagesQueue = [];

document.getElementById('submitButton').addEventListener('click', () => {
    var currentDate = new Date();
    whenInput.value = currentDate.getDate();
});

function clearInputField() {
    messagesQueue.push(textInput.value);
    textInput.value = "";
}

function sendMessage() {
    let text = messagesQueue.shift() || "";
    if (text.trim() === "") return;

    let when = new Date();
    let messagess = new Messagess(username, text, when);
    sendMessageToHub(messagess);
}

function addMessageToChat(messagess) {
    let isCurrentUserMessage = messagess.userName === username;

    let container = document.createElement('div');
    container.className = isCurrentUserMessage ? "container darker" : "container";

    let sender = document.createElement('p');
    sender.className = "sender";
    sender.innerHTML = messagess.userName;
    console.log(sender);
    let text = document.createElement('p');
    text.innerHTML = messagess.text;

    let when = document.createElement('span');
    when.className = isCurrentUserMessage ? "time-left" : "time-right";
    var currentDate = new Date();
    when.innerHTML = currentDate.getDate();

    container.appendChild(sender);
    container.appendChild(text);
    container.appendChild(when);
    chat.appendChild(container);
}
