
var ShowLogin = function (s, e) {
    // Show the login form and direct user to log in.
    $('#loginModal').modal({ backdrop: 'static', keyboard: false }).modal('show');

    // Test that the API can be reached. Don't frustrate the user by allowing them
    // to try and log in when it won't work.
    $.get('/api/isalive').done(function () {

        // Bind a click event to login button to allow the user to log in.
        $('#ButtonLogin').click(function (s, e) { Authenticate(s, e) })

    }).fail(function () {
       // alert the user that the system is down.
        $('#loginModal input, #loginModal button').attr('disabled', 'disabled');
        $('#SystemIsDown').fadeIn(1200);
    }); 
}

// Render the content after login.
var renderContent = function (data) {

    $('#loginModal').modal('hide');
    $('#UserName').text(data.userModel.userName);

    var ClaimsData = data.dataModel;
    $.each(ClaimsData, function (key, value) {

        // Generating html like this is ok for small sets of data.
        // Better than dealing with string templates later.
        var tableRow = document.createElement("tr");

        var tableCell_Code = document.createElement("td");
        tableCell_Code.innerText = value.lossTypeCode;
       
        var tableCell_Description = document.createElement("td");
        tableCell_Description.innerText = value.lossTypeDescription;

        var tableCell_Id = document.createElement("td");
        tableCell_Id.innerText = value.lossTypeID;

        tableRow.append(tableCell_Code);
        tableRow.append(tableCell_Description);
        tableRow.append(tableCell_Id);

        $('#DataTable').append(tableRow);
    }); 

    $('#Main').fadeIn(100);

}

// Authenticate the user and return loss data.
var Authenticate = function (s,e) {

    $('#LoginFailed').hide(); 

    var data = {username:'',password:''};
    data.username = $('#InpuUserName').val();
    data.password = $('#InputPassword').val();

    $.ajax({
        type: "POST",
        url: "api/Accounts/Login",
        data: data,
        dataType: 'JSON'
    }).done(function (dData) {
        renderContent(dData)
    }).fail(function () {
        $('#LoginFailed').fadeIn(1200); 
    });
}

$(document).ready(function () {
    ShowLogin();
    console.log('Application started.');
});


