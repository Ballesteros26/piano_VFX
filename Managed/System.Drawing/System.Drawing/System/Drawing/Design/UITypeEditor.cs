using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Security.Permissions;

namespace System.Drawing.Design
{
	/// <summary>Provides a base class that can be used to design value editors that can provide a user interface (UI) for representing and editing the values of objects of the supported data types.</summary>
	// Token: 0x0200012A RID: 298
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public class UITypeEditor
	{
		// Token: 0x06000D8F RID: 3471 RVA: 0x0001D9E4 File Offset: 0x0001BBE4
		static UITypeEditor()
		{
			Hashtable hashtable = new Hashtable();
			hashtable[typeof(DateTime)] = "System.ComponentModel.Design.DateTimeEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
			hashtable[typeof(Array)] = "System.ComponentModel.Design.ArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
			hashtable[typeof(IList)] = "System.ComponentModel.Design.CollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
			hashtable[typeof(ICollection)] = "System.ComponentModel.Design.CollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
			hashtable[typeof(byte[])] = "System.ComponentModel.Design.BinaryEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
			hashtable[typeof(Stream)] = "System.ComponentModel.Design.BinaryEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
			hashtable[typeof(string[])] = "System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
			hashtable[typeof(Collection<string>)] = "System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
			TypeDescriptor.AddEditorTable(typeof(UITypeEditor), hashtable);
		}

		/// <summary>Gets a value indicating whether drop-down editors should be resizable by the user.</summary>
		/// <returns>true if drop-down editors are resizable; otherwise, false. </returns>
		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000D91 RID: 3473 RVA: 0x0000915C File Offset: 0x0000735C
		public virtual bool IsDropDownResizable
		{
			get
			{
				return false;
			}
		}

		/// <summary>Edits the value of the specified object using the editor style indicated by the <see cref="M:System.Drawing.Design.UITypeEditor.GetEditStyle" /> method.</summary>
		/// <returns>The new value of the object.</returns>
		/// <param name="provider">An <see cref="T:System.IServiceProvider" /> that this editor can use to obtain services. </param>
		/// <param name="value">The object to edit. </param>
		// Token: 0x06000D92 RID: 3474 RVA: 0x0001DAAF File Offset: 0x0001BCAF
		public object EditValue(IServiceProvider provider, object value)
		{
			return this.EditValue(null, provider, value);
		}

		/// <summary>Edits the specified object's value using the editor style indicated by the <see cref="M:System.Drawing.Design.UITypeEditor.GetEditStyle" /> method.</summary>
		/// <returns>The new value of the object. If the value of the object has not changed, this should return the same object it was passed.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information. </param>
		/// <param name="provider">An <see cref="T:System.IServiceProvider" /> that this editor can use to obtain services. </param>
		/// <param name="value">The object to edit. </param>
		// Token: 0x06000D93 RID: 3475 RVA: 0x0001DABA File Offset: 0x0001BCBA
		public virtual object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			return value;
		}

		/// <summary>Gets the editor style used by the <see cref="M:System.Drawing.Design.UITypeEditor.EditValue(System.IServiceProvider,System.Object)" /> method.</summary>
		/// <returns>A <see cref="T:System.Drawing.Design.UITypeEditorEditStyle" /> enumeration value that indicates the style of editor used by the current <see cref="T:System.Drawing.Design.UITypeEditor" />. By default, this method will return <see cref="F:System.Drawing.Design.UITypeEditorEditStyle.None" />.</returns>
		// Token: 0x06000D94 RID: 3476 RVA: 0x0001DABD File Offset: 0x0001BCBD
		public UITypeEditorEditStyle GetEditStyle()
		{
			return this.GetEditStyle(null);
		}

		/// <summary>Indicates whether this editor supports painting a representation of an object's value.</summary>
		/// <returns>true if <see cref="M:System.Drawing.Design.UITypeEditor.PaintValue(System.Object,System.Drawing.Graphics,System.Drawing.Rectangle)" /> is implemented; otherwise, false.</returns>
		// Token: 0x06000D95 RID: 3477 RVA: 0x0001DAC6 File Offset: 0x0001BCC6
		public bool GetPaintValueSupported()
		{
			return this.GetPaintValueSupported(null);
		}

		/// <summary>Indicates whether the specified context supports painting a representation of an object's value within the specified context.</summary>
		/// <returns>true if <see cref="M:System.Drawing.Design.UITypeEditor.PaintValue(System.Object,System.Drawing.Graphics,System.Drawing.Rectangle)" /> is implemented; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information. </param>
		// Token: 0x06000D96 RID: 3478 RVA: 0x0000915C File Offset: 0x0000735C
		public virtual bool GetPaintValueSupported(ITypeDescriptorContext context)
		{
			return false;
		}

		/// <summary>Gets the editor style used by the <see cref="M:System.Drawing.Design.UITypeEditor.EditValue(System.IServiceProvider,System.Object)" /> method.</summary>
		/// <returns>A <see cref="T:System.Drawing.Design.UITypeEditorEditStyle" /> value that indicates the style of editor used by the <see cref="M:System.Drawing.Design.UITypeEditor.EditValue(System.IServiceProvider,System.Object)" /> method. If the <see cref="T:System.Drawing.Design.UITypeEditor" /> does not support this method, then <see cref="M:System.Drawing.Design.UITypeEditor.GetEditStyle" /> will return <see cref="F:System.Drawing.Design.UITypeEditorEditStyle.None" />.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information. </param>
		// Token: 0x06000D97 RID: 3479 RVA: 0x00006BA4 File Offset: 0x00004DA4
		public virtual UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.None;
		}

		/// <summary>Paints a representation of the value of the specified object to the specified canvas.</summary>
		/// <param name="value">The object whose value this type editor will display. </param>
		/// <param name="canvas">A drawing canvas on which to paint the representation of the object's value. </param>
		/// <param name="rectangle">A <see cref="T:System.Drawing.Rectangle" /> within whose boundaries to paint the value. </param>
		// Token: 0x06000D98 RID: 3480 RVA: 0x0001DACF File Offset: 0x0001BCCF
		public void PaintValue(object value, Graphics canvas, Rectangle rectangle)
		{
			this.PaintValue(new PaintValueEventArgs(null, value, canvas, rectangle));
		}

		/// <summary>Paints a representation of the value of an object using the specified <see cref="T:System.Drawing.Design.PaintValueEventArgs" />.</summary>
		/// <param name="e">A <see cref="T:System.Drawing.Design.PaintValueEventArgs" /> that indicates what to paint and where to paint it. </param>
		// Token: 0x06000D99 RID: 3481 RVA: 0x00002CE2 File Offset: 0x00000EE2
		public virtual void PaintValue(PaintValueEventArgs e)
		{
		}
	}
}
