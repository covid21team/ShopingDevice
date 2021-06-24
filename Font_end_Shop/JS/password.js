
  function myFunction(){
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
  var x =document.getElementById("pass");
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

function login(){
  var x =document.getElementById("pass");
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

