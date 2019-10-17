$("button.delete-btn").on("click", function (e) {
    e.preventDefault();
    var id = $(this).closest('tr').attr('id');
    $('#myModal').attr('data-target', id).modal('show');
});

$('body').on("click", "#btnDeleteYes", function () {
    var id = $('#myModal').attr('data-target');
    $('#'+id).find('form').submit();
    $('#myModal').modal('hide');
});