using System;

namespace System.Web.UI.Design
{
	/// <summary>Provides access to the state of the control designer in the design host through the <see cref="T:System.ComponentModel.Design.IComponentDesignerStateService" /> interface. This class cannot be inherited. </summary>
	// Token: 0x0200005B RID: 91
	public sealed class ControlDesignerState
	{
		// Token: 0x06000302 RID: 770 RVA: 0x00002352 File Offset: 0x00000552
		internal ControlDesignerState()
		{
		}

		/// <summary>Represents one element, identified by the given key, in the state collection for a control designer.</summary>
		/// <returns>The object identified by <paramref name="key" />.</returns>
		/// <param name="key">The name of the item to set or retrieve from the state collection.</param>
		// Token: 0x170000A4 RID: 164
		[MonoNotSupported("")]
		public object this[string key]
		{
			[MonoNotSupported("")]
			get
			{
				throw new NotImplementedException();
			}
			[MonoNotSupported("")]
			set
			{
				throw new NotImplementedException();
			}
		}
	}
}
