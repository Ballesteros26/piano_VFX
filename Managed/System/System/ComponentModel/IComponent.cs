using System;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Runtime.InteropServices;

namespace System.ComponentModel
{
	/// <summary>Provides functionality required by all components. </summary>
	// Token: 0x0200027B RID: 635
	[Designer("System.ComponentModel.Design.ComponentDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(IDesigner))]
	[Designer("System.Windows.Forms.Design.ComponentDocumentDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(IRootDesigner))]
	[TypeConverter(typeof(ComponentConverter))]
	[ComVisible(true)]
	[RootDesignerSerializer("System.ComponentModel.Design.Serialization.RootCodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", true)]
	public interface IComponent : IDisposable
	{
		/// <summary>Gets or sets the <see cref="T:System.ComponentModel.ISite" /> associated with the <see cref="T:System.ComponentModel.IComponent" />.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ISite" /> object associated with the component; or null, if the component does not have a site.</returns>
		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x0600144E RID: 5198
		// (set) Token: 0x0600144F RID: 5199
		ISite Site { get; set; }

		/// <summary>Represents the method that handles the <see cref="E:System.ComponentModel.IComponent.Disposed" /> event of a component.</summary>
		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06001450 RID: 5200
		// (remove) Token: 0x06001451 RID: 5201
		event EventHandler Disposed;
	}
}
