#region Related components
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Collections.Generic;
#endregion

namespace net.vieapps.Components.Utility.Epub
{
    class Guide
    {
        private readonly XElement _element;

		internal Guide() => this._element = new XElement(Document.OpfNS + "guide");

		internal void AddReference(string href, string type, string title = "")
        {
            var itemref = new XElement(Document.OpfNS + "reference", new XAttribute("href", href), new XAttribute("type", type), new XAttribute("title", title));
            if (!string.IsNullOrEmpty(title))
                itemref.SetAttributeValue("title", title);
            this._element.Add(itemref);
        }

		internal XElement ToElement() => this._element;
	}
}
