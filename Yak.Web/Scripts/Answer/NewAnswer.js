function PrepareMarkdownEditor() {
    $("textarea.mdd_editor").MarkdownDeep({
        help_location: "../Content/mdd_help.htm",
        ExtraMode: true,
        resizebar: false
    });
}

$(function () {

    $(".toggle-preview").click(function () {
        $(".markup-preview-box").toggle();
    });
});