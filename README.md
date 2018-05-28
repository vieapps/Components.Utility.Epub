# VIEApps.Components.Utility.Epub

A simple library for generating .EPUB documents on .NET Standard 2.0

[![NuGet](https://img.shields.io/nuget/v/VIEApps.Components.Utility.Epub.svg)](https://www.nuget.org/packages/VIEApps.Components.Utility.Epub)

```csharp
using net.vieapps.Components.Utility.Epub;

var epub = new Epub.Document();
epub.AddBookIdentifier(uuid);
epub.AddLanguage(language);
epub.AddTitle(title);
epub.AddAuthor(author);
epub.AddMetaItem("dc:contributor", contributor);
epub.AddMetaItem("book:Original", original);
epub.AddMetaItem("book:Publisher", publisher);
epub.AddStylesheetData("style.css", stylesheet);

var coverImageId = epub.AddImageData("cover.jpg", coverImageBinaryData);
epub.AddMetaItem("cover", coverImageId);

var pageTemplate = @"<!DOCTYPE html>
	<html xmlns=""http://www.w3.org/1999/xhtml"">
		<head>
			<title>{0}</title>
			<meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8""/>
			<link type=""text/css"" rel=""stylesheet"" href=""style.css""/>
			<style type=""text/css"">
				@page {
					padding: 0;
					margin: 0;
				}
			</style>
		</head>
		<body>
			{1}
		</body>
	</html>".Trim().Replace("\t", "");

// cover
epub.AddXhtmlData("page0.xhtml", pageTemplate.Replace("{0}", coverTitle).Replace("{1}", coverBody));

// chapter
for (var index = 0; index < pages.Count; index++)
{
	var name = string.Format("page{0}.xhtml", index + 1);
	var content = pages[index];

	epub.AddXhtmlData(name, pageTemplate.Replace("{0}", index < navs.Count ? navs[index] : coverTitle).Replace("{1}", content));
	epub.AddNavPoint(index < navs.Count ? navs[index] : coverTitle + " - " + (index + 1).ToString(), name, index + 1);
}

epub.Generate("sample-document.epub");
```

## Others

This library is hard-fork of [DotNetEpub](https://github.com/gonzoua/DotNetEpub) with some modifications to run wel on .NET Standard 2.0
