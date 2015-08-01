var tagApi = null;

function PrepareMarkdownEditor() {
    $("textarea.mdd_editor").MarkdownDeep({
        help_location: "../Content/mdd_help.htm",
        ExtraMode: true,
        resizebar: false
    });
}

function PrepareTagsManager() {
    tagApi = $(".tm-input").tagsManager(
        {
            hiddenTagListName: 'QuestionTags'
        });
}

function PushTagsToTagsManager(tagsString)
{
    var splittedTags = tagsString.split(',');
    for(var i = 0; i < splittedTags.length; i++)
    {
        tagApi.tagsManager("pushTag", splittedTags[i]);
    }
}

$(function () {

    $(".toggle-preview").click(function () {
        $(".markup-preview-box").toggle();
    });
});