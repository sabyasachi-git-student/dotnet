﻿$(document).ready(function () {
    $("input[type=text][id*=TextBox1]").attr("disabled", true);
    $("input[type=checkbox][id*=Chk_Action]").click(function () {
        if (this.checked)
            $(this).closest("tr").find("input[type=text][id*=TextBox1]").attr("disabled", false);
        else
            $(this).closest("tr").find("input[type=text][id*=TextBox1]").attr("disabled", true);
    });
});