$(document).ready(function () {
    GetTestResults();
});

/*Read From Database*/
function GetTestResults() {
    $.ajax({
        url: '/Test/GetTestResults',
        type: 'get',
        datatype: 'json',
        contentType: 'application/json:charset=utf-8',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                var object = '';
                object += '<tr>';
                object += '<td colspan="5">' + 'No test method records available!' +
                    '</td>';
                object += '</tr>';
                $('#tblBody').html(object)
            }
            else {
                var object = '';
                $.each(response, function (index, item) {
                    object += '<tr>';
                    object += '<td>' + item.id + '</td>';
                    object += '<td>' + item.description + '</td>';
                    object += '<td>' + item.docTests.testfor + '</td>';
                    object += '<td>' + item.date + '</td>';
                    object += '<td>' + item.results + '</td>';
                    //object += '<td>' + '<a href="#" class="btn btn-primary btn-sm" onclick="Edit(' + item.id + ')">Edit</a>' +
                    //    '<a href="#" class="btn btn-danger btn-sm" onclick="Delete(' + item.id + ')">Delete</a></td>';
                    object += '</tr>';
                });
                $('#tblBody').html(object);
            }
        },
        error: function () {
            alert('Unable to read data.');
        }
    });
}

$('#btnAdd').click(function () {
    $('#TestMethodModal').modal('show');
    $('#modalTitle').text('Add Test Method');
    $('#Update').css('display', 'none');
});

/*Insert To Database*/
function Insert() {
    var result = Validate();
    if (result == false) {
        return false;
    }

    var formData = new Object();

    formData.id = $('#Id').val();
    formData.description = $('#MethodName').val();
    formData.date = $('#Description').val();
    formData.results = $('#Description').val();

    $.ajax({
        url: '/Method/Insert',
        data: formData,
        type: 'post',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                alert('Unable to save data.');
            }
            else {
                HideModal();
                GetTestMethods();
                alert(response);
            }
        },
        error: function () {
            alert('Unable to save data.');
        }

    });
}

function HideModal() {
    ClearData();
    $('#TestMethodModal').modal('hide')
}
function ClearData() {
    $('#MethodName').val('');
    $('#Description').val('');

    $('#MethodName').css('border-color', 'lightgrey');
    $('#Description').css('border-color', 'lightgrey');

}

function Validate() {
    var isValid = true;

    if ($('#MethodName').val().trim == "") {

        $('#MethodName').css('border-color', 'Red');
        isValid = false;
    } else {
        $('#MethodName').css('border-color', 'lightgrey');
    }

    if ($('#Description').val().trim == "") {

        $('#Description').css('border-color', 'Red');
        isValid = false;
    } else {
        $('#Description').css('border-color', 'lightgrey');
    }
    return isValid;

}
$('#MethodName').change(function () {
    Validate();
});
$('#Description').change(function () {
    Validate();
});
/*Edit */
function Edit(id) {
    $.ajax({
        url: 'Method/Edit?id=' + id,
        type: 'get',
        contentType: 'application/json;charset=utf-8',
        datatype: 'json',
        success: function (response) {
            if (response == null || response == undefined) {
                alert('Unable to read data.');
            }
            else if (response.length == 0) {
                alert('Data with' + id + ' not available with the id')
            }
            else {
                $('#TestMethodModal').modal('show');
                $('#modalTitle').text('Update Record');
                $('#Save').css('display', 'none');
                $('#Update').css('display', 'block');
                $('#Id').val(response.id);
                $('#MethodName').val(response.methodName);
                $('#Description').val(response.description);
            }
        },
        error: function () {
            alert('Unable to read data.');
        }
    });
}
/*Update*/
function Update() {
    var result = Validate();
    if (result == false) {
        return false;
    }
    var formData = new Object();

    formData.id = $('#Id').val();
    formData.methodName = $('#MethodName').val();
    formData.description = $('#Description').val();

    $.ajax({
        url: '/Method/Update',
        data: formData,
        type: 'post',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                alert('Unable to save data.');
            }
            else {
                HideModal();
                GetTestMethods();
                alert(response);
            }
        },
        error: function () {
            alert('Unable to save data.');
        }

    });


}

/*Delete*/

function Delete(id) {
    if (confirm('Are you sure you want to delete this record?')) {
        $.ajax({
            url: '/Method/Delete?id=' + id,
            type: 'post',

            success: function (response) {
                if (response == null || response == undefined) {
                    alert('Unable to delete data.')

                }
                else if (response.length == 0) {
                    alert('Data with ' + id + 'not found.');
                }
                else {
                    GetTestMethods();
                    alert(response);
                }
            }
        })
    }



}