﻿// Copyright (c) 2014 AlphaSierraPapa for the SharpDevelop Team
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this
// software and associated documentation files (the "Software"), to deal in the Software
// without restriction, including without limitation the rights to use, copy, modify, merge,
// publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons
// to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
// PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
// FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

using System;
using ICSharpCode.NRefactory.TypeSystem;
using NUnit.Framework;
using XmlEditor.Tests.Utils;

namespace XmlEditor.Tests.Folding
{
	[TestFixture]
	public class MultiLineCommentFoldTestFixture
	{
		XmlFoldParserHelper helper;
		
		[SetUp]
		public void Init()
		{
			string xml = 
				"<!-- first line\r\n" +
				"second line -->\r\n" +
				"<a />";
			
			helper = new XmlFoldParserHelper();
			helper.CreateParser();
			helper.GetFolds(xml);
		}
		
		[Test]
		public void GetFolds_MultiLineComment_ReturnsOneFold()
		{
			int count = helper.Folds.Count;
			Assert.AreEqual(1, count);
		}
		
		[Test]
		public void GetFolds_MultiLineComment_FoldNameIsCommentTagsWithFirstLineOfTextBetween()
		{
			string name = helper.GetFirstFoldName();
			string expectedName = "<!-- first line -->";
			Assert.AreEqual(expectedName, name);
		}
		
		[Test]
		public void GetFolds_MultiLineComment_FoldRegionContainsEntireComment()
		{
			DomRegion region = helper.GetFirstFoldRegion();
			
			int beginLine = 1;
			int beginColumn = 1;
			int endLine = 2;
			int endColumn = 16;
			DomRegion expectedRegion = new DomRegion(beginLine, beginColumn, endLine, endColumn);
			
			Assert.AreEqual(expectedRegion, region);
		}
	}
}
