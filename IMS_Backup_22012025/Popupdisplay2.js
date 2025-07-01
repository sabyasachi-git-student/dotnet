

window.document.onkeydown = function (e) {
            if (!e) {
                e = event;
            }
            if (e.keyCode == 27) {
                lightbox_close();
            }
        }
        debugger;
function lightbox_open(Divname) {
    window.scrollTo(0, 0);
    debugger;
  //  document.getElementById(Divname).className = "light";
    debugger;
    document.getElementById(Divname).style.display = 'block';
    document.getElementById('fade').style.display = 'block';
    return false;
}
function lightbox_close() {
    document.getElementById('light').style.display = 'none';
    document.getElementById('fade').style.display = 'none';
    return false;
}




