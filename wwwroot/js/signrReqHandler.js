var connection = new signalR.HubConnectionBuilder()
    .withUrl('/chatHub')
    .build();

connection.on('receiveMessage', addMessageToChat);

connection.start()
    .catch(error => {
        console.error(error.messagess);
    });

function sendMessageToHub(messagess) {
    connection.invoke('sendMessage', messagess)
}