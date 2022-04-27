filename = "";
id = "";
function getfilename(_filename) {
    filename = _filename;
}

function getfilenameandid(_filename, _id) {
    filename = _filename;
    id = _id;
}
$(function () {
    var DOWNLOAD_ENDPOINT = "/download";

    var downloadForm = $("form#DownloadFile");

    downloadForm.submit(function (event) {
        event.preventDefault();

        var downloadWindow = window.open(
            DOWNLOAD_ENDPOINT + "/" + filename,
            "_blank"
        );
        downloadWindow.focus();
    });

    var deleteForm = $("form#DeleteFile");

    deleteForm.submit(function (event) {
        event.preventDefault();
        mMFinancial.transactions.upload.delete(id, filename);
        document.getElementById(id).hidden = true;

    });


    $("#UploadFileDto_File").change(function () {
        var fileName = $(this)[0].files[0].name;

        $("#UploadFileDto_Name").val(fileName);
    });
});