using MarkdownDeep;
using System.Web;
using System.Web.Mvc;

namespace Yak.Web.Utils
{
    public static class MarkdownHelper
    {
        public static Markdown MarkdownTransformer = new Markdown();

        public static IHtmlString TransformToMarkdown(this HtmlHelper helper, string text)
        {
            return helper.Raw(MarkdownTransformer.Transform(text));
        }
    }
}