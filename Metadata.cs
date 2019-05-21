#region Related components
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Collections.Generic;
#endregion

namespace net.vieapps.Components.Utility.Epub
{
	internal class Metadata
	{
		internal class Item
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

		internal class DCItem
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

			internal void SetAttribute(string name, string value)
				=> this._attributes.Add(name, value);

			internal void SetOpfAttribute(string name, string value)
				=> this._opfAttributes.Add(name, value);

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

		readonly List<Item> _items = new List<Item>();
		readonly List<DCItem> _dcItems = new List<DCItem>();

		internal Item AddItem(string name, string value)
		{
			var item = new Item(name, value);
			this._items.Add(item);
			return item;
		}

		internal DCItem AddDCItem(string name, string value)
		{
			var item = new DCItem(name, value);
			this._dcItems.Add(item);
			return item;
		}

		internal XElement ToElement()
		{
			XNamespace dc = "http://purl.org/dc/elements/1.1/";
			XNamespace opf = "http://www.idpf.org/2007/opf";
			var element = new XElement(Document.OpfNS + "metadata", new XAttribute(XNamespace.Xmlns + "dc", dc), new XAttribute(XNamespace.Xmlns + "opf", opf));
			this._items.ForEach(item => element.Add(item.ToElement()));
			this._dcItems.ForEach(item => element.Add(item.ToElement()));
			return element;
		}

		internal void AddCreator(string name, string role)
		{
			var item = this.AddDCItem("creator", name);
			if (!string.IsNullOrWhiteSpace(role))
				item.SetOpfAttribute("role", role);
		}

		internal void AddAuthor(string name) 
			=> this.AddCreator(name, "aut");

		internal void AddTranslator(string name)
			=> this.AddCreator(name, "trl");

		internal void AddContributor(string name)
			=> this.AddDCItem("contributor", name);

		internal void AddSubject(string subject)
			=> this.AddDCItem("subject", subject);

		internal void AddDescription(string description)
			=> this.AddDCItem("description", description);

		internal void AddType(string type)
			=> this.AddDCItem("type", type);

		internal void AddFormat(string format)
			=> this.AddDCItem("format", format);

		internal void AddLanguage(string language)
			=> this.AddDCItem("language", language);

		internal void AddRelation(string relation)
			=> this.AddDCItem("relation", relation);

		internal void AddRights(string rights)
			=> this.AddDCItem("rights", rights);

		internal void AddTitle(string title)
			=> this.AddDCItem("title", title);

		internal void AddPublisher(string publisher)
			=> this.AddDCItem("publisher", publisher);

		internal void AddBookIdentifier(string id, string uuid, string scheme)
		{
			var item = this.AddDCItem("identifier", uuid);
			item.SetAttribute("id", id);
			if (!string.IsNullOrEmpty(scheme))
				item.SetOpfAttribute("scheme", scheme);
		}

		internal void AddBookIdentifier(string id, string uuid)
			=> this.AddBookIdentifier(id, uuid, string.Empty);
	}
}