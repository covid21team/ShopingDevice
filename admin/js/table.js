
// Get the modal
var modal = document.getElementById('id01');

var modal1 = document.getElementById('id02');

// When the user clicks anywhere outside of the modal, close it
window.onclick = function(event) {
    if (event.target == modal||event.target==modal1) {
        modal.style.display = "none";
        modal1.style.display = "none";
    }
}
function Functionclick(){
    var modal1 = document.getElementById('id02');
    modal1.style.display = "none";
}

function Functionclick1(){
    var modal1 = document.getElementById('table1');
    var modal2 = document.getElementById('table2');
    modal1.style.display = "block";
    modal2.style.display = "none";

}function Functionclick2(){
    var modal1 = document.getElementById('table1');
    var modal2 = document.getElementById('table2');
    modal2.style.display = "block";
    modal1.style.display = "none";

}
function Functionclick3(){
    var modal1 = document.getElementById('id03');
 
    modal1.style.display = "block";
   
}


