using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace System.Web.UI.WebControls
{
	/// <summary>Displays an XML document without formatting or using Extensible Stylesheet Language Transformations (XSLT).</summary>
	// Token: 0x02000451 RID: 1105
	[PersistChildren(true)]
	[Designer("System.Web.UI.Design.WebControls.XmlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ControlBuilder(typeof(XmlBuilder))]
	[DefaultProperty("DocumentSource")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class Xml : Control
	{
		/// <summary>Overrides the <see cref="P:System.Web.UI.Control.ClientID" /> property and returns the base server control identifier.</summary>
		/// <returns>The base server control identifier.</returns>
		// Token: 0x1700102C RID: 4140
		// (get) Token: 0x06003331 RID: 13105 RVA: 0x00032ABF File Offset: 0x00030CBF
		[global::System.MonoTODO("Anything else?")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ClientID
		{
			get
			{
				return base.ClientID;
			}
		}

		/// <summary>Overrides the <see cref="P:System.Web.UI.Control.Controls" /> property and returns the base <see cref="T:System.Web.UI.ControlCollection" /> collection.</summary>
		/// <returns>The base <see cref="T:System.Web.UI.ControlCollection" /> collection.</returns>
		// Token: 0x1700102D RID: 4141
		// (get) Token: 0x06003332 RID: 13106 RVA: 0x00032AC7 File Offset: 0x00030CC7
		[global::System.MonoTODO("Anything else?")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override ControlCollection Controls
		{
			get
			{
				return base.Controls;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.XmlDocument" /> to display in the <see cref="T:System.Web.UI.WebControls.Xml" /> control.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlDocument" /> to display in the <see cref="T:System.Web.UI.WebControls.Xml" /> control.</returns>
		// Token: 0x1700102E RID: 4142
		// (get) Token: 0x06003333 RID: 13107 RVA: 0x000896E4 File Offset: 0x000878E4
		// (set) Token: 0x06003334 RID: 13108 RVA: 0x000896EC File Offset: 0x000878EC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Obsolete("Use the XPathNavigator property instead by creating an XPathDocument and calling CreateNavigator().")]
		public XmlDocument Document
		{
			get
			{
				return this.xml_document;
			}
			set
			{
				this.xml_content = null;
				this.xml_file = null;
				this.xml_document = value;
			}
		}

		/// <summary>Sets a string that contains the XML document to display in the <see cref="T:System.Web.UI.WebControls.Xml" /> control.</summary>
		/// <returns>A string that contains the XML document to display in the <see cref="T:System.Web.UI.WebControls.Xml" /> control.</returns>
		// Token: 0x1700102F RID: 4143
		// (get) Token: 0x06003335 RID: 13109 RVA: 0x00089703 File Offset: 0x00087903
		// (set) Token: 0x06003336 RID: 13110 RVA: 0x00089719 File Offset: 0x00087919
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string DocumentContent
		{
			get
			{
				if (this.xml_content == null)
				{
					return "";
				}
				return this.xml_content;
			}
			set
			{
				this.xml_content = value;
				this.xml_file = null;
				this.xml_document = null;
			}
		}

		/// <summary>Gets or sets the path to an XML document to display in the <see cref="T:System.Web.UI.WebControls.Xml" /> control.</summary>
		/// <returns>The path to an XML document to display in the <see cref="T:System.Web.UI.WebControls.Xml" /> control.</returns>
		// Token: 0x17001030 RID: 4144
		// (get) Token: 0x06003337 RID: 13111 RVA: 0x00089730 File Offset: 0x00087930
		// (set) Token: 0x06003338 RID: 13112 RVA: 0x00089746 File Offset: 0x00087946
		[global::System.MonoLimitation("Absolute path to the file system is not supported; use a relative URI instead.")]
		[WebSysDescription("")]
		[Editor("System.Web.UI.Design.XmlUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[UrlProperty]
		[DefaultValue("")]
		[WebCategory("Behavior")]
		public string DocumentSource
		{
			get
			{
				if (this.xml_file == null)
				{
					return "";
				}
				return this.xml_file;
			}
			set
			{
				this.xml_content = null;
				this.xml_file = value;
				this.xml_document = null;
			}
		}

		/// <summary>Overrides the <see cref="P:System.Web.UI.Control.EnableTheming" /> property. This property is not supported by the <see cref="T:System.Web.UI.WebControls.Xml" /> class.</summary>
		/// <returns>Always returns false. This property is not supported.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt is made to set the value of this property.</exception>
		// Token: 0x17001031 RID: 4145
		// (get) Token: 0x06003339 RID: 13113 RVA: 0x00008A69 File Offset: 0x00006C69
		// (set) Token: 0x0600333A RID: 13114 RVA: 0x00003A01 File Offset: 0x00001C01
		[Browsable(false)]
		[DefaultValue(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool EnableTheming
		{
			get
			{
				return false;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Overrides the <see cref="P:System.Web.UI.Control.SkinID" /> property. This property is not supported by the <see cref="T:System.Web.UI.WebControls.Xml" /> class.</summary>
		/// <returns>Always returns an empty string (""). This property is not supported.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt is made to set the value of this property.</exception>
		// Token: 0x17001032 RID: 4146
		// (get) Token: 0x0600333B RID: 13115 RVA: 0x0000EE9B File Offset: 0x0000D09B
		// (set) Token: 0x0600333C RID: 13116 RVA: 0x0008975D File Offset: 0x0008795D
		[DefaultValue("")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public override string SkinID
		{
			get
			{
				return string.Empty;
			}
			set
			{
				throw new NotSupportedException("SkinID is not supported on Xml control");
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.Xsl.XslTransform" /> object that formats the XML document before it is written to the output stream.</summary>
		/// <returns>The <see cref="T:System.Xml.Xsl.XslTransform" /> that formats the XML document before it is written to the output stream.</returns>
		// Token: 0x17001033 RID: 4147
		// (get) Token: 0x0600333D RID: 13117 RVA: 0x00089769 File Offset: 0x00087969
		// (set) Token: 0x0600333E RID: 13118 RVA: 0x00089771 File Offset: 0x00087971
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public XslTransform Transform
		{
			get
			{
				return this.xsl_transform;
			}
			set
			{
				this.transform_file = null;
				this.xsl_transform = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Xml.Xsl.XsltArgumentList" /> that contains a list of optional arguments passed to the style sheet and used during the Extensible Stylesheet Language Transformation (XSLT).</summary>
		/// <returns>A <see cref="T:System.Xml.Xsl.XsltArgumentList" /> that contains a list of optional arguments passed to the style sheet and used during the XSL Transformation.</returns>
		// Token: 0x17001034 RID: 4148
		// (get) Token: 0x0600333F RID: 13119 RVA: 0x00089781 File Offset: 0x00087981
		// (set) Token: 0x06003340 RID: 13120 RVA: 0x00089789 File Offset: 0x00087989
		[WebCategory("Behavior")]
		[WebSysDescription("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public XsltArgumentList TransformArgumentList
		{
			get
			{
				return this.transform_arguments;
			}
			set
			{
				this.transform_arguments = value;
			}
		}

		/// <summary>Gets or sets the path to an Extensible Stylesheet Language Transformation (XSLT) style sheet that formats the XML document before it is written to the output stream.</summary>
		/// <returns>The path to an XSL Transformation style sheet that formats the XML document before it is written to the output stream.</returns>
		// Token: 0x17001035 RID: 4149
		// (get) Token: 0x06003341 RID: 13121 RVA: 0x00089792 File Offset: 0x00087992
		// (set) Token: 0x06003342 RID: 13122 RVA: 0x000897A8 File Offset: 0x000879A8
		[Editor("System.Web.UI.Design.XslUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[global::System.MonoLimitation("Absolute path to the file system is not supported; use a relative URI instead.")]
		[DefaultValue("")]
		public string TransformSource
		{
			get
			{
				if (this.transform_file == null)
				{
					return "";
				}
				return this.transform_file;
			}
			set
			{
				this.transform_file = value;
				this.xsl_transform = null;
			}
		}

		/// <summary>Gets or sets a cursor model for navigating and editing the XML data associated with the <see cref="T:System.Web.UI.WebControls.Xml" /> control.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNavigator" /> object.</returns>
		// Token: 0x17001036 RID: 4150
		// (get) Token: 0x06003343 RID: 13123 RVA: 0x000897B8 File Offset: 0x000879B8
		// (set) Token: 0x06003344 RID: 13124 RVA: 0x000897C0 File Offset: 0x000879C0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public XPathNavigator XPathNavigator
		{
			get
			{
				return this.xpath_navigator;
			}
			set
			{
				this.xpath_navigator = value;
			}
		}

		/// <summary>Searches the page naming container for the specified server control.</summary>
		/// <returns>The specified control; otherwise, null if the specified control does not exist.</returns>
		/// <param name="id">The identifier for the control to be found.</param>
		// Token: 0x06003345 RID: 13125 RVA: 0x00003BEA File Offset: 0x00001DEA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Control FindControl(string id)
		{
			return null;
		}

		/// <summary>Overrides the <see cref="M:System.Web.UI.Control.Focus" /> method. This method is not supported by the <see cref="T:System.Web.UI.WebControls.Xml" /> class.</summary>
		/// <exception cref="T:System.NotSupportedException">An attempt is made to invoke this method.</exception>
		// Token: 0x06003346 RID: 13126 RVA: 0x00003A01 File Offset: 0x00001C01
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void Focus()
		{
			throw new NotSupportedException();
		}

		/// <summary>Determines whether the server control contains any child controls.</summary>
		/// <returns>true if the control contains other controls; otherwise, false.</returns>
		// Token: 0x06003347 RID: 13127 RVA: 0x00008A69 File Offset: 0x00006C69
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool HasControls()
		{
			return false;
		}

		/// <summary>Renders the results to the output stream.</summary>
		/// <param name="output">The result of the output stream.</param>
		// Token: 0x06003348 RID: 13128 RVA: 0x000897CC File Offset: 0x000879CC
		protected internal override void Render(HtmlTextWriter output)
		{
			XmlDocument xmlDocument = null;
			if (this.xpath_navigator == null)
			{
				if (this.xml_document != null)
				{
					xmlDocument = this.xml_document;
				}
				else if (this.xml_content != null)
				{
					xmlDocument = new XmlDocument();
					xmlDocument.LoadXml(this.xml_content);
				}
				else
				{
					if (this.xml_file == null)
					{
						return;
					}
					xmlDocument = new XmlDocument();
					xmlDocument.Load(base.MapPathSecure(this.xml_file));
				}
			}
			XslTransform xslTransform = this.xsl_transform;
			if (this.transform_file != null)
			{
				xslTransform = new XslTransform();
				xslTransform.Load(base.MapPathSecure(this.transform_file));
			}
			if (xslTransform != null)
			{
				if (this.xpath_navigator != null)
				{
					xslTransform.Transform(this.xpath_navigator, this.transform_arguments, output);
					return;
				}
				xslTransform.Transform(xmlDocument, this.transform_arguments, output, null);
				return;
			}
			else
			{
				XmlTextWriter xmlTextWriter = new XmlTextWriter(output);
				xmlTextWriter.Formatting = Formatting.None;
				if (this.xpath_navigator != null)
				{
					xmlTextWriter.WriteStartDocument();
					this.xpath_navigator.WriteSubtree(xmlTextWriter);
					return;
				}
				xmlDocument.Save(xmlTextWriter);
				return;
			}
		}

		/// <param name="obj">An <see cref="T:System.Object" /> that represents the <see cref="T:System.Web.UI.LiteralControl" /> to add.</param>
		/// <exception cref="T:System.Web.HttpException">
		///   <paramref name="obj" /> is not of type <see cref="T:System.Web.UI.LiteralControl" />.</exception>
		// Token: 0x06003349 RID: 13129 RVA: 0x000898BC File Offset: 0x00087ABC
		protected override void AddParsedSubObject(object obj)
		{
			LiteralControl literalControl = obj as LiteralControl;
			if (literalControl != null)
			{
				this.xml_document = new XmlDocument();
				this.xml_document.LoadXml(literalControl.Text);
				return;
			}
			throw new HttpException(string.Format("Objects of type {0} are not supported as children of the Xml control", obj.GetType()));
		}

		/// <summary>Creates a new <see cref="T:System.Web.UI.EmptyControlCollection" /> object.</summary>
		/// <returns>Always returns an <see cref="T:System.Web.UI.EmptyControlCollection" />.</returns>
		// Token: 0x0600334A RID: 13130 RVA: 0x00032889 File Offset: 0x00030A89
		protected override ControlCollection CreateControlCollection()
		{
			return new EmptyControlCollection(this);
		}

		/// <summary>Gets design-time data for a control.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> containing the design-time data for the <see cref="T:System.Web.UI.WebControls.Xml" /> control.</returns>
		// Token: 0x0600334B RID: 13131 RVA: 0x00003BEA File Offset: 0x00001DEA
		[global::System.MonoTODO("Always returns null")]
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		protected override IDictionary GetDesignModeState()
		{
			return null;
		}

		// Token: 0x04001CC2 RID: 7362
		private XmlDocument xml_document;

		// Token: 0x04001CC3 RID: 7363
		private XPathNavigator xpath_navigator;

		// Token: 0x04001CC4 RID: 7364
		private string xml_content;

		// Token: 0x04001CC5 RID: 7365
		private string xml_file;

		// Token: 0x04001CC6 RID: 7366
		private XslTransform xsl_transform;

		// Token: 0x04001CC7 RID: 7367
		private XsltArgumentList transform_arguments;

		// Token: 0x04001CC8 RID: 7368
		private string transform_file;
	}
}
