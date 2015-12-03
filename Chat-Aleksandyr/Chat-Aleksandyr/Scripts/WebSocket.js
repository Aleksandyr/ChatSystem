function WebSocketExample() {
    var webSocketUrl = "ws://localhost:10609/Handlers/WebSocketHandler.ashx";
    this.Socket = new WebSocket(webSocketUrl);
    this.Socket.onopen = function () {
        console.log("open");
    };
    this.Socket.onmessage = function (evt) {
        $("#chat-container").append("<div>" + evt.data + "</div>");
        console.log(evt);
    };
    this.Socket.onerror = function (evt) {
        console.log("Error: " + evt.data);
    };
    this.Socket.onclose = function () {
        console.log("close");
    };
    this.Test = 1;
}

var socket = new WebSocketExample();

function SendMessage() {
    socket.Socket.send($("#nickName").val() + ": " + $("#message").val());
    $("#message").val("");
}