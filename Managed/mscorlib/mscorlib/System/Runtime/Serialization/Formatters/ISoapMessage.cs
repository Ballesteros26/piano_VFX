using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Serialization.Formatters
{
	/// <summary>Provides an interface for an object that contains the names and types of parameters required during serialization of a SOAP RPC (Remote Procedure Call).</summary>
	// Token: 0x020006FD RID: 1789
	[ComVisible(true)]
	public interface ISoapMessage
	{
		/// <summary>Gets or sets the parameter names of the method call.</summary>
		/// <returns>The parameter names of the method call.</returns>
		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x06004B1D RID: 19229
		// (set) Token: 0x06004B1E RID: 19230
		string[] ParamNames { get; set; }

		/// <summary>Gets or sets the parameter values of a method call.</summary>
		/// <returns>The parameter values of a method call.</returns>
		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x06004B1F RID: 19231
		// (set) Token: 0x06004B20 RID: 19232
		object[] ParamValues { get; set; }

		/// <summary>Gets or sets the parameter types of a method call.</summary>
		/// <returns>The parameter types of a method call.</returns>
		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x06004B21 RID: 19233
		// (set) Token: 0x06004B22 RID: 19234
		Type[] ParamTypes { get; set; }

		/// <summary>Gets or sets the name of the called method.</summary>
		/// <returns>The name of the called method.</returns>
		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x06004B23 RID: 19235
		// (set) Token: 0x06004B24 RID: 19236
		string MethodName { get; set; }

		/// <summary>Gets or sets the XML namespace of the SOAP RPC (Remote Procedure Call) <see cref="P:System.Runtime.Serialization.Formatters.ISoapMessage.MethodName" /> element.</summary>
		/// <returns>The XML namespace name where the object that contains the called method is located.</returns>
		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x06004B25 RID: 19237
		// (set) Token: 0x06004B26 RID: 19238
		string XmlNameSpace { get; set; }

		/// <summary>Gets or sets the out-of-band data of the method call.</summary>
		/// <returns>The out-of-band data of the method call.</returns>
		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x06004B27 RID: 19239
		// (set) Token: 0x06004B28 RID: 19240
		Header[] Headers { get; set; }
	}
}
