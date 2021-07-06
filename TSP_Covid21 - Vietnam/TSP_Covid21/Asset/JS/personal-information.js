
function myFunction() {
    var checkBox = document.getElementById("id7");
    var text = document.getElementById("passan");
    if (checkBox.checked == true) {
        text.style.display = "block";
    } else {
        text.style.display = "none";
    }
}


// function Functionclickthongtinkhachhang(){
//     var modal1 = document.getElementById('banthongtincanhan'); 
//     modal1.style.display = "block";
// }

// $("#1").click(function(){
//     $('#1').css({"border": "#d6dee9"});

// });
function openCity(evt, cityName) {
    var i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("thongtincanhan");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }
    document.getElementById(cityName).style.display = "block";
    evt.currentTarget.className += " active";
}


function openthem() {
    var modal = document.getElementById('themthongtingiaohang');
    var modal1 = document.getElementById('tatbangiaohang');
    modal.style.display = "block";
    modal1.style.display = "none";

}
function tatthem() {
    var modal = document.getElementById('themthongtingiaohang');
    var modal1 = document.getElementById('tatbangiaohang');
    modal.style.display = "none";
    modal1.style.display = "block";

}
function okthem() {
    var modal = document.getElementById('themthongtingiaohang');
    var modal1 = document.getElementById('tatbangiaohang');
    modal.style.display = "none";
    modal1.style.display = "block";

}
function suathem() {
    var modal = document.getElementById('themthongtingiaohang');
    var modal1 = document.getElementById('tatbangiaohang');
    modal.style.display = "block";
    modal1.style.display = "none";

}
// function openthem(evt, cityName) {
//   var i, tabcontent, tablinks;
//   tabcontent = document.getElementsByClassName("tabcontent");
//   for (i = 0; i < tabcontent.length; i++) {
//     tabcontent[i].style.display = "none";
//   }
//   tablinks = document.getElementsByClassName("tablinks");
//   for (i = 0; i < tablinks.length; i++) {
//     tablinks[i].className = tablinks[i].className.replace(" active", "");
//   }
//   document.getElementById(cityName).style.display = "block";
//   evt.currentTarget.className += " active";
// }

function changeFunc() {
    var selectedBox = document.getElementById("province")
    var selectedValue = selectedBox.options[selectedBox.selectedIndex].getAttribute('data-province')
    var district = document.getElementById('district');
    $.ajax({
        url: "https://dev-online-gateway.ghn.vn/shiip/public-api/master-data/district",
        headers: {
            'token': "260fcbb6-d4ae-11eb-aa92-d25db748eae9",
            'content-Type': 'application/json'
        },
        method: 'GET',
        dataType: 'json',

        success: function (response) {

            var str = "<option selected>District </option>";
            for (var i = 0; i < response.data.length; i++) {
                if (response.data[i].ProvinceID == selectedValue) {
                    str = str + "<option class='districtID' data-district='" + response.data[i].DistrictID + "'>" + response.data[i].DistrictName
                        + "</option>"
                }

            }
            district.innerHTML = str;
        }
    });

};


function changeFuncDistrict() {
    var selectedBox = document.getElementById("district")
    var selectedValue = selectedBox.options[selectedBox.selectedIndex].getAttribute('data-district')
    var district = document.getElementById('ward');
    $.ajax({
        url: "https://dev-online-gateway.ghn.vn/shiip/public-api/master-data/ward?district_id=" + selectedValue,
        headers: {
            'token': "260fcbb6-d4ae-11eb-aa92-d25db748eae9",
            'content-Type': 'application/json'
        },
        method: 'GET',
        dataType: 'json',
        success: function (response) {
            console.log(response.data);
            var str = "<option selected>Wards</option>";
            for (var i = 0; i < response.data.length; i++) {
                str = str + "<option class='wardID' data-ward='" + response.data[i].WardCode + "'>" + response.data[i].WardName
                    + "</option>"
            }
            district.innerHTML = str;
        }
    });
}