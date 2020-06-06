
//Document Ready Fuction
$(document).ready(function () {

});


//For Image Preview
    function readURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#imagePreview').css('background-image', 'url(' + e.target.result + ')');
                $('#imagePreview').hide();
                $('#imagePreview').fadeIn(650);
            }
            reader.readAsDataURL(input.files[0]);
        }
    }
    $("#imageUpload").change(function () {
        readURL(this);
    });
    $("#cart").on("click", function () {
        $(".shopping-cart").fadeToggle("fast");
    });

//For Image Preview 
    $(function () {
        $(document).on("change", ".uploadFile", function () {
            var uploadFile = $(this);
            var files = !!this.files ? this.files : [];
            if (!files.length || !window.FileReader) return; // no file selected, or no FileReader support

            if (/^image/.test(files[0].type)) { // only image file
                var reader = new FileReader(); // instance of the FileReader
                reader.readAsDataURL(files[0]); // read the local file

                reader.onloadend = function () { // set image data as background of div
                    //alert(uploadFile.closest(".upimage").find('.imagePreview').length);
                    uploadFile.closest(".imgUp").find('.imagePreview').css("background-image", "url(" + this.result + ")");
                }
            }

        });
    });
//For Binding data with dropdownlist  of State
    function GetState() {
 
        $.ajax({  
            url: "/home/GetJsonState",
            dataType: "json",  
            type: "GET",  
            data: { state: $("#State").val() },
            error: function () {  
            },  
            beforeSend: function () {  
            },  
            success: function (data) {  
                var items = "";  
                items = "<option value=''>Choose City</option>";
                $.each(data, function (i, item) {  
                    items += "<option value=\"" + item.Text + "\">" + item.Text + "</option>";  
                });  
                $("#DropDownListStates").html(items);
            }  
        });  
         
    }

//For Binding data with dropdownlist
    function GetProvince() {

        $.ajax({
            url: "/home/GetJsonState",
            dataType: "json",
            type: "GET",
            data: { state: $("#Province").val() },
            error: function () {
            },
            beforeSend: function () {
            },
            success: function (data) {
                var items = "";
                items = "<option value=''>Choose City</option>";
                $.each(data, function (i, item) {
                    items += "<option value=\"" + item.Value + "\">" + item.Text + "</option>";
                });
                $("#DropDownListStates1").html(items);
            }
        });

    }


    //function Review(id)
    //{
    //    $.ajax({
    //        type: "POST",
    //        url: "/Admin/Review",
    //        data: { Id: id, Review: $("#Review").val() },
    //        contentType: "application/json; charset=utf-8",
    //        dataType: "json",
    //        success: function (response) {
    //            alert("Success");
    //        },
    //        error: function (response) {

    //            console.log(response);

    //            alert("Error");
    //        }
    //    });
    //}