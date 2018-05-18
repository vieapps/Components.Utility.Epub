#region Related components
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Collections.Generic;
#endregion

namespace net.vieapps.Components.Utility.Epub
{
    class Manifest
    {
        XElement _element;

		internal Manifest() => this._element = new XElement(Document.OpfNS + "manifest");

		internal void AddItem(string id, string href, string type)
        {
            var item = new XElement(Document.OpfNS + "item");
            item.SetAttributeValue("id", id);
            item.SetAttributeValue("href", href);
            item.SetAttributeValue("media-type", type);
            this._element.Add(item);
        }


		internal XElement ToElement() => this._element;
	}
}
