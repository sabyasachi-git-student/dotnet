
function DisplayModal(Divname) {
          
    //document.getElementById("overlay").style.height = document.body.clientHeight + 'px';
    //document.getElementById("overlay").className = "OverlayEffect";
    debugger;
    document.getElementById(Divname).className = "ShowModal";
    debugger;
}  
function RemoveModal() {
    document.getElementById("modalMsg").className = "HideModal";
    //document.getElementById("overlay").className = "";
    return false;
}
