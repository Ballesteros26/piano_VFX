using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Identifies the source interface and the class that implements the methods of the event interface that is generated when a coclass is imported from a COM type library.</summary>
	// Token: 0x020008CE RID: 2254
	[AttributeUsage(AttributeTargets.Interface, Inherited = false)]
	[ComVisible(true)]
	public sealed class ComEventInterfaceAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ComEventInterfaceAttribute" /> class with the source interface and event provider class.</summary>
		/// <param name="SourceInterface">A <see cref="T:System.Type" /> that contains the original source interface from the type library. COM uses this interface to call back to the managed class. </param>
		/// <param name="EventProvider">A <see cref="T:System.Type" /> that contains the class that implements the methods of the event interface. </param>
		// Token: 0x0600552F RID: 21807 RVA: 0x00128993 File Offset: 0x00126B93
		public ComEventInterfaceAttribute(Type SourceInterface, Type EventProvider)
		{
			this._SourceInterface = SourceInterface;
			this._EventProvider = EventProvider;
		}

		/// <summary>Gets the original source interface from the type library.</summary>
		/// <returns>A <see cref="T:System.Type" /> containing the source interface.</returns>
		// Token: 0x17000EE8 RID: 3816
		// (get) Token: 0x06005530 RID: 21808 RVA: 0x001289A9 File Offset: 0x00126BA9
		public Type SourceInterface
		{
			get
			{
				return this._SourceInterface;
			}
		}

		/// <summary>Gets the class that implements the methods of the event interface.</summary>
		/// <returns>A <see cref="T:System.Type" /> that contains the class that implements the methods of the event interface.</returns>
		// Token: 0x17000EE9 RID: 3817
		// (get) Token: 0x06005531 RID: 21809 RVA: 0x001289B1 File Offset: 0x00126BB1
		public Type EventProvider
		{
			get
			{
				return this._EventProvider;
			}
		}

		// Token: 0x04002CAD RID: 11437
		internal Type _SourceInterface;

		// Token: 0x04002CAE RID: 11438
		internal Type _EventProvider;
	}
}
