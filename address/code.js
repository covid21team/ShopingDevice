document.addEventListener("DOMContentLoaded",function(){
    var province = document.getElementById("province");
    window.onload=function(){
        $.ajax({
            url: "https://dev-online-gateway.ghn.vn/shiip/public-api/master-data/province",
            headers:{
                'token' : "260fcbb6-d4ae-11eb-aa92-d25db748eae9",
                'content-Type':'application/json'
            },
            method:'GET',
            dataType:'json',
            success:function(response){
                var str = "<option selected>Tỉnh Thành</option> ";
                for(var i=0;i<response.data.length;i++){
                    str=str+ "<option class = 'provinceID' data-province='"+response.data[i].ProvinceID+"'>"+response.data[i].ProvinceName+"</option> "
                }
                province.innerHTML=str;
            }
        });
    }
},false)

function changeFunc(){
    var selectedBox = document.getElementById("province")
    var selectedValue = selectedBox.options[selectedBox.selectedIndex].getAttribute('data-province')
    var district = document.getElementById('district');
    $.ajax({
        url: "https://dev-online-gateway.ghn.vn/shiip/public-api/master-data/district",
        headers:{
            'token' : "260fcbb6-d4ae-11eb-aa92-d25db748eae9",
            'content-Type':'application/json'
        },
        method:'GET',
        dataType:'json',

        success:function(response){

            var str="<option selected>Quận huyện</option>";
            for(var i=0;i<response.data.length;i++){
                if(response.data[i].ProvinceID==selectedValue)
                {
                    str=str+"<option class='districtID' data-district='"+response.data[i].DistrictID+"'>"+response.data[i].DistrictName
                    +"</option>"
                }

            }
            district.innerHTML=str;
        }
    });

};


function changeFuncDistrict(){
    var selectedBox = document.getElementById("district")
    var selectedValue = selectedBox.options[selectedBox.selectedIndex].getAttribute('data-district')
    var district = document.getElementById('ward');
    $.ajax({
        url: "https://dev-online-gateway.ghn.vn/shiip/public-api/master-data/ward?district_id="+selectedValue,
        headers:{
            'token' : "260fcbb6-d4ae-11eb-aa92-d25db748eae9",
            'content-Type':'application/json'
        },
        method:'GET',
        dataType:'json',
        success:function(response){
            console.log(response.data);
            var str="<option selected>Phường Xã</option>";
            for(var i=0;i<response.data.length;i++){
                    str=str+"<option class='wardID' data-ward='"+response.data[i].WardCode+"'>"+response.data[i].WardName
                    +"</option>"
            }
            district.innerHTML=str;
        }
    });

}



