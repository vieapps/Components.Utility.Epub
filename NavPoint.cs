#region Related components
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Collections.Generic;
#endregion

namespace net.vieapps.Components.Utility.Epub
{
    /// <summary>
    /// Class for TOC entry. Top-level navPoints should be created by Epub.Document.AddNavPoint method
    /// </summary>
    public class NavPoint
    {
        private readonly string _label;
        private readonly string _id;
        private readonly string _content;
        private readonly string _class;
        private readonly int _playOrder;
        List<NavPoint> _navPoints;

        internal NavPoint(string label, string id, string content, int playOrder, string @class = "")
        {
            this._label = label;
			this._id = id;
			this._content = content;
			this._playOrder = playOrder;
			this._class = @class;
			this._navPoints = new List<NavPoint>();
        }

        /// <summary>
        /// Add TOC entry as a direct child of this NavPoint
        /// </summary>
        /// <param name="label">Text of TOC entry</param>
        /// <param name="content">Link to TOC entry</param>
        /// <param name="playOrder">play order counter</param>
        /// <returns>newly created NavPoint </returns>
        public NavPoint AddNavPoint(string label, string content, int playOrder)
        {
            var id = $"{this._id}x{(this._navPoints.Count + 1)}";
			var nav = new NavPoint(label, id, content, playOrder);
            this._navPoints.Add(nav);
            return nav;
        }

        internal XElement ToElement()
        {
            var element = new XElement(NCX.NcxNS + "navPoint", new XAttribute("id", this._id), new XAttribute("playOrder", this._playOrder));
            if (!string.IsNullOrEmpty(this._class))
                element.Add(new XAttribute("class", this._class));
            element.Add(new XElement(NCX.NcxNS + "navLabel", new XElement(NCX.NcxNS + "text", this._label)));
            element.Add(new XElement(NCX.NcxNS + "content", new XAttribute("src", this._content)));
            foreach (var navPoint in this._navPoints)
				element.Add(navPoint.ToElement());
			return element;
        }
    }
}
