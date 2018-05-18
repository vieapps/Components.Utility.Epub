#region Related components
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Collections.Generic;
#endregion

namespace net.vieapps.Components.Utility.Epub
{
    class Spine
    {
        private struct ItemRef
        {
            public string ID;
            public bool Linear;
        };

        private string _toc;
        private List<ItemRef> _itemRefs;

		internal Spine() => this._itemRefs = new List<ItemRef>();

		internal void SetToc(string toc) => this._toc = toc;

		internal void AddItemRef(string id, bool linear)
        {
            ItemRef itemRef;
            itemRef.ID = id;
            itemRef.Linear = linear;
			this._itemRefs.Add(itemRef);
        }

        internal XElement ToElement()
        {
            var element = new XElement(Document.OpfNS + "spine");
            if (!string.IsNullOrEmpty(this._toc))
                element.Add(new XAttribute("toc", this._toc));
            foreach (var itemRef in this._itemRefs)
            {
                var item = new XElement(Document.OpfNS + "itemref", new XAttribute("idref", itemRef.ID));
                if (!itemRef.Linear)
                    item.SetAttributeValue("linear", "no");
                element.Add(item);
            }
            return element;
        }
    }
}
