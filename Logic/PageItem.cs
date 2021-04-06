using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assigment.Logic
{
    public class PageItem
    {
        

        public PageItem(string text, int index, string cssClass)
        {
            Text = text;
            Index = index;
            CssClass = cssClass;
        }
        public PageItem(string text, int index, string cssClass, string searchID, string searchName) : this(text, index, cssClass)
        {
            this.SearchID = searchID;
            this.SearchName = searchName;
        }
        //public PageItem(string text, int index, string cssClass, string searchID, string searchName)
        //{
        //    Text = text;
        //    Index = index;
        //    CssClass = cssClass;
        //    SearchID = searchID;
        //    SearchName = searchName;
        //}

        public string Text { get; set; }
        public int Index { get; set; }
        public string CssClass { get; set; }
        public string SearchID { get; set; }
        public string SearchName { get; set; }
        
        //public PageItem(string text, int index, string cssClass)
        //{
        //    Text = text;
        //    Index = index;
        //    CssClass = cssClass;
        //}
    }
}
