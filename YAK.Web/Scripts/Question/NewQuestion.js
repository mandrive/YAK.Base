var tagApi = null;

function PrepareMarkdownEditor() {
    $("textarea.mdd_editor").MarkdownDeep({
        help_location: "/Scripts/mdd_help.htm",
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