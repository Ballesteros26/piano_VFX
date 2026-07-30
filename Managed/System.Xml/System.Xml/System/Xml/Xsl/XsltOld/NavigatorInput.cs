using System;
using System.Diagnostics;
using System.Xml.XPath;
using System.Xml.Xsl.Xslt;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000527 RID: 1319
	internal class NavigatorInput
	{
		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x06003508 RID: 13576 RVA: 0x0012B34F File Offset: 0x0012954F
		// (set) Token: 0x06003509 RID: 13577 RVA: 0x0012B357 File Offset: 0x00129557
		internal NavigatorInput Next
		{
			get
			{
				return this._Next;
			}
			set
			{
				this._Next = value;
			}
		}

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x0600350A RID: 13578 RVA: 0x0012B360 File Offset: 0x00129560
		internal string Href
		{
			get
			{
				return this._Href;
			}
		}

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x0600350B RID: 13579 RVA: 0x0012B368 File Offset: 0x00129568
		internal KeywordsTable Atoms
		{
			get
			{
				return this._Atoms;
			}
		}

		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x0600350C RID: 13580 RVA: 0x0012B370 File Offset: 0x00129570
		internal XPathNavigator Navigator
		{
			get
			{
				return this._Navigator;
			}
		}

		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x0600350D RID: 13581 RVA: 0x0012B378 File Offset: 0x00129578
		internal InputScopeManager InputScopeManager
		{
			get
			{
				return this._Manager;
			}
		}

		// Token: 0x0600350E RID: 13582 RVA: 0x0012B380 File Offset: 0x00129580
		internal bool Advance()
		{
			return this._Navigator.MoveToNext();
		}

		// Token: 0x0600350F RID: 13583 RVA: 0x0012B38D File Offset: 0x0012958D
		internal bool Recurse()
		{
			return this._Navigator.MoveToFirstChild();
		}

		// Token: 0x06003510 RID: 13584 RVA: 0x0012B39A File Offset: 0x0012959A
		internal bool ToParent()
		{
			return this._Navigator.MoveToParent();
		}

		// Token: 0x06003511 RID: 13585 RVA: 0x0012B3A7 File Offset: 0x001295A7
		internal void Close()
		{
			this._Navigator = null;
			this._PositionInfo = null;
		}

		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x06003512 RID: 13586 RVA: 0x0012B3B7 File Offset: 0x001295B7
		internal int LineNumber
		{
			get
			{
				return this._PositionInfo.LineNumber;
			}
		}

		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x06003513 RID: 13587 RVA: 0x0012B3C4 File Offset: 0x001295C4
		internal int LinePosition
		{
			get
			{
				return this._PositionInfo.LinePosition;
			}
		}

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x06003514 RID: 13588 RVA: 0x0012B3D1 File Offset: 0x001295D1
		internal XPathNodeType NodeType
		{
			get
			{
				return this._Navigator.NodeType;
			}
		}

		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x06003515 RID: 13589 RVA: 0x0012B3DE File Offset: 0x001295DE
		internal string Name
		{
			get
			{
				return this._Navigator.Name;
			}
		}

		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x06003516 RID: 13590 RVA: 0x0012B3EB File Offset: 0x001295EB
		internal string LocalName
		{
			get
			{
				return this._Navigator.LocalName;
			}
		}

		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x06003517 RID: 13591 RVA: 0x0012B3F8 File Offset: 0x001295F8
		internal string NamespaceURI
		{
			get
			{
				return this._Navigator.NamespaceURI;
			}
		}

		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x06003518 RID: 13592 RVA: 0x0012B405 File Offset: 0x00129605
		internal string Prefix
		{
			get
			{
				return this._Navigator.Prefix;
			}
		}

		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x06003519 RID: 13593 RVA: 0x0012B412 File Offset: 0x00129612
		internal string Value
		{
			get
			{
				return this._Navigator.Value;
			}
		}

		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x0600351A RID: 13594 RVA: 0x0012B41F File Offset: 0x0012961F
		internal bool IsEmptyTag
		{
			get
			{
				return this._Navigator.IsEmptyElement;
			}
		}

		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x0600351B RID: 13595 RVA: 0x0012B42C File Offset: 0x0012962C
		internal string BaseURI
		{
			get
			{
				return this._Navigator.BaseURI;
			}
		}

		// Token: 0x0600351C RID: 13596 RVA: 0x0012B439 File Offset: 0x00129639
		internal bool MoveToFirstAttribute()
		{
			return this._Navigator.MoveToFirstAttribute();
		}

		// Token: 0x0600351D RID: 13597 RVA: 0x0012B446 File Offset: 0x00129646
		internal bool MoveToNextAttribute()
		{
			return this._Navigator.MoveToNextAttribute();
		}

		// Token: 0x0600351E RID: 13598 RVA: 0x0012B453 File Offset: 0x00129653
		internal bool MoveToFirstNamespace()
		{
			return this._Navigator.MoveToFirstNamespace(XPathNamespaceScope.ExcludeXml);
		}

		// Token: 0x0600351F RID: 13599 RVA: 0x0012B461 File Offset: 0x00129661
		internal bool MoveToNextNamespace()
		{
			return this._Navigator.MoveToNextNamespace(XPathNamespaceScope.ExcludeXml);
		}

		// Token: 0x06003520 RID: 13600 RVA: 0x0012B470 File Offset: 0x00129670
		internal NavigatorInput(XPathNavigator navigator, string baseUri, InputScope rootScope)
		{
			if (navigator == null)
			{
				throw new ArgumentNullException("navigator");
			}
			if (baseUri == null)
			{
				throw new ArgumentNullException("baseUri");
			}
			this._Next = null;
			this._Href = baseUri;
			this._Atoms = new KeywordsTable(navigator.NameTable);
			this._Navigator = navigator;
			this._Manager = new InputScopeManager(this._Navigator, rootScope);
			this._PositionInfo = PositionInfo.GetPositionInfo(this._Navigator);
			if (this.NodeType == XPathNodeType.Root)
			{
				this._Navigator.MoveToFirstChild();
			}
		}

		// Token: 0x06003521 RID: 13601 RVA: 0x0012B4FC File Offset: 0x001296FC
		internal NavigatorInput(XPathNavigator navigator)
			: this(navigator, navigator.BaseURI, null)
		{
		}

		// Token: 0x06003522 RID: 13602 RVA: 0x00002F50 File Offset: 0x00001150
		[Conditional("DEBUG")]
		internal void AssertInput()
		{
		}

		// Token: 0x040021D8 RID: 8664
		private XPathNavigator _Navigator;

		// Token: 0x040021D9 RID: 8665
		private PositionInfo _PositionInfo;

		// Token: 0x040021DA RID: 8666
		private InputScopeManager _Manager;

		// Token: 0x040021DB RID: 8667
		private NavigatorInput _Next;

		// Token: 0x040021DC RID: 8668
		private string _Href;

		// Token: 0x040021DD RID: 8669
		private KeywordsTable _Atoms;
	}
}
