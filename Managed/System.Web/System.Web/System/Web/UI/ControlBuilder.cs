using System;
using System.CodeDom;
using System.Collections;
using System.Configuration;
using System.Reflection;
using System.Security.Permissions;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using System.Web.Util;
using Unity;

namespace System.Web.UI
{
	/// <summary>Supports the page parser in building a control and the child controls it contains.</summary>
	// Token: 0x020001B6 RID: 438
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ControlBuilder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ControlBuilder" /> class.</summary>
		// Token: 0x06001174 RID: 4468 RVA: 0x00030693 File Offset: 0x0002E893
		public ControlBuilder()
		{
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x000306A4 File Offset: 0x0002E8A4
		internal ControlBuilder(TemplateParser parser, ControlBuilder parentBuilder, Type type, string tagName, string id, IDictionary attribs, int line, string sourceFileName)
		{
			this.parser = parser;
			this.parserType = ((parser != null) ? parser.GetType() : null);
			this.parentBuilder = parentBuilder;
			this.type = type;
			this.tagName = tagName;
			this.id = id;
			this.attribs = attribs;
			this.line = line;
			this.fileName = sourceFileName;
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x0003070D File Offset: 0x0002E90D
		internal void EnsureOtherTags()
		{
			if (this.otherTags == null)
			{
				this.otherTags = new ArrayList();
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06001177 RID: 4471 RVA: 0x00030722 File Offset: 0x0002E922
		internal ControlBuilder ParentBuilder
		{
			get
			{
				return this.parentBuilder;
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06001178 RID: 4472 RVA: 0x0003072A File Offset: 0x0002E92A
		internal IDictionary Attributes
		{
			get
			{
				return this.attribs;
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06001179 RID: 4473 RVA: 0x00030732 File Offset: 0x0002E932
		// (set) Token: 0x0600117A RID: 4474 RVA: 0x0003073A File Offset: 0x0002E93A
		internal int Line
		{
			get
			{
				return this.line;
			}
			set
			{
				this.line = value;
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x0600117B RID: 4475 RVA: 0x00030743 File Offset: 0x0002E943
		// (set) Token: 0x0600117C RID: 4476 RVA: 0x0003074B File Offset: 0x0002E94B
		internal string FileName
		{
			get
			{
				return this.fileName;
			}
			set
			{
				this.fileName = value;
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x0600117D RID: 4477 RVA: 0x00030754 File Offset: 0x0002E954
		internal ControlBuilder DefaultPropertyBuilder
		{
			get
			{
				return this.defaultPropertyBuilder;
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x0600117E RID: 4478 RVA: 0x0003075C File Offset: 0x0002E95C
		// (set) Token: 0x0600117F RID: 4479 RVA: 0x00030764 File Offset: 0x0002E964
		internal bool HaveParserVariable
		{
			get
			{
				return this.haveParserVariable;
			}
			set
			{
				this.haveParserVariable = value;
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06001180 RID: 4480 RVA: 0x0003076D File Offset: 0x0002E96D
		// (set) Token: 0x06001181 RID: 4481 RVA: 0x00030775 File Offset: 0x0002E975
		internal CodeMemberMethod Method
		{
			get
			{
				return this.method;
			}
			set
			{
				this.method = value;
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06001182 RID: 4482 RVA: 0x0003077E File Offset: 0x0002E97E
		// (set) Token: 0x06001183 RID: 4483 RVA: 0x00030786 File Offset: 0x0002E986
		internal CodeMemberMethod DataBindingMethod { get; set; }

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06001184 RID: 4484 RVA: 0x0003078F File Offset: 0x0002E98F
		// (set) Token: 0x06001185 RID: 4485 RVA: 0x00030797 File Offset: 0x0002E997
		internal CodeStatementCollection MethodStatements
		{
			get
			{
				return this.methodStatements;
			}
			set
			{
				this.methodStatements = value;
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06001186 RID: 4486 RVA: 0x000307A0 File Offset: 0x0002E9A0
		// (set) Token: 0x06001187 RID: 4487 RVA: 0x000307A8 File Offset: 0x0002E9A8
		internal CodeMemberMethod RenderMethod
		{
			get
			{
				return this.renderMethod;
			}
			set
			{
				this.renderMethod = value;
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06001188 RID: 4488 RVA: 0x000307B1 File Offset: 0x0002E9B1
		internal int RenderIndex
		{
			get
			{
				return this.renderIndex;
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06001189 RID: 4489 RVA: 0x000307B9 File Offset: 0x0002E9B9
		internal bool IsProperty
		{
			get
			{
				return this.isProperty;
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x0600118A RID: 4490 RVA: 0x000307C1 File Offset: 0x0002E9C1
		internal bool IsPropertyWritable
		{
			get
			{
				return this.isPropertyWritable;
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x0600118B RID: 4491 RVA: 0x000307C9 File Offset: 0x0002E9C9
		// (set) Token: 0x0600118C RID: 4492 RVA: 0x000307D1 File Offset: 0x0002E9D1
		internal ILocation Location
		{
			get
			{
				return this.location;
			}
			set
			{
				this.location = new Location(value);
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x0600118D RID: 4493 RVA: 0x000307DF File Offset: 0x0002E9DF
		internal ArrayList OtherTags
		{
			get
			{
				return this.otherTags;
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> for the control to be created.</summary>
		/// <returns>The <see cref="T:System.Type" /> for the control to be created.</returns>
		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x0600118E RID: 4494 RVA: 0x000307E7 File Offset: 0x0002E9E7
		public Type ControlType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Gets a value that determines whether the control has a <see cref="T:System.Web.UI.ParseChildrenAttribute" /> with <see cref="P:System.Web.UI.ParseChildrenAttribute.ChildrenAsProperties" /> set to true.</summary>
		/// <returns>true if the control has a <see cref="T:System.Web.UI.ParseChildrenAttribute" /> with <see cref="P:System.Web.UI.ParseChildrenAttribute.ChildrenAsProperties" /> set to true, otherwise false.</returns>
		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x0600118F RID: 4495 RVA: 0x000307EF File Offset: 0x0002E9EF
		protected bool FChildrenAsProperties
		{
			get
			{
				return this.childrenAsProperties;
			}
		}

		/// <summary>Gets a value that determines whether the control implements the <see cref="T:System.Web.UI.IParserAccessor" /> interface.</summary>
		/// <returns>false if the control implements the <see cref="T:System.Web.UI.IParserAccessor" /> interface, otherwise true.</returns>
		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001190 RID: 4496 RVA: 0x000307F7 File Offset: 0x0002E9F7
		protected bool FIsNonParserAccessor
		{
			get
			{
				return !this.isIParserAccessor;
			}
		}

		/// <summary>Gets a value indicating whether the control contains any code blocks.</summary>
		/// <returns>true if the control contains at least one code block; otherwise, false.</returns>
		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001191 RID: 4497 RVA: 0x00030802 File Offset: 0x0002EA02
		public bool HasAspCode
		{
			get
			{
				return this.hasAspCode;
			}
		}

		/// <summary>Gets or sets the identifier property for the control to be built.</summary>
		/// <returns>The identifier property for the control.</returns>
		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06001192 RID: 4498 RVA: 0x0003080A File Offset: 0x0002EA0A
		// (set) Token: 0x06001193 RID: 4499 RVA: 0x00030812 File Offset: 0x0002EA12
		public string ID
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06001194 RID: 4500 RVA: 0x0003081B File Offset: 0x0002EA1B
		internal ArrayList Children
		{
			get
			{
				return this.children;
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06001195 RID: 4501 RVA: 0x00030823 File Offset: 0x0002EA23
		internal ArrayList TemplateChildren
		{
			get
			{
				return this.templateChildren;
			}
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x0003082B File Offset: 0x0002EA2B
		internal void SetControlType(Type t)
		{
			this.type = t;
		}

		/// <summary>Returns whether the <see cref="T:System.Web.UI.ControlBuilder" /> is running in the designer.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.ControlBuilder" /> is running in the designer; otherwise, false.</returns>
		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06001197 RID: 4503 RVA: 0x00008A69 File Offset: 0x00006C69
		protected bool InDesigner
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the type of the naming container for the control that this builder creates.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represent the type of the naming container for the control that this builder creates.</returns>
		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001198 RID: 4504 RVA: 0x00030834 File Offset: 0x0002EA34
		public Type NamingContainerType
		{
			get
			{
				ControlBuilder controlBuilder = this.myNamingContainer;
				if (controlBuilder == null)
				{
					return typeof(Control);
				}
				return controlBuilder.ControlType;
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001199 RID: 4505 RVA: 0x0003085C File Offset: 0x0002EA5C
		internal bool IsNamingContainer
		{
			get
			{
				return !(this.type == null) && typeof(INamingContainer).IsAssignableFrom(this.type);
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x0600119A RID: 4506 RVA: 0x00030883 File Offset: 0x0002EA83
		internal bool IsTemplate
		{
			get
			{
				if (this.isTemplate == null)
				{
					this.isTemplate = new bool?(typeof(TemplateBuilder).IsAssignableFrom(base.GetType()));
				}
				return this.isTemplate.Value;
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x0600119B RID: 4507 RVA: 0x000308BD File Offset: 0x0002EABD
		internal bool PropertyBuilderShouldReturnValue
		{
			get
			{
				return this.isProperty && this.isPropertyWritable && this.RenderMethod == null && !this.IsTemplate && !(this is CollectionBuilder) && !(this is RootBuilder);
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x0600119C RID: 4508 RVA: 0x000308F8 File Offset: 0x0002EAF8
		private ControlBuilder MyNamingContainer
		{
			get
			{
				if (this.myNamingContainer == null)
				{
					Type type = ((this.parentBuilder != null) ? this.parentBuilder.ControlType : null);
					if (this.parentBuilder == null && type == null)
					{
						this.myNamingContainer = null;
					}
					else if (this.parentBuilder is TemplateBuilder)
					{
						this.myNamingContainer = this.parentBuilder;
					}
					else if (type != null && typeof(INamingContainer).IsAssignableFrom(type))
					{
						this.myNamingContainer = this.parentBuilder;
					}
					else
					{
						this.myNamingContainer = this.parentBuilder.MyNamingContainer;
					}
				}
				return this.myNamingContainer;
			}
		}

		/// <summary>Gets the type of the binding container for the control that this builder creates.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represent the type of the binding container for the control that this builder creates.</returns>
		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x0600119D RID: 4509 RVA: 0x0003099C File Offset: 0x0002EB9C
		public virtual Type BindingContainerType
		{
			get
			{
				ControlBuilder controlBuilder = ((this is TemplateBuilder && !(this is RootBuilder)) ? this : this.MyNamingContainer);
				if (controlBuilder == null)
				{
					if (this is RootBuilder && this.parserType == typeof(PageParser))
					{
						return typeof(Page);
					}
					return typeof(Control);
				}
				else
				{
					if (controlBuilder != this && controlBuilder is ContentBuilderInternal && !typeof(INonBindingContainer).IsAssignableFrom(controlBuilder.BindingContainerType))
					{
						return controlBuilder.BindingContainerType;
					}
					if (controlBuilder is TemplateBuilder)
					{
						Type type = ((TemplateBuilder)controlBuilder).ContainerType;
						if (typeof(INonBindingContainer).IsAssignableFrom(type))
						{
							return this.MyNamingContainer.BindingContainerType;
						}
						if (type != null)
						{
							return type;
						}
						type = controlBuilder.ControlType;
						if (type == null)
						{
							return typeof(Control);
						}
						if (typeof(INonBindingContainer).IsAssignableFrom(type) || !typeof(INamingContainer).IsAssignableFrom(type))
						{
							return this.MyNamingContainer.BindingContainerType;
						}
						return type;
					}
					else
					{
						Type type = controlBuilder.ControlType;
						if (type == null)
						{
							return typeof(Control);
						}
						if (typeof(INonBindingContainer).IsAssignableFrom(type) || !typeof(INamingContainer).IsAssignableFrom(type))
						{
							return this.MyNamingContainer.BindingContainerType;
						}
						return controlBuilder.ControlType;
					}
				}
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x0600119E RID: 4510 RVA: 0x00030B03 File Offset: 0x0002ED03
		internal TemplateBuilder ParentTemplateBuilder
		{
			get
			{
				if (this.parentBuilder == null)
				{
					return null;
				}
				if (this.parentBuilder is TemplateBuilder)
				{
					return (TemplateBuilder)this.parentBuilder;
				}
				return this.parentBuilder.ParentTemplateBuilder;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.TemplateParser" /> responsible for parsing the control.</summary>
		/// <returns>The <see cref="T:System.Web.UI.TemplateParser" /> used to parse the control.</returns>
		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x0600119F RID: 4511 RVA: 0x00030B33 File Offset: 0x0002ED33
		protected TemplateParser Parser
		{
			get
			{
				return this.parser;
			}
		}

		/// <summary>Gets the tag name for the control to be built.</summary>
		/// <returns>The tag name for the control.</returns>
		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x060011A0 RID: 4512 RVA: 0x00030B3B File Offset: 0x0002ED3B
		public string TagName
		{
			get
			{
				return this.tagName;
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x060011A1 RID: 4513 RVA: 0x00030B43 File Offset: 0x0002ED43
		internal string OriginalTagName
		{
			get
			{
				if (this.originalTagName == null || this.originalTagName.Length == 0)
				{
					return this.TagName;
				}
				return this.originalTagName;
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x060011A2 RID: 4514 RVA: 0x00030B67 File Offset: 0x0002ED67
		internal RootBuilder Root
		{
			get
			{
				if (typeof(RootBuilder).IsAssignableFrom(base.GetType()))
				{
					return (RootBuilder)this;
				}
				return this.parentBuilder.Root;
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x060011A3 RID: 4515 RVA: 0x000307EF File Offset: 0x0002E9EF
		internal bool ChildrenAsProperties
		{
			get
			{
				return this.childrenAsProperties;
			}
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x00030B92 File Offset: 0x0002ED92
		internal string GetAttribute(string name)
		{
			if (this.attribs == null)
			{
				return null;
			}
			return this.attribs[name] as string;
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x00030BAF File Offset: 0x0002EDAF
		internal void IncreaseRenderIndex()
		{
			this.renderIndex++;
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x00030BC0 File Offset: 0x0002EDC0
		private void AddChild(object child)
		{
			if (this.children == null)
			{
				this.children = new ArrayList();
			}
			this.children.Add(child);
			ControlBuilder controlBuilder = child as ControlBuilder;
			if (controlBuilder != null && controlBuilder is TemplateBuilder)
			{
				if (this.templateChildren == null)
				{
					this.templateChildren = new ArrayList();
				}
				this.templateChildren.Add(child);
			}
			if (this.parser == null)
			{
				return;
			}
			string text = ((controlBuilder != null) ? controlBuilder.TagName : null);
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			RootBuilder root = this.Root;
			AspComponentFoundry aspComponentFoundry = ((root != null) ? root.Foundry : null);
			if (aspComponentFoundry == null)
			{
				return;
			}
			AspComponent component = aspComponentFoundry.GetComponent(text);
			if (component == null || !component.FromConfig)
			{
				return;
			}
			this.parser.AddImport(component.Namespace);
			this.parser.AddDependency(component.Source);
		}

		/// <summary>Determines whether white space literals are permitted in the content between a control's opening and closing tags. This method is called by the ASP.NET page framework.</summary>
		/// <returns>Always returns true.</returns>
		// Token: 0x060011A7 RID: 4519 RVA: 0x00008B66 File Offset: 0x00006D66
		public virtual bool AllowWhitespaceLiterals()
		{
			return true;
		}

		/// <summary>Adds the specified literal content to a control. This method is called by the ASP.NET page framework.</summary>
		/// <param name="s">The content to add to the control.</param>
		/// <exception cref="T:System.Web.HttpException">The string literal is not well formed. </exception>
		// Token: 0x060011A8 RID: 4520 RVA: 0x00030C94 File Offset: 0x0002EE94
		public virtual void AppendLiteralString(string s)
		{
			if (s == null || s.Length == 0)
			{
				return;
			}
			if (this.childrenAsProperties || !this.isIParserAccessor)
			{
				if (this.defaultPropertyBuilder != null)
				{
					this.defaultPropertyBuilder.AppendLiteralString(s);
					return;
				}
				if (s.Trim().Length != 0)
				{
					throw new HttpException(string.Format("Literal content not allowed for '{0}' {1} \"{2}\"", this.tagName, base.GetType(), s));
				}
				return;
			}
			else
			{
				if (!this.AllowWhitespaceLiterals() && s.Trim().Length == 0)
				{
					return;
				}
				if (this.HtmlDecodeLiterals())
				{
					s = HttpUtility.HtmlDecode(s);
				}
				this.AddChild(s);
				return;
			}
		}

		/// <summary>Adds builders to the <see cref="T:System.Web.UI.ControlBuilder" /> object for any child controls that belong to the container control.</summary>
		/// <param name="subBuilder">The <see cref="T:System.Web.UI.ControlBuilder" /> object assigned to the child control. </param>
		// Token: 0x060011A9 RID: 4521 RVA: 0x00030D2C File Offset: 0x0002EF2C
		public virtual void AppendSubBuilder(ControlBuilder subBuilder)
		{
			subBuilder.OnAppendToParentBuilder(this);
			subBuilder.parentBuilder = this;
			if (this.childrenAsProperties)
			{
				this.AppendToProperty(subBuilder);
				return;
			}
			if (typeof(CodeRenderBuilder).IsAssignableFrom(subBuilder.GetType()))
			{
				this.AppendCode(subBuilder);
				return;
			}
			this.AddChild(subBuilder);
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x00030D7D File Offset: 0x0002EF7D
		private void AppendToProperty(ControlBuilder subBuilder)
		{
			if (typeof(CodeRenderBuilder) == subBuilder.GetType())
			{
				throw new HttpException("Code render not supported here.");
			}
			if (this.defaultPropertyBuilder != null)
			{
				this.defaultPropertyBuilder.AppendSubBuilder(subBuilder);
				return;
			}
			this.AddChild(subBuilder);
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x00030DC0 File Offset: 0x0002EFC0
		private void AppendCode(ControlBuilder subBuilder)
		{
			if (this.type != null && !typeof(Control).IsAssignableFrom(this.type))
			{
				throw new HttpException("Code render not supported here.");
			}
			if (typeof(CodeRenderBuilder) == subBuilder.GetType())
			{
				this.hasAspCode = true;
			}
			this.AddChild(subBuilder);
		}

		/// <summary>Called by the parser to inform the builder that the parsing of the control's opening and closing tags is complete.</summary>
		// Token: 0x060011AC RID: 4524 RVA: 0x0000393A File Offset: 0x00001B3A
		public virtual void CloseControl()
		{
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x00030E24 File Offset: 0x0002F024
		private static Type MapTagType(Type tagType)
		{
			if (tagType == null)
			{
				return null;
			}
			PagesSection pagesSection = WebConfigurationManager.GetSection("system.web/pages") as PagesSection;
			if (pagesSection == null)
			{
				return tagType;
			}
			TagMapCollection tagMapping = pagesSection.TagMapping;
			if (tagMapping == null || tagMapping.Count == 0)
			{
				return tagType;
			}
			string text = tagType.ToString();
			string text2 = string.Empty;
			string text3 = string.Empty;
			foreach (object obj in tagMapping)
			{
				TagMapInfo tagMapInfo = (TagMapInfo)obj;
				Exception ex = null;
				Type type = null;
				bool flag;
				try
				{
					text2 = tagMapInfo.TagType;
					type = HttpApplication.LoadType(text2);
					flag = type == null;
				}
				catch (Exception ex2)
				{
					flag = true;
					ex = ex2;
				}
				if (flag)
				{
					throw new HttpException(string.Format("Could not load type {0}", text2), ex);
				}
				if (text2 == text)
				{
					text3 = tagMapInfo.MappedTagType;
					ex = null;
					Type type2 = null;
					try
					{
						type2 = HttpApplication.LoadType(text3);
						flag = type2 == null;
					}
					catch (Exception ex3)
					{
						flag = true;
						ex = ex3;
					}
					if (flag)
					{
						throw new HttpException(string.Format("Could not load type {0}", text3), ex);
					}
					if (!type2.IsSubclassOf(type))
					{
						throw new ConfigurationErrorsException(string.Format("The specified type '{0}' used for mapping must inherit from the original type '{1}'.", text3, text2));
					}
					return type2;
				}
			}
			return tagType;
		}

		/// <summary>Creates a <see cref="T:System.Web.UI.ControlBuilder" /> object from the specified tag name and object type, as well as other parameters defining the builder.</summary>
		/// <returns>The builder that is responsible for creating the control.</returns>
		/// <param name="parser">The <see cref="T:System.Web.UI.TemplateParser" /> object responsible for parsing the control. </param>
		/// <param name="parentBuilder">The <see cref="T:System.Web.UI.ControlBuilder" /> object responsible for building the parent control. </param>
		/// <param name="type">The <see cref="T:System.Type" /> of the object that the builder will create. </param>
		/// <param name="tagName">The name of the tag to be built. This allows the builder to support multiple tag types. </param>
		/// <param name="id">The <see cref="P:System.Web.UI.ControlBuilder.ID" /> attribute assigned to the control. </param>
		/// <param name="attribs">The <see cref="T:System.Collections.IDictionary" /> object that holds all the specified tag attributes. </param>
		/// <param name="line">The source file line number for the specified control. </param>
		/// <param name="sourceFileName">The name of the source file from which the control is to be created. </param>
		// Token: 0x060011AE RID: 4526 RVA: 0x00030FA4 File Offset: 0x0002F1A4
		public static ControlBuilder CreateBuilderFromType(TemplateParser parser, ControlBuilder parentBuilder, Type type, string tagName, string id, IDictionary attribs, int line, string sourceFileName)
		{
			Type type2 = ControlBuilder.MapTagType(type);
			object[] customAttributes = type2.GetCustomAttributes(typeof(ControlBuilderAttribute), true);
			ControlBuilder controlBuilder;
			if (customAttributes != null && customAttributes.Length != 0)
			{
				controlBuilder = (ControlBuilder)Activator.CreateInstance(((ControlBuilderAttribute)customAttributes[0]).BuilderType);
			}
			else
			{
				controlBuilder = new ControlBuilder();
			}
			controlBuilder.Init(parser, parentBuilder, type2, tagName, id, attribs);
			controlBuilder.line = line;
			controlBuilder.fileName = sourceFileName;
			return controlBuilder;
		}

		/// <summary>Obtains the <see cref="T:System.Type" /> of the control type corresponding to a child tag. This method is called by the ASP.NET page framework.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the specified control's child.</returns>
		/// <param name="tagName">The tag name of the child. </param>
		/// <param name="attribs">An array of attributes contained in the child control. </param>
		// Token: 0x060011AF RID: 4527 RVA: 0x00003BEA File Offset: 0x00001DEA
		public virtual Type GetChildControlType(string tagName, IDictionary attribs)
		{
			return null;
		}

		/// <summary>Determines if a control has both an opening and closing tag. This method is called by the ASP.NET page framework.</summary>
		/// <returns>true if the control has an opening and closing tag; otherwise, false.</returns>
		// Token: 0x060011B0 RID: 4528 RVA: 0x00008B66 File Offset: 0x00006D66
		public virtual bool HasBody()
		{
			return true;
		}

		/// <summary>Determines whether the literal string of an HTML control must be HTML decoded. This method is called by the ASP.NET page framework.</summary>
		/// <returns>true if the HTML control literal string is to be decoded; otherwise, false.</returns>
		// Token: 0x060011B1 RID: 4529 RVA: 0x00008A69 File Offset: 0x00006C69
		public virtual bool HtmlDecodeLiterals()
		{
			return false;
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x00031010 File Offset: 0x0002F210
		private ControlBuilder CreatePropertyBuilder(string propName, TemplateParser parser, IDictionary atts)
		{
			int num;
			string text;
			if ((num = propName.IndexOf(':')) >= 0)
			{
				text = propName.Substring(num + 1);
			}
			else
			{
				text = propName;
			}
			PropertyInfo property = this.type.GetProperty(text, ControlBuilder.FlagsNoCase);
			if (property == null)
			{
				throw new HttpException(string.Format("Property {0} not found in type {1}", text, this.type));
			}
			Type propertyType = property.PropertyType;
			ControlBuilder controlBuilder;
			if (typeof(ICollection).IsAssignableFrom(propertyType))
			{
				controlBuilder = new CollectionBuilder();
			}
			else if (typeof(ITemplate).IsAssignableFrom(propertyType))
			{
				controlBuilder = new TemplateBuilder(property);
			}
			else
			{
				if (!(typeof(string) == propertyType))
				{
					controlBuilder = ControlBuilder.CreateBuilderFromType(parser, this.parentBuilder, propertyType, property.Name, null, atts, this.line, this.fileName);
					controlBuilder.isProperty = true;
					controlBuilder.isPropertyWritable = property.CanWrite;
					if (num >= 0)
					{
						controlBuilder.originalTagName = propName;
					}
					return controlBuilder;
				}
				controlBuilder = new StringPropertyBuilder(property.Name);
			}
			controlBuilder.Init(parser, this, null, property.Name, null, atts);
			controlBuilder.fileName = this.fileName;
			controlBuilder.line = this.line;
			controlBuilder.isProperty = true;
			controlBuilder.isPropertyWritable = property.CanWrite;
			if (num >= 0)
			{
				controlBuilder.originalTagName = propName;
			}
			return controlBuilder;
		}

		/// <summary>Initializes the <see cref="T:System.Web.UI.ControlBuilder" /> for use after it is instantiated. This method is called by the ASP.NET page framework.</summary>
		/// <param name="parser">The <see cref="T:System.Web.UI.TemplateParser" /> object responsible for parsing the control. </param>
		/// <param name="parentBuilder">The <see cref="T:System.Web.UI.ControlBuilder" /> object responsible for building the parent control. </param>
		/// <param name="type">The <see cref="T:System.Type" /> assigned to the control that the builder will create. </param>
		/// <param name="tagName">The name of the tag to be built. This allows the builder to support multiple tag types. </param>
		/// <param name="id">The <see cref="P:System.Web.UI.ControlBuilder.ID" /> attribute assigned to the control. </param>
		/// <param name="attribs">The <see cref="T:System.Collections.IDictionary" /> object that holds all the specified tag attributes. </param>
		// Token: 0x060011B3 RID: 4531 RVA: 0x00031168 File Offset: 0x0002F368
		public virtual void Init(TemplateParser parser, ControlBuilder parentBuilder, Type type, string tagName, string id, IDictionary attribs)
		{
			this.parser = parser;
			if (parser != null)
			{
				this.Location = parser.Location;
			}
			this.parentBuilder = parentBuilder;
			this.type = type;
			this.tagName = tagName;
			this.id = id;
			this.attribs = attribs;
			if (type == null)
			{
				return;
			}
			if (this is TemplateBuilder)
			{
				return;
			}
			object[] customAttributes = type.GetCustomAttributes(typeof(ParseChildrenAttribute), true);
			if (!typeof(IParserAccessor).IsAssignableFrom(type) && customAttributes.Length == 0)
			{
				this.isIParserAccessor = false;
				this.childrenAsProperties = true;
				return;
			}
			if (customAttributes.Length != 0)
			{
				ParseChildrenAttribute parseChildrenAttribute = (ParseChildrenAttribute)customAttributes[0];
				this.childrenAsProperties = parseChildrenAttribute.ChildrenAsProperties;
				if (this.childrenAsProperties && parseChildrenAttribute.DefaultProperty.Length != 0)
				{
					this.defaultPropertyBuilder = this.CreatePropertyBuilder(parseChildrenAttribute.DefaultProperty, parser, null);
				}
			}
		}

		/// <summary>Determines if the control builder needs to get its inner text. If so, the <see cref="M:System.Web.UI.ControlBuilder.SetTagInnerText(System.String)" /> method must be called. This method is called by the ASP.NET page framework.</summary>
		/// <returns>true if the control builder needs to get its inner text. The default is false.</returns>
		// Token: 0x060011B4 RID: 4532 RVA: 0x00008A69 File Offset: 0x00006C69
		public virtual bool NeedsTagInnerText()
		{
			return false;
		}

		/// <summary>Notifies the <see cref="T:System.Web.UI.ControlBuilder" /> that it is being added to a parent control builder.</summary>
		/// <param name="parentBuilder">The <see cref="T:System.Web.UI.ControlBuilder" /> object to which the current builder is added. </param>
		// Token: 0x060011B5 RID: 4533 RVA: 0x00031240 File Offset: 0x0002F440
		public virtual void OnAppendToParentBuilder(ControlBuilder parentBuilder)
		{
			if (this.defaultPropertyBuilder == null)
			{
				return;
			}
			ControlBuilder controlBuilder = this.defaultPropertyBuilder;
			this.defaultPropertyBuilder = null;
			this.AppendSubBuilder(controlBuilder);
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x0003126B File Offset: 0x0002F46B
		internal void SetTagName(string name)
		{
			this.tagName = name;
		}

		/// <summary>Provides the <see cref="T:System.Web.UI.ControlBuilder" /> with the inner text of the control tag.</summary>
		/// <param name="text">The text to be provided. </param>
		// Token: 0x060011B7 RID: 4535 RVA: 0x0000393A File Offset: 0x00001B3A
		public virtual void SetTagInnerText(string text)
		{
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x00031274 File Offset: 0x0002F474
		internal string GetNextID(string proposedID)
		{
			if (proposedID != null && proposedID.Trim().Length != 0)
			{
				return proposedID;
			}
			return "_bctrl_" + ControlBuilder.nextID++;
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x000312A4 File Offset: 0x0002F4A4
		internal string GetNextLocalVariableName(string baseName)
		{
			this.localVariableCount++;
			return baseName + this.localVariableCount.ToString();
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x000312C8 File Offset: 0x0002F4C8
		internal virtual ControlBuilder CreateSubBuilder(string tagid, IDictionary atts, Type childType, TemplateParser parser, ILocation location)
		{
			if (this.childrenAsProperties)
			{
				ControlBuilder controlBuilder;
				if (this.defaultPropertyBuilder == null)
				{
					controlBuilder = this.CreatePropertyBuilder(tagid, parser, atts);
				}
				else if (string.Compare(this.defaultPropertyBuilder.TagName, tagid, true, Helpers.InvariantCulture) == 0)
				{
					this.defaultPropertyBuilder = null;
					controlBuilder = this.CreatePropertyBuilder(tagid, parser, atts);
				}
				else
				{
					Type controlType = this.ControlType;
					MemberInfo[] array = ((controlType != null) ? controlType.GetMember(tagid, MemberTypes.Property, ControlBuilder.FlagsNoCase) : null);
					PropertyInfo propertyInfo = ((array != null && array.Length != 0) ? (array[0] as PropertyInfo) : null);
					if (propertyInfo != null && typeof(ITemplate).IsAssignableFrom(propertyInfo.PropertyType))
					{
						controlBuilder = this.CreatePropertyBuilder(tagid, parser, atts);
						this.defaultPropertyBuilder = null;
					}
					else
					{
						controlBuilder = this.defaultPropertyBuilder.CreateSubBuilder(tagid, atts, null, parser, location);
					}
				}
				return controlBuilder;
			}
			if (string.Compare(this.tagName, tagid, true, Helpers.InvariantCulture) == 0)
			{
				return null;
			}
			childType = this.GetChildControlType(tagid, atts);
			if (childType == null)
			{
				return null;
			}
			return ControlBuilder.CreateBuilderFromType(parser, this, childType, tagid, this.id, atts, location.BeginLine, location.Filename);
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x000313F4 File Offset: 0x0002F5F4
		internal virtual object CreateInstance()
		{
			object[] customAttributes = this.type.GetCustomAttributes(typeof(ConstructorNeedsTagAttribute), true);
			object[] array = null;
			if (customAttributes != null && customAttributes.Length != 0 && ((ConstructorNeedsTagAttribute)customAttributes[0]).NeedsTag)
			{
				array = new object[] { this.tagName };
			}
			return Activator.CreateInstance(this.type, array);
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x0003144C File Offset: 0x0002F64C
		internal virtual void CreateChildren(object parent)
		{
			if (this.children == null || this.children.Count == 0)
			{
				return;
			}
			IParserAccessor parserAccessor = parent as IParserAccessor;
			if (parserAccessor == null)
			{
				return;
			}
			foreach (object obj in this.children)
			{
				if (obj is string)
				{
					parserAccessor.AddParsedSubObject(new LiteralControl((string)obj));
				}
				else
				{
					parserAccessor.AddParsedSubObject(((ControlBuilder)obj).CreateInstance());
				}
			}
		}

		/// <summary>Builds a design-time instance of the control that is referred to by this <see cref="T:System.Web.UI.ControlBuilder" /> object.</summary>
		/// <returns>The resulting built control.</returns>
		// Token: 0x060011BD RID: 4541 RVA: 0x000314E8 File Offset: 0x0002F6E8
		[global::System.MonoTODO("unsure, lack documentation")]
		public virtual object BuildObject()
		{
			return this.CreateInstance();
		}

		/// <summary>Enables custom control builders to access the generated Code Document Object Model (CodeDom) and insert and modify code during the process of parsing and building controls.</summary>
		/// <param name="codeCompileUnit">The root container of a CodeDOM graph of the control that is being built.</param>
		/// <param name="baseType">The base type of the page or user control that contains the control that is being built.</param>
		/// <param name="derivedType">The derived type of the page or user control that contains the control that is being built.</param>
		/// <param name="buildMethod">The code that is used to build the control.</param>
		/// <param name="dataBindingMethod">The code that is used to build the data-binding method of the control.</param>
		// Token: 0x060011BE RID: 4542 RVA: 0x0000393A File Offset: 0x00001B3A
		public virtual void ProcessGeneratedCode(CodeCompileUnit codeCompileUnit, CodeTypeDeclaration baseType, CodeTypeDeclaration derivedType, CodeMemberMethod buildMethod, CodeMemberMethod dataBindingMethod)
		{
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x000314F0 File Offset: 0x0002F6F0
		internal void ResetState()
		{
			this.renderIndex = 0;
			this.haveParserVariable = false;
			if (this.Children != null)
			{
				foreach (object obj in this.Children)
				{
					ControlBuilder controlBuilder = obj as ControlBuilder;
					if (controlBuilder != null)
					{
						controlBuilder.ResetState();
					}
				}
			}
		}

		/// <summary>Gets the control builder that corresponds to the binding container for the control that this builder creates.</summary>
		/// <returns>The control builder that corresponds to the binding container for the control.</returns>
		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x060011C1 RID: 4545 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual ControlBuilder BindingContainerBuilder
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a collection of complex property entries.</summary>
		/// <returns>A collection of complex property entries.</returns>
		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x060011C2 RID: 4546 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public ICollection ComplexPropertyEntries
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets an <see cref="T:System.Web.UI.IFilterResolutionService" /> object that is used to manage device-filter related services when parsing and persisting controls in the designer.</summary>
		/// <returns>An <see cref="T:System.Web.UI.IFilterResolutionService" /> object that is used to manage device filter related services when parsing and persisting controls in the designer.</returns>
		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x060011C3 RID: 4547 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public IFilterResolutionService CurrentFilterResolutionService
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the type that will be used by code generation to declare the control.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the control used by code generation to declare the control.</returns>
		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x060011C4 RID: 4548 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual Type DeclareType
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a Boolean value indicating whether this <see cref="T:System.Web.UI.ControlBuilder" /> object is used to generate page themes.</summary>
		/// <returns>true to use this <see cref="T:System.Web.UI.ControlBuilder" /> to generate page themes; otherwise, false.</returns>
		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x060011C5 RID: 4549 RVA: 0x00031570 File Offset: 0x0002F770
		protected bool InPageTheme
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets the type set on the binding container.</summary>
		/// <returns>The type set on the binding container.</returns>
		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x060011C6 RID: 4550 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual string ItemType
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a Boolean value indicating whether the control that is created by this <see cref="T:System.Web.UI.ControlBuilder" /> object is localized.</summary>
		/// <returns>true to indicate that the control created by this <see cref="T:System.Web.UI.ControlBuilder" /> object is localized; otherwise, false.</returns>
		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x060011C7 RID: 4551 RVA: 0x0003158C File Offset: 0x0002F78C
		public bool Localize
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets the virtual path of a page to be built by this <see cref="T:System.Web.UI.ControlBuilder" /> instance.</summary>
		/// <returns>The virtual path of the page to be built.</returns>
		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x060011C8 RID: 4552 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string PageVirtualPath
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the service object for this <see cref="T:System.Web.UI.ControlBuilder" /> object.</summary>
		/// <returns>An <see cref="T:System.IServiceProvider" /> that represents the service object for this <see cref="T:System.Web.UI.ControlBuilder" />.</returns>
		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x060011C9 RID: 4553 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public IServiceProvider ServiceProvider
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a list of child <see cref="T:System.Web.UI.ControlBuilder" /> objects for this <see cref="T:System.Web.UI.ControlBuilder" /> object.</summary>
		/// <returns>A list of child <see cref="T:System.Web.UI.ControlBuilder" /> objects.</returns>
		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x060011CA RID: 4554 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public ArrayList SubBuilders
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a collection of template property entries.</summary>
		/// <returns>A collection of template property entries.</returns>
		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x060011CB RID: 4555 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public ICollection TemplatePropertyEntries
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets an <see cref="T:System.Web.UI.IThemeResolutionService" /> object that is used in design time to manage control themes and skins.</summary>
		/// <returns>An <see cref="T:System.Web.UI.IThemeResolutionService" /> object that is used in design time to manage control themes and skins.</returns>
		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x060011CC RID: 4556 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public IThemeResolutionService ThemeResolutionService
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Creates the <see cref="T:System.Web.UI.ObjectPersistData" /> object for this <see cref="T:System.Web.UI.ControlBuilder" /> object.</summary>
		/// <returns>The <see cref="T:System.Web.UI.ObjectPersistData" /> for this <see cref="T:System.Web.UI.ControlBuilder" />.</returns>
		// Token: 0x060011CD RID: 4557 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public ObjectPersistData GetObjectPersistData()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Retrieves the resource key for this <see cref="T:System.Web.UI.ControlBuilder" /> object.</summary>
		/// <returns>The resource key for this <see cref="T:System.Web.UI.ControlBuilder" />.</returns>
		// Token: 0x060011CE RID: 4558 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string GetResourceKey()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Sets the resource key for this <see cref="T:System.Web.UI.ControlBuilder" /> object.</summary>
		/// <param name="resourceKey">The resource key for this <see cref="T:System.Web.UI.ControlBuilder" />.</param>
		// Token: 0x060011CF RID: 4559 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void SetResourceKey(string resourceKey)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Sets the service object for this <see cref="T:System.Web.UI.ControlBuilder" /> object.</summary>
		/// <param name="serviceProvider">An <see cref="T:System.IServiceProvider" /> that represents the service object for the <see cref="T:System.Web.UI.ControlBuilder" />.</param>
		// Token: 0x060011D0 RID: 4560 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public void SetServiceProvider(IServiceProvider serviceProvider)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040013E0 RID: 5088
		internal static readonly BindingFlags FlagsNoCase = BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;

		// Token: 0x040013E1 RID: 5089
		private ControlBuilder myNamingContainer;

		// Token: 0x040013E2 RID: 5090
		private TemplateParser parser;

		// Token: 0x040013E3 RID: 5091
		private Type parserType;

		// Token: 0x040013E4 RID: 5092
		private ControlBuilder parentBuilder;

		// Token: 0x040013E5 RID: 5093
		private Type type;

		// Token: 0x040013E6 RID: 5094
		private string tagName;

		// Token: 0x040013E7 RID: 5095
		private string originalTagName;

		// Token: 0x040013E8 RID: 5096
		private string id;

		// Token: 0x040013E9 RID: 5097
		private IDictionary attribs;

		// Token: 0x040013EA RID: 5098
		private int line;

		// Token: 0x040013EB RID: 5099
		private string fileName;

		// Token: 0x040013EC RID: 5100
		private bool childrenAsProperties;

		// Token: 0x040013ED RID: 5101
		private bool isIParserAccessor = true;

		// Token: 0x040013EE RID: 5102
		private bool hasAspCode;

		// Token: 0x040013EF RID: 5103
		private ControlBuilder defaultPropertyBuilder;

		// Token: 0x040013F0 RID: 5104
		private ArrayList children;

		// Token: 0x040013F1 RID: 5105
		private ArrayList templateChildren;

		// Token: 0x040013F2 RID: 5106
		private static int nextID;

		// Token: 0x040013F3 RID: 5107
		private bool haveParserVariable;

		// Token: 0x040013F4 RID: 5108
		private CodeMemberMethod method;

		// Token: 0x040013F5 RID: 5109
		private CodeStatementCollection methodStatements;

		// Token: 0x040013F6 RID: 5110
		private CodeMemberMethod renderMethod;

		// Token: 0x040013F7 RID: 5111
		private int renderIndex;

		// Token: 0x040013F8 RID: 5112
		private bool isProperty;

		// Token: 0x040013F9 RID: 5113
		private bool isPropertyWritable;

		// Token: 0x040013FA RID: 5114
		private ILocation location;

		// Token: 0x040013FB RID: 5115
		private ArrayList otherTags;

		// Token: 0x040013FC RID: 5116
		private int localVariableCount;

		// Token: 0x040013FD RID: 5117
		private bool? isTemplate;

		/// <summary>Represents the "__designer" literal string.</summary>
		// Token: 0x040013FF RID: 5119
		public static readonly string DesignerFilter;
	}
}
