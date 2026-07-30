using System;
using System.ComponentModel;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Unity;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Provides a reusable <see cref="T:System.Windows.Forms.UserControl" /> object for editing <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> objects.</summary>
	// Token: 0x020001A6 RID: 422
	public class ParameterEditorUserControl : UserControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.ParameterEditorUserControl" /> class using the specified <see cref="T:System.IServiceProvider" />.</summary>
		/// <param name="serviceProvider">An <see cref="T:System.IServiceProvider" /> interface to the current design host, such as Visual Studio 2005.</param>
		// Token: 0x06000B80 RID: 2944 RVA: 0x00009519 File Offset: 0x00007719
		public ParameterEditorUserControl(IServiceProvider serviceProvider)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns a value indicating whether all the parameters in the editor are configured.</summary>
		/// <returns>true if all parameters in the editor are fully configured; otherwise false.</returns>
		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000B81 RID: 2945 RVA: 0x00016778 File Offset: 0x00014978
		public bool ParametersConfigured
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Provides metadata for the <see cref="T:System.Web.UI.Design.WebControls.ParameterEditorUserControl" /> class. </summary>
		/// <returns>A type descriptor provider object.</returns>
		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000B82 RID: 2946 RVA: 0x0000970B File Offset: 0x0000790B
		public TypeDescriptionProvider TypeDescriptionProvider
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Occurs when any parameter or one of the parameter's properties is changed.</summary>
		// Token: 0x1400003E RID: 62
		// (add) Token: 0x06000B83 RID: 2947 RVA: 0x00009519 File Offset: 0x00007719
		// (remove) Token: 0x06000B84 RID: 2948 RVA: 0x00009519 File Offset: 0x00007719
		public event EventHandler ParametersChanged
		{
			add
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
			remove
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Adds an array of existing parameters to the editor.</summary>
		/// <param name="parameters">A <see cref="T:System.Web.UI.WebControls.Parameter" /> array of existing parameters to add to the editor.</param>
		// Token: 0x06000B85 RID: 2949 RVA: 0x00009519 File Offset: 0x00007719
		public void AddParameters(Parameter[] parameters)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes all parameters from the editor.</summary>
		// Token: 0x06000B86 RID: 2950 RVA: 0x00009519 File Offset: 0x00007719
		public void ClearParameters()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets all parameters from the editor.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Parameter" /> array of all parameters from the editor.</returns>
		// Token: 0x06000B87 RID: 2951 RVA: 0x0000970B File Offset: 0x0000790B
		public Parameter[] GetParameters()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Design.WebControls.ParameterEditorUserControl.ParametersChanged" /> event when the state of a parameter in the editor changes.</summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
		// Token: 0x06000B88 RID: 2952 RVA: 0x00009519 File Offset: 0x00007719
		protected virtual void OnParametersChanged(object sender, EventArgs e)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Controls whether additions and deletions can be made to the values in the editor.</summary>
		/// <param name="allowChanges">A <see cref="T:System.Boolean" /> that indicates whether additions and deletions can be made to the <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> being edited.</param>
		// Token: 0x06000B89 RID: 2953 RVA: 0x00009519 File Offset: 0x00007719
		public void SetAllowCollectionChanges(bool allowChanges)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
