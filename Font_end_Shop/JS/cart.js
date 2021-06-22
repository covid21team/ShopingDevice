//var newcurr = accounting.formatMoney(number,"", 0, ",", "."); // 4300 => 4,300
//var number = Number(currency.replace(/[^0-9.-]+/g,""));       // 4,300 => 4300

/* Set rates + misc */
var taxRate = 0.05;
var shippingRate = 0; 
var fadeTime = 0;


/* Assign actions */
$('.number_car input').change( function() {
  updateQuantity(this);
});

$('.product-removal button').click( function() {
  removeItem(this);
});


$('.checkbox_cart input').change( function(){
  updatePrice(this);
});

/* Recalculate cart */
function recalculateCart(price_product)
{
  var subtotal = 0;
  
  /* Sum up row totals */
  $('.product_cart').each(function () {
    subtotal += price_product;
  });
 
  
  /* Calculate totals */
  var tax = subtotal * taxRate;
  var shipping = (subtotal > 0 ? shippingRate : 0);
  var total = subtotal + tax + shipping;
  
  /* Update totals display */
  $('.totals_value').fadeOut(fadeTime, function() {

    var temp_subtotal = accounting.formatMoney(subtotal,"", 0, ",", "."); // 4300 => 4,300
    $('#subtotal_cart').html(temp_subtotal + "đ");

    var temp_tax = accounting.formatMoney(tax,"", 0, ",", "."); // 4300 => 4,300
    $('#tax_cart').html(temp_tax + "đ");

    if(shipping == 0){
      $('#shipping_cart').html("Free Ship");
    }else{
      var temp_shipping = accounting.formatMoney(shipping,"", 0, ",", "."); // 4300 => 4,300
      $('#shipping_cart').html(temp_shipping + "đ");
    }

    var temp_total = accounting.formatMoney(total,"", 0, ",", "."); // 4300 => 4,300
    $('#total_cart').html(temp_total + "đ");

    if(total == 0){
      $('.checkout_cart').fadeOut(fadeTime);
    }else{
      $('.checkout_cart').fadeIn(fadeTime);
    }

    $('.totals_value').fadeIn(fadeTime);
    $('.table_payment td').css({"width": "220px"});
  });
}


function updatePrice(quantityInput)
{
  /* Calculate line price */
  var productRow = $(quantityInput).parent().parent();
  var price_temp = productRow.children().children('.product_price_cart').text();
  var price = Number(price_temp.replace(/[^0-9.-]+/g,"")); // 4,300 => 4300
  var quantity = 0;

  var checkedtemp = quantityInput.checked;

  if(checkedtemp == false)
    quantity = 0;
  else
    quantity = parseInt(productRow.children('.number_car').children('.number_quantity_cart').val());
  
  var linePrice = price * quantity;

  /* Update line price display and recalc cart totals */
  productRow.children().children('.product_line_price_cart').each(function () {
    $(this).fadeOut(fadeTime, function() {
      var temp_linePrice = accounting.formatMoney(linePrice,"", 0, ",", "."); // 4300 => 4,300
      $(this).text(temp_linePrice + "đ");
      recalculateCart(linePrice);
      $(this).fadeIn(fadeTime);
    });
  });  
}

/* Update quantity */
function updateQuantity(quantityInput)
{
  /* Calculate line price */
  var productRow = $(quantityInput).parent().parent();
  var price_temp = productRow.children().children('.product_price_cart').text();
  var price = Number(price_temp.replace(/[^0-9.-]+/g,"")); // 4,300 => 4300
  var quantity = $(quantityInput).val();
  var linePrice = price * quantity;
  
/* Update line price display and recalc cart totals */
  productRow.children().children('.product_line_price_cart').each(function () {
    $(this).fadeOut(fadeTime, function() {
      var temp_linePrice = accounting.formatMoney(linePrice,"", 0, ",", "."); // 4300 => 4,300
      $(this).text(temp_linePrice + "đ");
      recalculateCart(linePrice);
      $(this).fadeIn(fadeTime);
    });
  });  
}


/* Remove item from cart */
function removeItem(removeButton)
{
  /* Remove row from DOM and recalc cart total */
  var productRow = $(removeButton).parent().parent();
  productRow.slideUp(fadeTime, function() {
    productRow.remove();
    recalculateCart();
  });
}