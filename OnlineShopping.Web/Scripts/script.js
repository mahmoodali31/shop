$(document).ready(function () {
    $.get("CategoryList/Index", function (data) {
        //alert(data);
        $("#category").append(data);
    });
    $.get("ProductList/Index", function (data) {
        //alert(data);
        $("#product").append(data);
    });

    //var productId = $(this).attr('data-product-id');

    //url = "Product/AddItemToCart/?productId=" + productId;

    //$.get(url, function (data) {

    //    console.log(data);
    //    $("#count").empty();
    //    $("#count").append(data);


    //});
    url = "Product/NoOfItemInTheCart";
    $.get(url, function (data) {

        console.log(data);
        $("#count").empty();
        $("#count").append(data);


    });

});
$(document).on('click', '.category', function () {
    $("#slider").hide();
   
    var categoryId = $(this).attr('data-cateogry-id');
    url = "Product/ProductByCategoryId/?FKCatId="+categoryId;
    console.log(categoryId); $("#product").empty();
    $.get(url, function (data) {
      
        $("#product").append(data);


    });
  

});


$(document).on('click', '.product', function () {


    var productId = $(this).attr('data-product-id');

    url = "Product/AddItemToCart/?productId=" + productId;

    $.get(url, function (data) {

        console.log(data);
        $("#count").empty();
        $("#count").html(data);


    });
    //console.log(data);
    //alert(data);

});


$(document).on('click', '.delProduct', function () {


    var productId = $(this).attr('data-product-id');
    url = "RemoveItemFromCart/?itemId="+ productId;
    console.log(productId); 
    $.get(url, function (data) {
        console.log(data);
        $("#cartItem").empty();
        //location.reload();
        $("#cartItem").html(data);
        UpdateItemInCart();
       
     
       


    });

   
});

function UpdateItemInCart()
{
    url = "NoOfItemInTheCart";
    $.get(url, function (data) {

        console.log(data);
        $("#count").empty();
        $("#count").append(data);


    });
}