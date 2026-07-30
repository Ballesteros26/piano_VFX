using System;
using System.Collections;
using System.Security.Permissions;
using System.Web.Compilation;
using System.Web.UI.HtmlControls;
using Unity;

namespace System.Web.UI
{
	/// <summary>Supports the page parser in defining the behavior for how content is parsed.</summary>
	// Token: 0x02000222 RID: 546
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class RootBuilder : TemplateBuilder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.RootBuilder" /> class. </summary>
		// Token: 0x06001659 RID: 5721 RVA: 0x0003BC81 File Offset: 0x00039E81
		public RootBuilder()
		{
			this.foundry = new AspComponentFoundry();
			base.Line = 1;
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x0003BC9C File Offset: 0x00039E9C
		static RootBuilder()
		{
			RootBuilder.htmlControls.Add("A", typeof(HtmlAnchor));
			RootBuilder.htmlControls.Add("BUTTON", typeof(HtmlButton));
			RootBuilder.htmlControls.Add("FORM", typeof(HtmlForm));
			RootBuilder.htmlControls.Add("HEAD", typeof(HtmlHead));
			RootBuilder.htmlControls.Add("IMG", typeof(HtmlImage));
			RootBuilder.htmlControls.Add("INPUT", "INPUT");
			RootBuilder.htmlControls.Add("SELECT", typeof(HtmlSelect));
			RootBuilder.htmlControls.Add("TABLE", typeof(HtmlTable));
			RootBuilder.htmlControls.Add("TD", typeof(HtmlTableCell));
			RootBuilder.htmlControls.Add("TH", typeof(HtmlTableCell));
			RootBuilder.htmlControls.Add("TR", typeof(HtmlTableRow));
			RootBuilder.htmlControls.Add("TEXTAREA", typeof(HtmlTextArea));
			RootBuilder.htmlInputControls = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
			RootBuilder.htmlInputControls.Add("BUTTON", typeof(HtmlInputButton));
			RootBuilder.htmlInputControls.Add("SUBMIT", typeof(HtmlInputSubmit));
			RootBuilder.htmlInputControls.Add("RESET", typeof(HtmlInputReset));
			RootBuilder.htmlInputControls.Add("CHECKBOX", typeof(HtmlInputCheckBox));
			RootBuilder.htmlInputControls.Add("FILE", typeof(HtmlInputFile));
			RootBuilder.htmlInputControls.Add("HIDDEN", typeof(HtmlInputHidden));
			RootBuilder.htmlInputControls.Add("IMAGE", typeof(HtmlInputImage));
			RootBuilder.htmlInputControls.Add("RADIO", typeof(HtmlInputRadioButton));
			RootBuilder.htmlInputControls.Add("TEXT", typeof(HtmlInputText));
			RootBuilder.htmlInputControls.Add("PASSWORD", typeof(HtmlInputPassword));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.RootBuilder" /> class with the specified template parser.</summary>
		/// <param name="parser">The object to use to parse files.</param>
		// Token: 0x0600165B RID: 5723 RVA: 0x0003BEE8 File Offset: 0x0003A0E8
		public RootBuilder(TemplateParser parser)
		{
			this.foundry = new AspComponentFoundry();
			base.Line = 1;
			if (parser != null)
			{
				base.FileName = parser.InputFile;
			}
			this.Init(parser, null, null, null, null, null);
		}

		/// <summary>Returns the control type of any parsed child controls.</summary>
		/// <returns>The type of the child control.</returns>
		/// <param name="tagName">The tag name of the child control.</param>
		/// <param name="attribs">The <see cref="T:System.Collections.IDictionary" /> object that holds all the specified tag attributes.</param>
		// Token: 0x0600165C RID: 5724 RVA: 0x0003BF20 File Offset: 0x0003A120
		public override Type GetChildControlType(string tagName, IDictionary attribs)
		{
			if (tagName == null)
			{
				throw new ArgumentNullException("tagName");
			}
			AspComponent component = this.foundry.GetComponent(tagName);
			if (component != null)
			{
				if (!string.IsNullOrEmpty(component.Source))
				{
					TemplateParser parser = base.Parser;
					if (component.FromConfig)
					{
						string baseVirtualDir = parser.BaseVirtualDir;
						VirtualPath virtualPath = new VirtualPath(component.Source);
						if (baseVirtualDir == virtualPath.Directory)
						{
							throw new ParseException(parser.Location, string.Format("The page '{0}' cannot use the user control '{1}', because it is registered in web.config and lives in the same directory as the page.", parser.VirtualPath, virtualPath.Absolute));
						}
						base.Parser.AddDependency(component.Source);
					}
				}
				return component.Type;
			}
			if (component != null && component.Prefix != string.Empty)
			{
				throw new Exception("Unknown server tag '" + tagName + "'");
			}
			return RootBuilder.LookupHtmlControls(tagName, attribs);
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x0003BFF4 File Offset: 0x0003A1F4
		private static Type LookupHtmlControls(string tagName, IDictionary attribs)
		{
			object obj = RootBuilder.htmlControls[tagName];
			if (!(obj is string))
			{
				if (obj == null)
				{
					obj = typeof(HtmlGenericControl);
				}
				return (Type)obj;
			}
			if (attribs == null)
			{
				throw new HttpException("Unable to map input type control to a Type.");
			}
			string text = attribs["TYPE"] as string;
			if (text == null)
			{
				text = "TEXT";
			}
			Type type = RootBuilder.htmlInputControls[text] as Type;
			if (type == null)
			{
				throw new HttpException("Unable to map input type control to a Type.");
			}
			return type;
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x0600165E RID: 5726 RVA: 0x0003C076 File Offset: 0x0003A276
		// (set) Token: 0x0600165F RID: 5727 RVA: 0x0003C07E File Offset: 0x0003A27E
		internal AspComponentFoundry Foundry
		{
			get
			{
				return this.foundry;
			}
			set
			{
				if (value != null)
				{
					this.foundry = value;
				}
			}
		}

		/// <summary>Gets a collection of the objects to persist that were built by the root builder.</summary>
		/// <returns>A <see cref="T:System.Collections.Hashtable" /> object that contains the objects that were built by the root builder.</returns>
		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06001660 RID: 5728 RVA: 0x0003C08A File Offset: 0x0003A28A
		public IDictionary BuiltObjects
		{
			get
			{
				if (this.built_objects == null)
				{
					this.built_objects = new Hashtable();
				}
				return this.built_objects;
			}
		}

		/// <summary>Provides a way to modify the <see cref="T:System.CodeDom.CodeCompileUnit" /> object after code generation is finished.</summary>
		// Token: 0x06001661 RID: 5729 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected internal virtual void OnCodeGenerationComplete()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001566 RID: 5478
		private Hashtable built_objects;

		// Token: 0x04001567 RID: 5479
		private static Hashtable htmlControls = new Hashtable(StringComparer.InvariantCultureIgnoreCase);

		// Token: 0x04001568 RID: 5480
		private static Hashtable htmlInputControls;

		// Token: 0x04001569 RID: 5481
		private AspComponentFoundry foundry;
	}
}
