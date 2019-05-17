#region Related components
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Collections.Generic;
#endregion

namespace net.vieapps.Components.Utility.Epub
{
    class DCItem
    {
        readonly string _name;
        readonly string _value;

		readonly IDictionary<string, string> _attributes;
		readonly IDictionary<string, string> _opfAttributes;

        internal DCItem(string name, string value)
        {
            this._name = name;
			this._value = value;
			this._attributes = new Dictionary<string, string>();
			this._opfAttributes = new Dictionary<string, string>();
        }

		internal void SetAttribute(string name, string value) => this._attributes.Add(name, value);

		internal void SetOpfAttribute(string name, string value) => this._opfAttributes.Add(name, value);

		internal XElement ToElement()
        {
            var element = new XElement(Document.DcNS + this._name, this._value);

            foreach (string key in this._opfAttributes.Keys)
				element.SetAttributeValue(Document.OpfNS + key, this._opfAttributes[key]);

			foreach (string key in this._attributes.Keys)
				element.SetAttributeValue(key, this._attributes[key]);

			return element;
        }
    }
}
