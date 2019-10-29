/// <reference path="jquery-ui-1.12.1.min.js" />


$(document).ready(function() {
    $(".datepicker").datepicker({
        dateFormat: "dd.mm.yy",
        changeMonth: true,
        changeYear: true,
        yearRange: "-10:+10",
        showOn: "both",
        buttonText: "<i class='fa fa-calendar'></i>"
    });
})

$("button.delete-btn").on("click", function (e) {
    e.preventDefault();
    var id = $(this).closest('tr').attr('id');
    $('#myModal').attr('data-target', id);
    $('#myModal').modal('show');
    $('body').on("click", "#btnDeleteYes", function () {
        var id = $('#myModal').attr('data-target');
        $('#' + id).find('form').submit();
        $('#myModal').modal('hide');
    });
});



$(document).ready(function () {
    $('input[type="checkbox"').click(function () {
        var numberOfChecked = $('input:checkbox:checked').length;
        if (numberOfChecked == 0) {
            $('#deleteMultiple').css({ 'font-weight': 'lighter' });
            $('#countChecks').css({ 'font-weight': 'lighter' });
            $('#countChecks').html('No item is selected');
        }
        if (numberOfChecked == 1) {
            $('#countChecks').html('1 item selected');
            $('#deleteMultiple').css({ 'font-weight': 'bolder' });
            $('#countChecks').css({ 'font-weight': 'bolder' });
        }
        if (numberOfChecked > 1) {
            $('#countChecks').html(numberOfChecked + ' items selected');

        }
    })
});



var ConfirmDeleteMultiple = function () {
    if ($('input:checkbox:checked').length > 0) {
        $('#btnDeleteYes').on("click", DeleteMultiple);
        $('#myModal').modal('show');
    }
    else {

    }
}
$('#delete-multiple-btn').on("click", ConfirmDeleteMultiple);

var DeleteMultiple = function () {

    var itemsToDelete = new Array();
    $("input[name='projectIdsToDelete']:checked").each(function () {
        itemsToDelete.push($(this).val());
    })
    var data = { projectIds: itemsToDelete };
    var urldata = "/Projects/DeleteRange";
    $.ajax({
        type: "POST",
        url: urldata,
        data: data,
        success: function () {
            location.reload(true);
        },
        error: function () {
            alert("Error loading data...");
        }
    });

};



