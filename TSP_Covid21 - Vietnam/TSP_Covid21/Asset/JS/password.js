function ForgotFunction1() {
    var x = document.getElementById("pass_forgot1");
    var t = document.getElementById("hidel_forgot2");
    var e = document.getElementById("hidel_forgot1");

    if (x.type === 'password') {
        x.type = 'text';
        t.style.display = "block";
        e.style.display = "none"
    } else {
        x.type = 'password';
        t.style.display = "none";
        e.style.display = "block"
    }

} 

function ForgotFunction2() {
    var x = document.getElementById("pass_forgot3");
    var t = document.getElementById("hidel_forgot4");
    var e = document.getElementById("hidel_forgot3");

    if (x.type === 'password') {
        x.type = 'text';
        t.style.display = "block";
        e.style.display = "none"
    } else {
        x.type = 'password';
        t.style.display = "none";
        e.style.display = "block"
    }

} 


function my10Function(){
    var x =document.getElementById("pass1");
    var t =document.getElementById("hidel3");
    var e =document.getElementById("hidel4");
  
    if(x.type==='password'){
        x.type ='text';
        t.style.display="block";
        e.style.display="none"
    }else{
        x.type ='password';
        t.style.display="none";
        e.style.display="block"
    }
    
} 
function my1Function(){
  var x =document.getElementById("pass2");
  var y =document.getElementById("hidel1");
  var z =document.getElementById("hidel2");

  if(x.type==='password'){
  x.type ='text';
  y.style.display="block";
  z.style.display="none"
}else{
  x.type ='password';
  y.style.display="none";
  z.style.display="block"
} 
}

function my2Function(){
    var x = document.getElementById("pass_signin");
  var y =document.getElementById("hidel5");
  var z =document.getElementById("hidel6");

  if(x.type==='password'){
  x.type ='text';
  y.style.display="block";
  z.style.display="none"
}else{
  x.type ='password';
  y.style.display="none";
  z.style.display="block"
} 
}

