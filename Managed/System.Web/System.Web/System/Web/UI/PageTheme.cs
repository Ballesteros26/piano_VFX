using System;
using System.Collections;
using System.ComponentModel;
using System.Xml;

namespace System.Web.UI
{
	/// <summary>Represents the base class for a page theme, which is a collection of resources that are used to define a consistent look across pages and controls in a Web site. The page theme can be set through configuration or the page directive.</summary>
	// Token: 0x02000214 RID: 532
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public abstract class PageTheme
	{
		/// <summary>Creates a lookup key object for a particular control type and skin ID. </summary>
		/// <returns>An object that can be used as a lookup key in a dictionary-style collection, which contains the control type and skin ID information.</returns>
		/// <param name="controlType">The <see cref="T:System.Type" /> of control to which a control skin applies, which is passed typically from the <see cref="P:System.Web.UI.ControlBuilder.ControlType" />.</param>
		/// <param name="skinID">The name of the control skin for which to create a key. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="controlType" /> is null.</exception>
		// Token: 0x060015E8 RID: 5608 RVA: 0x0003B488 File Offset: 0x00039688
		public static object CreateSkinKey(Type controlType, string skinID)
		{
			return skinID + ":" + controlType;
		}

		/// <summary>Uses the <see cref="M:System.Web.UI.DataBinder.Eval(System.Object,System.String)" /> method of the <see cref="P:System.Web.UI.PageTheme.Page" /> property that the instance of the <see cref="T:System.Web.UI.PageTheme" /> class is associated with to evaluate a data-binding expression.</summary>
		/// <returns>An object that results from the evaluation of the data-binding expression.</returns>
		/// <param name="expression">The navigation path from the container to the public property value. For details, see <see cref="T:System.Web.UI.DataBinder" />.</param>
		// Token: 0x060015E9 RID: 5609 RVA: 0x0003B496 File Offset: 0x00039696
		protected object Eval(string expression)
		{
			return this.Page.Eval(expression);
		}

		/// <summary>Uses the <see cref="M:System.Web.UI.DataBinder.Eval(System.Object,System.String,System.String)" /> method of the <see cref="P:System.Web.UI.PageTheme.Page" /> property that the instance of the <see cref="T:System.Web.UI.PageTheme" /> class is associated with to evaluate a data-binding expression.</summary>
		/// <returns>A string that results from the evaluation of the data-binding expression and conversion to a string type.</returns>
		/// <param name="expression">The navigation path from the container to the public property value. For details, see <see cref="T:System.Web.UI.DataBinder" />.</param>
		/// <param name="format">A .NET Framework format string. For details, see <see cref="T:System.Web.UI.DataBinder" />.</param>
		// Token: 0x060015EA RID: 5610 RVA: 0x0003B4A4 File Offset: 0x000396A4
		protected string Eval(string expression, string format)
		{
			return this.Page.Eval(expression, format);
		}

		/// <summary>Tests whether a device filter applies to the <see cref="T:System.Web.UI.Page" /> control that the instance of the <see cref="T:System.Web.UI.PageTheme" /> class is associated with.</summary>
		/// <returns>true if <paramref name="deviceFilterName" /> applies to the page; otherwise; false.</returns>
		/// <param name="deviceFilterName">The string name of the device filter to check. </param>
		// Token: 0x060015EB RID: 5611 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public bool TestDeviceFilter(string deviceFilterName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Evaluates an XPath data-binding expression.</summary>
		/// <returns>An object that results from the evaluation of the data-binding <paramref name="xPathExpression" />.</returns>
		/// <param name="xPathExpression">The XPath expression to evaluate. For details, see <see cref="T:System.Web.UI.XPathBinder" />.</param>
		// Token: 0x060015EC RID: 5612 RVA: 0x0003B4B3 File Offset: 0x000396B3
		protected object XPath(string xPathExpression)
		{
			return this.Page.XPath(xPathExpression);
		}

		/// <summary>Evaluates an XPath data-binding expression using the specified prefix and namespace mappings for namespace resolution. </summary>
		/// <returns>An object that results from the evaluation of the data-binding <paramref name="xPathExpression" />.</returns>
		/// <param name="xPathExpression">The XPath expression to evaluate. For details, see <see cref="T:System.Web.UI.XPathBinder" />. </param>
		/// <param name="resolver">A set of prefix and namespace mappings used for namespace resolution.</param>
		// Token: 0x060015ED RID: 5613 RVA: 0x0003B4C1 File Offset: 0x000396C1
		protected object XPath(string xPathExpression, IXmlNamespaceResolver resolver)
		{
			return this.Page.XPath(xPathExpression, resolver);
		}

		/// <summary>Evaluates an XPath data-binding expression using the specified format string to display the result.</summary>
		/// <returns>A string that results from the evaluation of the data-binding expression and conversion to a string type.</returns>
		/// <param name="xPathExpression">The XPath expression to evaluate. For details, see <see cref="T:System.Web.UI.XPathBinder" />.</param>
		/// <param name="format">A .NET Framework format string to apply to the result. </param>
		// Token: 0x060015EE RID: 5614 RVA: 0x0003B4D0 File Offset: 0x000396D0
		protected string XPath(string xPathExpression, string format)
		{
			return this.Page.XPath(xPathExpression, format);
		}

		/// <summary>Uses the <see cref="M:System.Web.UI.TemplateControl.XPath(System.String,System.String,System.Xml.IXmlNamespaceResolver)" /> method of the <see cref="P:System.Web.UI.PageTheme.Page" /> control that the instance of the <see cref="T:System.Web.UI.PageTheme" /> class is associated with to evaluate an XPath data-binding expression.</summary>
		/// <returns>A string that results from the evaluation of the data-binding expression and conversion to a string type.</returns>
		/// <param name="xPathExpression">The XPath expression to evaluate. For details, see <see cref="T:System.Web.UI.XPathBinder" />. </param>
		/// <param name="format">A .NET Framework format string to apply to the result. </param>
		/// <param name="resolver">A set of prefix and namespace mappings used for namespace resolution.</param>
		// Token: 0x060015EF RID: 5615 RVA: 0x0003B4DF File Offset: 0x000396DF
		protected string XPath(string xPathExpression, string format, IXmlNamespaceResolver resolver)
		{
			return this.Page.XPath(xPathExpression, format, resolver);
		}

		/// <summary>Evaluates an XPath data-binding expression and returns a node collection that implements the <see cref="T:System.Collections.IEnumerable" /> interface.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerable" /> list of nodes.</returns>
		/// <param name="xPathExpression">The XPath expression to evaluate. For details, see <see cref="T:System.Web.UI.XPathBinder" />.</param>
		// Token: 0x060015F0 RID: 5616 RVA: 0x0003B4EF File Offset: 0x000396EF
		protected IEnumerable XPathSelect(string xPathExpression)
		{
			return this.Page.XPathSelect(xPathExpression);
		}

		/// <summary>Evaluates an XPath data-binding expression using the specified prefix and namespace mappings for namespace resolution and returns a node collection that implements the <see cref="T:System.Collections.IEnumerable" /> interface.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerable" /> list of nodes. </returns>
		/// <param name="xPathExpression">The XPath expression to evaluate. For details, see <see cref="T:System.Web.UI.XPathBinder" />. </param>
		/// <param name="resolver">A set of prefix and namespace mappings used to for namespace resolution. </param>
		// Token: 0x060015F1 RID: 5617 RVA: 0x0003B4FD File Offset: 0x000396FD
		protected IEnumerable XPathSelect(string xPathExpression, IXmlNamespaceResolver resolver)
		{
			return this.Page.XPathSelect(xPathExpression, resolver);
		}

		/// <summary>When overridden a derived class, gets the relative URL of the directory for the <see cref="T:System.Web.UI.PageTheme" /> object.</summary>
		/// <returns>A string value containing the relative URL of the <see cref="T:System.Web.UI.PageTheme" /> directory.</returns>
		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x060015F2 RID: 5618
		protected abstract string AppRelativeTemplateSourceDirectory { get; }

		/// <summary>When overridden in a derived class, gets an <see cref="T:System.Collections.IDictionary" /> interface of the names of all default skins that are available for the current page theme, indexed by control type.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> that represents the control skins associated with the current page theme.</returns>
		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x060015F3 RID: 5619
		protected abstract IDictionary ControlSkins { get; }

		/// <summary>When overridden in a derived class, gets an array of style sheets that are linked to this page.</summary>
		/// <returns>A string array of style sheets linked to this page.</returns>
		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x060015F4 RID: 5620
		protected abstract string[] LinkedStyleSheets { get; }

		/// <summary>Gets the <see cref="T:System.Web.UI.Page" /> object that is associated with the instance of the <see cref="T:System.Web.UI.PageTheme" /> class.</summary>
		/// <returns>The <see cref="T:System.Web.UI.Page" /> associated with the <see cref="T:System.Web.UI.PageTheme" />.</returns>
		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x060015F5 RID: 5621 RVA: 0x0003B50C File Offset: 0x0003970C
		protected Page Page
		{
			get
			{
				return this._page;
			}
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x0003B514 File Offset: 0x00039714
		internal void SetPage(Page page)
		{
			this._page = page;
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x0003B520 File Offset: 0x00039720
		internal ControlSkin GetControlSkin(Type controlType, string skinID)
		{
			object obj = PageTheme.CreateSkinKey(controlType, skinID);
			return this.ControlSkins[obj] as ControlSkin;
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x0003B546 File Offset: 0x00039746
		internal string[] GetStyleSheets()
		{
			return this.LinkedStyleSheets;
		}

		// Token: 0x0400153E RID: 5438
		private Page _page;
	}
}
