using System;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Serves as the base class for all zone controls that act as containers for <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls.</summary>
	// Token: 0x020006DD RID: 1757
	public abstract class EditorZoneBase : ToolZone
	{
		/// <summary>Initializes the class for use by an inherited class instance. This constructor can only be called by an inherited class.</summary>
		// Token: 0x06004A7B RID: 19067 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected EditorZoneBase()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> object that enables end users to apply editing changes to a control in edit mode. </summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> that enables end users to apply changes to a control.</returns>
		// Token: 0x170016FE RID: 5886
		// (get) Token: 0x06004A7C RID: 19068 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual WebPartVerb ApplyVerb
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> object that enables end users to cancel editing changes to a control in edit mode. </summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> that enables end users to cancel editing changes to a control.</returns>
		// Token: 0x170016FF RID: 5887
		// (get) Token: 0x06004A7D RID: 19069 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual WebPartVerb CancelVerb
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x17001700 RID: 5888
		// (get) Token: 0x06004A7E RID: 19070 RVA: 0x000CA850 File Offset: 0x000C8A50
		protected override bool Display
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets a reference to the instance of the <see cref="T:System.Web.UI.WebControls.WebParts.EditorPartChrome" /> class associated with the <see cref="T:System.Web.UI.WebControls.WebParts.EditorZoneBase" /> zone.</summary>
		/// <returns>An <see cref="T:System.Web.UI.WebControls.WebParts.EditorPartChrome" /> that contains style characteristics for the chrome elements of <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls in a zone.</returns>
		// Token: 0x17001701 RID: 5889
		// (get) Token: 0x06004A7F RID: 19071 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public EditorPartChrome EditorPartChrome
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a collection of all the <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls contained in an <see cref="T:System.Web.UI.WebControls.WebParts.EditorZoneBase" /> zone. </summary>
		/// <returns>An <see cref="T:System.Web.UI.WebControls.WebParts.EditorPartCollection" /> that contains all the individual <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls in a zone. </returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> being added to the collection does not have a value assigned to its ID property.</exception>
		// Token: 0x17001702 RID: 5890
		// (get) Token: 0x06004A80 RID: 19072 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public EditorPartCollection EditorParts
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x17001703 RID: 5891
		// (get) Token: 0x06004A81 RID: 19073 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004A82 RID: 19074 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public override string EmptyZoneText
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the text of a zone-level error message to display at the top of the editing user interface (UI).</summary>
		/// <returns>A localized string that contains the error message. </returns>
		// Token: 0x17001704 RID: 5892
		// (get) Token: 0x06004A83 RID: 19075 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004A84 RID: 19076 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual string ErrorText
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x17001705 RID: 5893
		// (get) Token: 0x06004A85 RID: 19077 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004A86 RID: 19078 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public override string HeaderText
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x17001706 RID: 5894
		// (get) Token: 0x06004A87 RID: 19079 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004A88 RID: 19080 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public override string InstructionText
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> object that applies editing changes to a control in edit mode, and hides the editing user interface (UI). </summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> that applies editing changes to a control and hides the editing UI.</returns>
		// Token: 0x17001707 RID: 5895
		// (get) Token: 0x06004A89 RID: 19081 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public virtual WebPartVerb OKVerb
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control that is currently being edited. </summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> that is currently in edit mode.</returns>
		// Token: 0x17001708 RID: 5896
		// (get) Token: 0x06004A8A RID: 19082 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected WebPart WebPartToEdit
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Called when a user clicks a close verb in the header of a zone, this method ends the process of editing <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls, and returns a Web Parts page's display mode to browse mode.</summary>
		// Token: 0x06004A8B RID: 19083 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected override void Close()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a reference to a new <see cref="T:System.Web.UI.WebControls.WebParts.EditorPartChrome" /> object used to render the peripheral user interface (UI) elements around an <see cref="T:System.Web.UI.WebControls.WebParts.EditorZoneBase" /> zone.</summary>
		/// <returns>An <see cref="T:System.Web.UI.WebControls.WebParts.EditorPartChrome" /> that renders the peripheral UI elements for the zone.</returns>
		// Token: 0x06004A8C RID: 19084 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected virtual EditorPartChrome CreateEditorPartChrome()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Provides a base method declaration that derived zones can override to handle the details of creating the <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls contained in a zone.</summary>
		/// <returns>An <see cref="T:System.Web.UI.WebControls.WebParts.EditorPartCollection" /> that contains the collection of <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls associated with the zone. </returns>
		// Token: 0x06004A8D RID: 19085
		protected abstract EditorPartCollection CreateEditorParts();

		/// <summary>Sets the collection of <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls associated with an <see cref="T:System.Web.UI.WebControls.WebParts.EditorZoneBase" /> zone to null, which results in the <see cref="M:System.Web.UI.WebControls.WebParts.EditorZoneBase.CreateEditorParts" /> method being called to recreate the collection.</summary>
		// Token: 0x06004A8E RID: 19086 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected void InvalidateEditorParts()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.DisplayModeChanged" /> event and destroys all <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> and child controls in the zone in preparation for a Web page to enter or exit the edit display mode. </summary>
		/// <param name="sender">An <see cref="T:System.Object" /> that raises the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.DisplayModeChanged" /> event. </param>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDisplayModeEventArgs" /> that contains the event data. </param>
		// Token: 0x06004A8F RID: 19087 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected override void OnDisplayModeChanged(object sender, WebPartDisplayModeEventArgs e)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.SelectedWebPartChanged" /> event and sets the <see cref="P:System.Web.UI.WebControls.WebParts.EditorZoneBase.EditorParts" /> collection to null in the zone in preparation for a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control to enter or exit edit mode. </summary>
		/// <param name="sender">An <see cref="T:System.Object" /> that raises the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.SelectedWebPartChanged" /> event. </param>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDisplayModeEventArgs" /> that contains the event data. </param>
		// Token: 0x06004A90 RID: 19088 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected override void OnSelectedWebPartChanged(object sender, WebPartEventArgs e)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Carries out the actions associated with one of the zone verbs, or raises an event that posts back to the server.</summary>
		/// <param name="eventArgument">A <see cref="T:System.String" /> that contains the argument for the event. </param>
		// Token: 0x06004A91 RID: 19089 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected override void RaisePostBackEvent(string eventArgument)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Overrides the base method to render the body area of a zone derived from the <see cref="T:System.Web.UI.WebControls.WebParts.EditorZoneBase" /> class.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the zone's body content. </param>
		// Token: 0x06004A92 RID: 19090 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected override void RenderBody(HtmlTextWriter writer)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Renders the verbs that apply at the zone level.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the zone's body content. </param>
		// Token: 0x06004A93 RID: 19091 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected override void RenderVerbs(HtmlTextWriter writer)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
