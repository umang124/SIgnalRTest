// create connection
var connectionChat = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/chat")
    .withAutomaticReconnect([0, 1000, 5000, null])
    .build();

//connectionChat.on("ReceiveUserConnected", function (userId, userName, isOldConnection) {
//    if (!isOldConnection) {
//        addMessage(`${userName} is online`);
//    }
//});




function addMessage(msg) {
    if (msg == null && msg == '') {
        return;
    }
    let ui = document.getElementById('messageList');
    let li = document.createElement("li");
    li.innerHTML = msg;
    ui.appendChild(li);
}

connectionChat.start();