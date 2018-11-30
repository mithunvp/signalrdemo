//1
let connection = new signalR.HubConnectionBuilder().withUrl("/fileWatcherHub").build();

//2
function getConnected() {
    connection.start().then(function () {
        document.getElementById('connectAlertDiv').style.display = "block";
        document.getElementById('btnConnnect').style.display = "none";       
        TestConnection();
    }).catch(function (err) {
        return console.error(err.toString());
    });
}
//3
function TestConnection() {
    connection.invoke("NotifyConnection").catch(function (err) {
        return console.error(err.toString());
    });
}

//4
connection.on("TestBrodcasting", function (time) {   
    document.getElementById('broadcastDiv').innerHTML = time;
    document.getElementById('broadcastDiv').style.display = "block";
});

connection.on("onFileChange", function (filedetails) {
    let liStr = `File: '${filedetails.name}' got '${filedetails.changeType}'`;

    let li = document.createElement("li");
    li.className = "list-group-item d-flex justify-content-between align-items-center"
    li.textContent = liStr;
    document.getElementById("fileList").appendChild(li);
   
});