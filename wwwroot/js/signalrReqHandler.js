const { signalR } = require("./signalr/dist/browser/signalr");

var connection = new signalR.HubConnectionBuilder().withUrl("/chathub").build();

connection.on("ReceiveMessage", addMessageToChat);

connection.start()
    .catch(error => {
        console.error(error.messagess);
    });

function sendMessageToHub(messagess) {
    connection.invoke("SendMessage", messagess);
}