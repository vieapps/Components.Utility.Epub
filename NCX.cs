#region Related components
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Collections.Generic;
#endregion

namespace net.vieapps.Components.Utility.Epub
{
    class NCX
    {
        private string _title;
        private List<String> _authors;
        private string _uid;
        private List<NavPoint> _navPoints;
        internal static XNamespace NcxNS = "http://www.daisy.org/z3986/2005/ncx/";

        internal NCX()
        {
            this._navPoints = new List<NavPoint>();
			this._authors = new List<string>();
			this._title = String.Empty;
        }

		internal void SetUid(string uid) => this._uid = uid;

		internal void AddAuthor(string author) => this._authors.Add(author);

		internal void AddTitle(string title) => this._title += " " + title;

		internal void SetTitle(string title) => this._title = title ?? string.Empty;

		internal XDocument ToXmlDocument()
        {
            var doc = new XDocument(new XDocumentType("ncx", "-//NISO//DTD ncx 2005-1//EN", "http://www.daisy.org/z3986/2005/ncx-2005-1.dtd", null));
			var ncx = new XElement(NcxNS + "ncx");
            ncx.Add(CreateHeadElement());

            // create doc data
            ncx.Add(new XElement(NcxNS + "docTitle", new XElement(NcxNS + "text", _title)));

            foreach (var author in this._authors)
				ncx.Add(new XElement(NcxNS + "docAuthor", new XElement(NcxNS + "text", author)));

			var navMap = new XElement(NcxNS + "navMap");
            foreach (var navPoint in this._navPoints)
				navMap.Add(navPoint.ToElement());

			ncx.Add(navMap);
            doc.Add(ncx);
            return doc;
        }

        internal NavPoint AddNavPoint(string label, string id, string content, int playOrder)
        {
            var navPoint = new NavPoint(label, id, content, playOrder);
            this._navPoints.Add(navPoint);
            return navPoint;
        }

        private XElement CreateHeadElement()
        {
            var head = new XElement(NcxNS + "head");
            head.Add(new XElement(NcxNS + "meta", new XAttribute("name", "dtb:uid"), new XAttribute("content", _uid)));
            head.Add(new XElement(NcxNS + "meta", new XAttribute("name", "dtb:depth"), new XAttribute("content", "1")));
            head.Add(new XElement(NcxNS + "meta", new XAttribute("name", "dtb:totalPageCount"), new XAttribute("content", "0")));
            head.Add(new XElement(NcxNS + "meta", new XAttribute("name", "dtb:maxPageNumber"), new XAttribute("content", "0")));
            return head;
        }
    }
}
