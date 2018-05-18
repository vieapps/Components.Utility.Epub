#region Related components
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Collections.Generic;
#endregion

namespace net.vieapps.Components.Utility.Epub
{
    class Item
    {
        private readonly string _name;
        private readonly string _value;

        internal Item(string name, string value)
        {
            this._name = name;
			this._value = value;
        }

        internal XElement ToElement()
        {
            var element = new XElement(Document.OpfNS + "meta");
            element.SetAttributeValue("name", this._name);
            element.SetAttributeValue("content", this._value);
            return element;
        }
    }
}
