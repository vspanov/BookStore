using System;
using System.Web.Mvc;
using System.Linq;
using System.Web;

namespace BookStore.Helpers
{
    public static class ListHelper
    {
        //public static MvcHtmlString CreateList(this HtmlHelper html, string[] items)
        //{
        //    TagBuilder ul = new TagBuilder("ul");
        //    foreach (string item in items)
        //    {
        //        TagBuilder li = new TagBuilder("li");
        //        li.SetInnerText(item);
        //        ul.InnerHtml += li.ToString();
        //    }
        //    return new MvcHtmlString(ul.ToString());
        //}
        public static MvcHtmlString CreateList(this HtmlHelper html, string[] items, object htmlAttributes = null)
        {
            TagBuilder ul = new TagBuilder("ul");
            foreach (string item in items)
            {
                TagBuilder li = new TagBuilder("li");
                li.SetInnerText(item);
                ul.InnerHtml += li.ToString();
            }
            ul.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));

            return MvcHtmlString.Create(ul.ToString());
        }
    }
}