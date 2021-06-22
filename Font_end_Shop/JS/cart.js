
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
function recalculateCart()
{
  var subtotal = 0;
  
  /* Sum up row totals */
  $('.product_cart').each(function () {
    subtotal += parseFloat($(this).children().children('.product_line_price_cart').text());
  });
 
  
  /* Calculate totals */
  var tax = subtotal * taxRate;
  var shipping = (subtotal > 0 ? shippingRate : 0);
  var total = subtotal + tax + shipping;
  
  /* Update totals display */
  $('.totals_value').fadeOut(fadeTime, function() {
    $('#subtotal_cart').html(subtotal);
    $('#tax_cart').html(tax);
    $('#shipping_cart').html(shipping);
    $('#total_cart').html(total);
    if(total == 0){
      $('.checkout_cart').fadeOut(fadeTime);
    }else{
      $('.checkout_cart').fadeIn(fadeTime);
    }
    $('.totals_value').fadeIn(fadeTime);
    $('.table_payment td').css({"width": "200px"});
  });
}


function updatePrice(quantityInput)
{
  /* Calculate line price */
  var productRow = $(quantityInput).parent().parent();
  var price = parseFloat(productRow.children().children('.product_price_cart').text());
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
      $(this).text(linePrice);
      recalculateCart();
      $(this).fadeIn(fadeTime);
    });
  });  
}

/* Update quantity */
function updateQuantity(quantityInput)
{
  /* Calculate line price */
  var productRow = $(quantityInput).parent().parent();
  var price = parseFloat(productRow.children().children('.product_price_cart').text());
  var quantity = $(quantityInput).val();
  var linePrice = price * quantity;
  
/* Update line price display and recalc cart totals */
  productRow.children().children('.product_line_price_cart').each(function () {
    $(this).fadeOut(fadeTime, function() {
      $(this).text(linePrice);
      recalculateCart();
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