using System;
using System.ComponentModel;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Inserted into a Web page as a placeholder when the attempt to load or create a new instance of a dynamic <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control has failed. </summary>
	// Token: 0x020006D6 RID: 1750
	[ToolboxItem(false)]
	public class ErrorWebPart : ProxyWebPart, ITrackingPersonalizable
	{
		/// <summary>Initializes a new instance of the control.</summary>
		/// <param name="originalID">A string that is the control ID (not the unique ID) of the failing control. If a <see cref="T:System.Web.UI.WebControls.WebParts.GenericWebPart" /> control is involved in the failure, the ID is the ID of its child server control. </param>
		/// <param name="originalTypeName">A string that is the name of the <see cref="T:System.Type" /> of the failed control. If a <see cref="T:System.Web.UI.WebControls.WebParts.GenericWebPart" /> control is involved in the failure, the type name is the type of its child server control. </param>
		/// <param name="originalPath">A string that contains the path to a user control, if a <see cref="T:System.Web.UI.WebControls.WebParts.GenericWebPart" /> control that contains a child user control is involved in the failure. </param>
		/// <param name="genericWebPartID">A string that returns the ID of a <see cref="T:System.Web.UI.WebControls.WebParts.GenericWebPart" /> control, if that type of control was involved in the failure to load or create a control. This is needed for controls that do not inherit from the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> base class.</param>
		// Token: 0x06004A38 RID: 19000 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public ErrorWebPart(string originalID, string originalTypeName, string originalPath, string genericWebPartID)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets the error message that is displayed in a Web page in place of a control that could not be successfully added to the page.</summary>
		/// <returns>A string that contains the text of the error message. The default value is a culture-specific error message.</returns>
		// Token: 0x170016E8 RID: 5864
		// (get) Token: 0x06004A39 RID: 19001 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004A3A RID: 19002 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public string ErrorMessage
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

		// Token: 0x06004A3B RID: 19003 RVA: 0x000CA6E4 File Offset: 0x000C88E4
		bool ITrackingPersonalizable.get_TracksChanges()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Sets several important properties on the <see cref="T:System.Web.UI.WebControls.WebParts.ErrorWebPart" /> control prior to rendering, to prevent users from being able to personalize the control.</summary>
		// Token: 0x06004A3C RID: 19004 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void EndLoadPersonalization()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>This method is added only to implement the <see cref="T:System.Web.UI.WebControls.WebParts.ITrackingPersonalizable" /> interface.</summary>
		// Token: 0x06004A3D RID: 19005 RVA: 0x0000B3E4 File Offset: 0x000095E4
		void ITrackingPersonalizable.BeginLoad()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>This method is added only to implement the <see cref="T:System.Web.UI.WebControls.WebParts.ITrackingPersonalizable" /> interface.</summary>
		// Token: 0x06004A3E RID: 19006 RVA: 0x0000B3E4 File Offset: 0x000095E4
		void ITrackingPersonalizable.BeginSave()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Implements the <see cref="T:System.Web.UI.WebControls.WebParts.ITrackingPersonalizable" /> interface and calls the <see cref="M:System.Web.UI.WebControls.WebParts.ErrorWebPart.EndLoadPersonalization" /> method.</summary>
		// Token: 0x06004A3F RID: 19007 RVA: 0x0000B3E4 File Offset: 0x000095E4
		void ITrackingPersonalizable.EndLoad()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>This method is added only to implement the <see cref="T:System.Web.UI.WebControls.WebParts.ITrackingPersonalizable" /> interface.</summary>
		// Token: 0x06004A40 RID: 19008 RVA: 0x0000B3E4 File Offset: 0x000095E4
		void ITrackingPersonalizable.EndSave()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
