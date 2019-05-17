#region Related components

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

#endregion Related components

namespace net.vieapps.Components.Utility.Epub
{
    internal class Metadata
    {
        private List<DCItem> _dcItems = new List<DCItem>();
        private List<Item> _items = new List<Item>();

        internal void AddAuthor(string name) => this.AddCreator(name, "aut");

        internal void AddTranslator(string name) => this.AddCreator(name, "trl");

        internal void AddSubject(string subj) => this._dcItems.Add(new DCItem("subject", subj));

        internal void AddDescription(string description) => this._dcItems.Add(new DCItem("description", description));

        internal void AddType(string @type) => this._dcItems.Add(new DCItem("type", @type));

        internal void AddFormat(string format) => this._dcItems.Add(new DCItem("format", format));

        internal void AddLanguage(string lang) => this._dcItems.Add(new DCItem("language", lang));

        internal void AddRelation(string relation) => this._dcItems.Add(new DCItem("relation", relation));

        internal void AddRights(string rights) => this._dcItems.Add(new DCItem("rights", rights));

        internal void AddCreator(string name, string role)
        {
            var dcitem = new DCItem("creator", name);
            dcitem.SetOpfAttribute("role", role);
            this._dcItems.Add(dcitem);
        }

        internal void AddCcontributor(string name, string role)
        {
            var dcitem = new DCItem("contributor", name);
            dcitem.SetOpfAttribute("role", role);
            this._dcItems.Add(dcitem);
        }

        internal void AddTitle(string title) => this._dcItems.Add(new DCItem("title", title));

        internal void AddPublisher(string publisher) => this._dcItems.Add(new DCItem("publisher", publisher));

        internal void AddBookIdentifier(string id, string uuid) => this.AddBookIdentifier(id, uuid, string.Empty);

        internal void AddBookIdentifier(string id, string uuid, string scheme)
        {
            var dcitem = new DCItem("identifier", uuid);
            dcitem.SetAttribute("id", id);
            if (!string.IsNullOrEmpty(scheme))
                dcitem.SetOpfAttribute("scheme", scheme);
            this._dcItems.Add(dcitem);
        }

        internal void AddItem(string name, string value) => this._items.Add(new Item(name, value));

        internal XElement ToElement()
        {
            XNamespace dc = "http://purl.org/dc/elements/1.1/";
            XNamespace opf = "http://www.idpf.org/2007/opf";

            var element = new XElement(Document.OpfNS + "metadata", new XAttribute(XNamespace.Xmlns + "dc", dc), new XAttribute(XNamespace.Xmlns + "opf", opf));

            foreach (var item in this._items)
                element.Add(item.ToElement());

            foreach (var item in this._dcItems)
                element.Add(item.ToElement());

            return element;
        }
    }
}