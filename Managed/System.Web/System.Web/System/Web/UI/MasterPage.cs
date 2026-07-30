using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Compilation;
using System.Web.Hosting;

namespace System.Web.UI
{
	/// <summary>Acts as a template and merging container for pages that are composed only of <see cref="T:System.Web.UI.WebControls.Content" /> controls and their respective child controls.</summary>
	// Token: 0x020001E8 RID: 488
	[ControlBuilder(typeof(MasterPageControlBuilder))]
	[ParseChildren(false)]
	public class MasterPage : UserControl
	{
		/// <summary>Adds a <see cref="T:System.Web.UI.WebControls.Content" /> control to the <see cref="P:System.Web.UI.MasterPage.ContentTemplates" /> dictionary.</summary>
		/// <param name="templateName">A unique name for the <see cref="T:System.Web.UI.WebControls.Content" />.</param>
		/// <param name="template">The <see cref="T:System.Web.UI.WebControls.Content" />.</param>
		/// <exception cref="T:System.Web.HttpException">A <see cref="T:System.Web.UI.WebControls.Content" /> control with the same name already exists in the <see cref="P:System.Web.UI.MasterPage.ContentTemplates" /> dictionary.</exception>
		// Token: 0x060013B5 RID: 5045 RVA: 0x000355DE File Offset: 0x000337DE
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected internal void AddContentTemplate(string templateName, ITemplate template)
		{
			if (this.definedContentTemplates.ContainsKey(templateName))
			{
				throw new HttpException("Multiple contents applied to " + templateName);
			}
			this.definedContentTemplates[templateName] = template;
		}

		/// <summary>Gets a list of <see cref="T:System.Web.UI.WebControls.ContentPlaceHolder" /> controls that the master page uses to define different content regions.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> of <see cref="T:System.Web.UI.WebControls.ContentPlaceHolder" /> controls that the master page uses as placeholders for <see cref="T:System.Web.UI.WebControls.Content" /> controls found in content pages.</returns>
		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x060013B6 RID: 5046 RVA: 0x0003560C File Offset: 0x0003380C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected internal IList ContentPlaceHolders
		{
			get
			{
				if (this.placeholders == null)
				{
					this.placeholders = new List<string>();
				}
				return this.placeholders;
			}
		}

		/// <summary>Gets a list of content controls that are associated with the master page. </summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> of content controls associated with the master page. </returns>
		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x060013B7 RID: 5047 RVA: 0x00035627 File Offset: 0x00033827
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected internal IDictionary ContentTemplates
		{
			get
			{
				return this.templates;
			}
		}

		/// <summary>Gets or sets the name of the master page that contains the current content.</summary>
		/// <returns>The name of the master page that is the parent of the current master page; otherwise, null, if the current master page has no parent.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.UI.MasterPage.MasterPageFile" /> property can only be set in or before the <see cref="E:System.Web.UI.Page.PreInit" /> event.</exception>
		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x060013B8 RID: 5048 RVA: 0x0003562F File Offset: 0x0003382F
		// (set) Token: 0x060013B9 RID: 5049 RVA: 0x00035637 File Offset: 0x00033837
		[DefaultValue("")]
		public string MasterPageFile
		{
			get
			{
				return this.parentMasterPageFile;
			}
			set
			{
				this.parentMasterPageFile = value;
				this.parentMasterPage = null;
			}
		}

		/// <summary>Gets the parent master page of the current master in nested master pages scenarios.</summary>
		/// <returns>The master page that is the parent of the current master page; otherwise, null, if the current master page has no parent.</returns>
		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x060013BA RID: 5050 RVA: 0x00035647 File Offset: 0x00033847
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public MasterPage Master
		{
			get
			{
				if (this.parentMasterPage == null && this.parentMasterPageFile != null)
				{
					this.parentMasterPage = MasterPage.CreateMasterPage(this, this.Context, this.parentMasterPageFile, this.definedContentTemplates);
				}
				return this.parentMasterPage;
			}
		}

		/// <summary>Provides a method to set the current template control to a page that owns the master page.</summary>
		/// <param name="contentPlaceHolder">The control that represents the container of the content.</param>
		/// <param name="template">The <see cref="T:System.Web.UI.WebControls.Content" /> instance to use.</param>
		// Token: 0x060013BB RID: 5051 RVA: 0x0003567D File Offset: 0x0003387D
		public void InstantiateInContentPlaceHolder(Control contentPlaceHolder, ITemplate template)
		{
			if (contentPlaceHolder == null || template == null)
			{
				throw new NullReferenceException();
			}
			if (contentPlaceHolder != null && template != null)
			{
				template.InstantiateIn(contentPlaceHolder);
			}
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x00035698 File Offset: 0x00033898
		internal static MasterPage CreateMasterPage(TemplateControl owner, HttpContext context, string masterPageFile, IDictionary contentTemplateCollection)
		{
			HttpRequest request = context.Request;
			if (request != null)
			{
				masterPageFile = HostingEnvironment.VirtualPathProvider.CombineVirtualPaths(request.CurrentExecutionFilePath, masterPageFile);
			}
			MasterPage masterPage = BuildManager.CreateInstanceFromVirtualPath(masterPageFile, typeof(MasterPage)) as MasterPage;
			if (masterPage == null)
			{
				throw new HttpException("Failed to create MasterPage instance for '" + masterPageFile + "'.");
			}
			if (contentTemplateCollection != null)
			{
				foreach (object obj in contentTemplateCollection.Keys)
				{
					string text = (string)obj;
					if (masterPage.ContentTemplates[text] == null)
					{
						masterPage.ContentTemplates[text] = contentTemplateCollection[text];
					}
				}
			}
			masterPage.Page = owner.Page;
			masterPage.InitializeAsUserControlInternal();
			List<string> list = masterPage.placeholders;
			if (contentTemplateCollection != null && list != null && list.Count > 0)
			{
				foreach (object obj2 in contentTemplateCollection.Keys)
				{
					string text2 = (string)obj2;
					if (!list.Contains(text2.ToLowerInvariant()))
					{
						throw new HttpException(string.Format("Cannot find ContentPlaceHolder '{0}' in the master page '{1}'", text2, masterPageFile));
					}
				}
			}
			return masterPage;
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x000357F4 File Offset: 0x000339F4
		internal static void ApplyMasterPageRecursive(string currentFilePath, VirtualPathProvider vpp, MasterPage master, Dictionary<string, bool> appliedMasterPageFiles)
		{
			string text = master.MasterPageFile;
			if (!string.IsNullOrEmpty(text))
			{
				text = vpp.CombineVirtualPaths(currentFilePath, text);
				if (appliedMasterPageFiles.ContainsKey(text))
				{
					throw new HttpException("circular dependency in master page files detected");
				}
				MasterPage master2 = master.Master;
				if (master2 != null)
				{
					master.Controls.Clear();
					master.Controls.Add(master2);
					appliedMasterPageFiles.Add(text, true);
					MasterPage.ApplyMasterPageRecursive(currentFilePath, vpp, master2, appliedMasterPageFiles);
				}
			}
		}

		// Token: 0x04001479 RID: 5241
		private Hashtable definedContentTemplates = new Hashtable();

		// Token: 0x0400147A RID: 5242
		private Hashtable templates = new Hashtable();

		// Token: 0x0400147B RID: 5243
		private List<string> placeholders;

		// Token: 0x0400147C RID: 5244
		private string parentMasterPageFile;

		// Token: 0x0400147D RID: 5245
		private MasterPage parentMasterPage;
	}
}
