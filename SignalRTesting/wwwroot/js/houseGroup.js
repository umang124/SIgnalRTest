let lbl_houseJoined = document.getElementById("lbl_houseJoined");

let btn_subs_gryffindor = document.getElementById("btn_subs_gryffindor");
let btn_subs_slytherin = document.getElementById("btn_subs_slytherin");
let btn_subs_hufflepuff = document.getElementById("btn_subs_hufflepuff");
let btn_subs_ravenclaw = document.getElementById("btn_subs_ravenclaw");

let btn_unsubs_gryffindor = document.getElementById("btn_unsubs_gryffindor");
let btn_unsubs_slytherin = document.getElementById("btn_unsubs_slytherin");
let btn_unsubs_hufflepuff = document.getElementById("btn_unsubs_hufflepuff");
let btn_unsubs_ravenclaw = document.getElementById("btn_unsubs_ravenclaw");

let btn_trig_gryffindor = document.getElementById("btn_trig_gryffindor");
let btn_trig_slytherin = document.getElementById("btn_trig_slytherin");
let btn_trig_hufflepuff = document.getElementById("btn_trig_hufflepuff");
let btn_trig_ravenclaw = document.getElementById("btn_trig_ravenclaw");

// create connection
var connectionHouseGroup = new signalR.HubConnectionBuilder().withUrl("/hubs/houseGroup").build();

btn_subs_gryffindor.addEventListener("click", function (event) {
    connectionHouseGroup.send("JoinHouse", "Gryffindor")
    event.preventDefault();
});

btn_subs_slytherin.addEventListener("click", function (event) {
    connectionHouseGroup.send("JoinHouse", "Slytherin")
    event.preventDefault();
});

btn_subs_hufflepuff.addEventListener("click", function (event) {
    connectionHouseGroup.send("JoinHouse", "Hufflepuff")
    event.preventDefault();
});

btn_subs_ravenclaw.addEventListener("click", function (event) {
    connectionHouseGroup.send("JoinHouse", "Ravenclaw")
    event.preventDefault();
});

// 

btn_unsubs_gryffindor.addEventListener("click", function (event) {
    connectionHouseGroup.send("LeaveHouse", "Gryffindor")
    event.preventDefault();
});

btn_unsubs_slytherin.addEventListener("click", function (event) {
    connectionHouseGroup.send("LeaveHouse", "Slytherin")
    event.preventDefault();
});

btn_unsubs_hufflepuff.addEventListener("click", function (event) {
    connectionHouseGroup.send("LeaveHouse", "Hufflepuff")
    event.preventDefault();
});

btn_unsubs_ravenclaw.addEventListener("click", function (event) {
    connectionHouseGroup.send("LeaveHouse", "Ravenclaw")
    event.preventDefault();
});

//

btn_trig_gryffindor.addEventListener("click", function (event) {
    connectionHouseGroup.send("TriggerHouseNotify", "Gryffindor")
    event.preventDefault();
});

btn_trig_slytherin.addEventListener("click", function (event) {
    connectionHouseGroup.send("TriggerHouseNotify", "Slytherin")
    event.preventDefault();
});

btn_trig_hufflepuff.addEventListener("click", function (event) {
    connectionHouseGroup.send("TriggerHouseNotify", "Hufflepuff")
    event.preventDefault();
});

btn_trig_ravenclaw.addEventListener("click", function (event) {
    connectionHouseGroup.send("TriggerHouseNotify", "Ravenclaw")
    event.preventDefault();
});

//

connectionHouseGroup.on("triggerHouseNotification", (houseName) => {
    toastr.success(`A new notification for ${houseName} has been launched`);
});

connectionHouseGroup.on("newMemberAddedToHouse", (houseName) => {
    toastr.success(`Member has subscribed to ${houseName}`);
});

connectionHouseGroup.on("newMemberRemovedFromHouse", (houseName) => {
    toastr.warning(`Member has unsubscribed from ${houseName}`);
});

connectionHouseGroup.on("subscriptionStatus", (strGroupJoined, houseName, hasSubscribed) => {
    lbl_houseJoined.innerText = strGroupJoined;

    if (hasSubscribed) {
        // subscribe to
        switch (houseName) {
            case 'slytherin':
                btn_subs_slytherin.style.display = "none";
                btn_unsubs_slytherin.style.display = "";
                break;
            case 'gryffindor':
                btn_subs_gryffindor.style.display = "none";
                btn_unsubs_gryffindor.style.display = "";
                break;
            case 'hufflepuff':
                btn_subs_hufflepuff.style.display = "none";
                btn_unsubs_hufflepuff.style.display = "";
                break;
            case 'ravenclaw':
                btn_subs_ravenclaw.style.display = "none";
                btn_unsubs_ravenclaw.style.display = "";
                break;
        }
        toastr.success(`You have Subscribed Successfully. ${houseName}`);
    }
    else {
        // unsubscribe
        switch (houseName) {
            case 'slytherin':
                btn_subs_slytherin.style.display = "";
                btn_unsubs_slytherin.style.display = "none";
                break;
            case 'gryffindor':
                btn_subs_gryffindor.style.display = "";
                btn_unsubs_gryffindor.style.display = "none";
                break;
            case 'hufflepuff':
                btn_subs_hufflepuff.style.display = "";
                btn_unsubs_hufflepuff.style.display = "none";
                break;
            case 'ravenclaw':
                btn_subs_ravenclaw.style.display = "";
                btn_unsubs_ravenclaw.style.display = "none";
                break;
        }
        toastr.success(`You have unsubscribed Successfully. ${houseName}`);
    }
})



// start connection
function fulfilled() {
    
}

function rejected() {

}

connectionHouseGroup.start().then(fulfilled, rejected);