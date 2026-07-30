using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Specifies the class identifier of a coclass imported from a type library.</summary>
	// Token: 0x020008CD RID: 2253
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Interface, Inherited = false)]
	public sealed class CoClassAttribute : Attribute
	{
		/// <summary>Initializes new instance of the <see cref="T:System.Runtime.InteropServices.CoClassAttribute" /> with the class identifier of the original coclass.</summary>
		/// <param name="coClass">A <see cref="T:System.Type" /> that contains the class identifier of the original coclass. </param>
		// Token: 0x0600552D RID: 21805 RVA: 0x0012897C File Offset: 0x00126B7C
		public CoClassAttribute(Type coClass)
		{
			this._CoClass = coClass;
		}

		/// <summary>Gets the class identifier of the original coclass.</summary>
		/// <returns>A <see cref="T:System.Type" /> containing the class identifier of the original coclass.</returns>
		// Token: 0x17000EE7 RID: 3815
		// (get) Token: 0x0600552E RID: 21806 RVA: 0x0012898B File Offset: 0x00126B8B
		public Type CoClass
		{
			get
			{
				return this._CoClass;
			}
		}

		// Token: 0x04002CAC RID: 11436
		internal Type _CoClass;
	}
}
