#region Related components
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Collections.Generic;
#endregion

namespace net.vieapps.Components.Utility.Epub
{
    class Container
    {
        private struct RootFile
        {
            public string File;
            public string MediaType;
        }

		readonly List<RootFile> _rootFiles;

		internal Container()
			=> this._rootFiles = new List<RootFile>();

		internal void AddRootFile(string file, string mediaType)
			=> this._rootFiles.Add(new RootFile { File = file, MediaType = mediaType });

		internal XElement ToElement()
        {
            XNamespace ns = "urn:oasis:names:tc:opendocument:xmlns:container";
            var element = new XElement(ns + "container", new XAttribute("version", "1.0"));
			var filesElement = new XElement(ns + "rootfiles");
            foreach (var rootFile in _rootFiles)
            {
				var fileElement = new XElement(ns + "rootfile", new XAttribute("full-path", rootFile.File), new XAttribute("media-type", rootFile.MediaType));
				filesElement.Add(fileElement);
            }
            element.Add(filesElement);
            return element;
        }
    }
}
