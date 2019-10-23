/// <reference path="jquery-ui-1.12.1.min.js" />

$("button.delete-btn").on("click", function (e) {
    e.preventDefault();
    var id = $(this).closest('tr').attr('id');
    $('#myModal').attr('data-target', id).modal('show');
});

$('body').on("click", "#btnDeleteYes", function () {
    var id = $('#myModal').attr('data-target');
    $('#' + id).find('form').submit();
    $('#myModal').modal('hide');
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

$(document).ready(function () {
    $('#delete-multiple-btn').click(function () {
        
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
            dataType: "json",
            success: function () {
                setTimeout(function () { location.reload(); }, 1000);
            },
            error: function () {
                alert("Error loading data...");
            }
        });
    });
});


