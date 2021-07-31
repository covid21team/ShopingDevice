
// Get the modal
var modal = document.getElementById('productDetail');
var modal1 = document.getElementById('AddProduct');
var modal2 = document.getElementById('EditProduct');

var modal3 = document.getElementById('UpPic');

// When the user clicks anywhere outside of the modal, close it
window.onclick = function(event) {
    if (event.target == modal || event.target == modal1 || event.target == modal2) {
        modal.style.display = "none";
        modal1.style.display = "none";
        modal2.style.display = "none";
    }

    if (event.target == modal3) {
        modal3.style.display = "none";
    }
}




function Functionclick(){
    var modal1 = document.getElementById('id02');
    var modal = document.getElementById('id03');
    modal.style.display = "none";
    modal1.style.display = "none";
}

function Functionclick1(){
    var modal1 = document.getElementById('table1');
    var modal2 = document.getElementById('table2');
    modal1.style.display = "block";
    modal2.style.display = "none";
    $(".btn_addConfig").css({ "background": "white" });
    $(".edit").css({ "background": "#d6dee9" });
}
function Functionclick2(){
    var modal1 = document.getElementById('table1');
    var modal2 = document.getElementById('table2');
    modal2.style.display = "block";
    modal1.style.display = "none";
    $(".btn_addConfig").css({ "background": "#d6dee9" });
    $(".edit").css({ "background": "white" });

}
function Functionclick3(){
    var modal1 = document.getElementById('id03');
 
    modal1.style.display = "block";
   
}

function Functionclick4(){

    var y = document.getElementById("country1")
    var x = document.getElementById("TxtBoxBrand");
    if (x.style.display === "block") {
        x.style.display = "none";
      y.style.display = "block";
    
    } else {
        x.style.display = "block";
        y.style.display = "none";
    
    }
   
}

function Functionclick5(){

    var x = document.getElementById("TxtBoxBrand");
      x.style.display = "none";
   
}

$("#edit").click(function(){
    $('#edit').css({"background": "#d6dee9"});
       $('#btn_addConfig').css({"background": "white"});
});

$("#btn_addConfig").click(function(){
    $('#edit').css({"background": "white"});
       $('#btn_addConfig').css({"background": "#d6dee9"});
});

