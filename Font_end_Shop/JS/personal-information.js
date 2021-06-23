
function myFunction() {
  var checkBox = document.getElementById("id7");
  var text = document.getElementById("passan");
  if (checkBox.checked == true){
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


  function openthem(){
    var modal = document.getElementById('themthongtingiaohang');  
    var modal1 = document.getElementById('tatbangiaohang');
    modal.style.display = "block";
    modal1.style.display = "none";

}
function tatthem(){
  var modal = document.getElementById('themthongtingiaohang');  
  var modal1 = document.getElementById('tatbangiaohang');
  modal.style.display = "none";
  modal1.style.display = "block";

}
function okthem(){
  var modal = document.getElementById('themthongtingiaohang');  
  var modal1 = document.getElementById('tatbangiaohang');
  modal.style.display = "none";
  modal1.style.display = "block";

}
function suathem(){
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