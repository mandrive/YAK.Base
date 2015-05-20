(function() {
	var converter = Markdown.getSanitizingConverter();
                            
    var editor = new Markdown.Editor("Content", converter);
                
    editor.run();
})();