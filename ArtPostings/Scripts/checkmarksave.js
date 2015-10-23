function checkmarksave(obj, edit, url) {
    event.preventDefault();
    debugger;
    var itemposting = (function () {
        var title = $("").val();
        var desc = $("ItemPosting_Description").val();
        var id = obj.Id;
        var filepath = obj.FilePath;
        var shortname = obj.Shortname;
        var header = $("ItemPosting_Header").val();
        var size = $("ItemPosting_Size").val();
        var price = $("ItemPosting_Price").val();
        return {
            Id: id,
            FilePath: filepath,
            Title: title,
            ShortName: shortname,
            Header: header,
            Description: desc,
            Size: size,
            Price: price
        };
    })();
    var posting = { "Editing": edit, "Posting": itemposting };
    $.ajax({
        url: url, // controller-agnostic: this might call either the Archive or Home versions of 'Edit'
        type: 'POST',
        data: JSON.stringify({ "Editing": "false", "ItemPosting": itemposting }),
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        cache: false,
        success: function (htmlstring) {
            // need to replace content at a fairly high level to ensure old postings are cleared out of the view
            $('#listitems').html(htmlstring);
            $('#listitems').show();
        }
    });
    return false;
}
