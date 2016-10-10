function RemoveClass(obj) {
   
    if ($(obj).val() != "")
        $(obj).removeClass("error");
    else
        $(obj).addClass("error");
}
//function Radiobtn() {
//    //alert(obj.type);


//    //if (obj.type == "radio" && obj.checked == true)
//    //    {
//    //        alert(obj.checked == true);
//    //        $(obj).parent().addClass("selectbox");
//    //    }

//    var inputElems = document.getElementsByName("g2")
//    for (var i = 0; i < inputElems.length; i++) {
//        if (inputElems[i].type == "radio" && inputElems[i].checked == true) {
//            $(inputElems[i]).parent().addClass("selectbox");
//        }
//        else {
//            $(inputElems[i]).parent().removeClass("selectbox");
//        }
//    }
//}