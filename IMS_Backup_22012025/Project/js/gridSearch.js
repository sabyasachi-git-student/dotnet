$(function () {
    $('.search_textbox').each(function (i) {
        $(this).quicksearch("[id*=gv_itempurchasepricelist] tr:not(:has(th))", {
            'testQuery': function (query, txt, row) {
                return $(row).children(":eq(" + i + ")").text().toLowerCase().indexOf(query[0].toLowerCase()) != -1;
            }
        });
    });
});