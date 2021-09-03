/*Tắt form không muốn sử dụng*/
var modal = document.getElementById('Bets_Parent');
    
window.onclick = function (event) {
    if (event.target == modal) {
            $("#Text_Bets").val("");
        modal.style.display = "none";
    }
}

/*Load bàn cờ*/
var isChessBoard = true;
reload();
function reload() {
    setInterval(function () {
        loadChessBoard();
    }, 2000);
}

//@Url.Action("ExitJoinRoom", "BauCua")
function exitRoom(playerId, url) {
    $.ajax({
        url: url,
        type: 'POST',
        data: {
            playerId: playerId,
        },
        success: function (result) {
            isChessBoard = false;
            var create = document.createElement("div");

            $("#Frame_ListRoom").html(create)
            $("#Frame_ListRoom div").html(result)
            $("#Frame_Room").css({ "display": "none" });
            $("#Frame_ListRoom").css({ "display": "block" });
            $("#Frame_Room div").remove();
        }
    })
}

    $("#Exit_Room").click(function () {
                $.ajax({
                    url: '@Url.Action("ExitJoinRoom", "BauCua")',
                    type: 'POST',
                    data: {
                        playerId: @Model,
        },
            success: function (result) {
                isChessBoard = false;
            var create = document.createElement("div");

            $("#Frame_ListRoom").html(create)
            $("#Frame_ListRoom div").html(result)
                $("#Frame_Room").css({"display": "none" });
                $("#Frame_ListRoom").css({"display": "block" });
            $("#Frame_Room div").remove();
        }
    })
})

    $("#Bets_Animal").click(function () {
        var animalId = $("#Select_Animal").text();
            var value = $("#Text_Bets").val();
        $.ajax({
                url: "@Url.Action("Bets", "BauCua")",
            type: "POST",
            data: {
                animalId: animalId,
            playerId: @Model,
            betsValue: value,
            roomId: @roomId,
        },
            success: function (result) {
                if (result == "0") {
                alert("Không đủ tiền cược");
            } else {
                loadChessBoard();
            }
        },
            error: function () {
                alert("Error_JoinRoom_BetsAnimal");
            }
        })
    })

    function loadChessBoard() {
        if (isChessBoard == true) {
                $.ajax({
                    url: '@Url.Action("CheckChessBoard","BauCua")',
                    type: "POST",
                    data: {
                        roomId: @roomId,
        },
                success: function (result) {
                    if (result == 'True') {
                $.ajax({
                    url: '@Url.Action("LoadRoom", "BauCua")',
                    type: 'POST',
                    data: {
                        roomId: @roomId,
        },
                            success: function (result) {
                $("#Chess_Board").html(result);
            },
                            error: function () {
                alert("Error_JoinRoom_Reload()_True");
            }
        })
                    } else {
                isChessBoard = false;
            $.ajax({
                url: '@Url.Action("ListRoom","BauCua")',
            type: 'POST',
                            success: function (result) {
                alert("Chủ phòng đã xóa phòng!!");
            var create = document.createElement("div");

            $("#Frame_ListRoom").html(create)
            $("#Frame_ListRoom div").html(result)
                                $("#Frame_Room").css({"display": "none" });
                                $("#Frame_ListRoom").css({"display": "block" });
        },
                            error: function () {
                alert("Error_JoinRoom_Reload()_False");
            }
        })
    }
},
                error: function () {
                alert("Error_JoinRoom_Reload()");
            }
        })
    }
}


        <!--Xác nhận màu của player-- >

                var start = {
                    loadData: function () {
                    BorderColor($("#Player_01 img"), @player_01.COLORID);
                BorderColor($("#Player_02 img"), @player_02.COLORID);
                BorderColor($("#Player_03 img"), @player_03.COLORID);
                BorderColor($("#Player_04 img"), @player_04.COLORID);
                BorderColor($("#Player_05 img"), @player_05.COLORID);
                BorderColor($("#Player_06 img"), @player_06.COLORID);
            }
        }
        start.loadData();
    
    function BorderColor(t, color) {
        switch (color) {
            case 1: {
                    t.css({ "border": "5px solid red" })
                } break;
            case 2: {
                    t.css({ "border": "5px solid yellow" })
                } break;
            case 3: {
                    t.css({ "border": "5px solid green" })
                } break;
            case 4: {
                    t.css({ "border": "5px solid blue" })
                } break;
            case 5: {
                    t.css({ "border": "5px solid gray" })
                } break;
            case 6: {
                    t.css({ "border": "5px solid violet" })
                } break;
            }
        }

                function Deer() {
                    alert("h");
                $("#Bets_Parent").css({"display": "block" });
                $("#Select_Id").text("1");
            }
        
    $("#Deer").click(function () {
                    alert("s");
                $("#Bets_Parent").css({"display": "block" });
                $("#Select_Id").text("1");
        
        /*$("#Deer_Player_01").css({"display": "inline-block", "border": "3px solid green"});
                $("#Deer_Player_01").text("1");
        
        $("#Deer_Player_02").css({"display": "inline-block", "border": "3px solid violet" });
                $("#Deer_Player_02").text("1");
        
        $("#Deer_Player_03").css({"display": "inline-block", "border": "3px solid red" });
                $("#Deer_Player_03").text("1");
        
        $("#Deer_Player_04").css({"display": "inline-block", "border": "3px solid yellow" });
                $("#Deer_Player_04").text("1");
        
        $("#Deer_Player_05").css({"display": "inline-block", "border": "3px solid gray" });
                $("#Deer_Player_05").text("1");
        
        $("#Deer_Player_06").css({"display": "inline-block", "border": "3px solid blue" });
                $("#Deer_Player_06").text("1");*/
            })