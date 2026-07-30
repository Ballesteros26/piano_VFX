using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Determines whether the <see cref="T:System.Runtime.Remoting.Messaging.RemotingSurrogateSelector" /> class should ignore a particular <see cref="T:System.Runtime.Remoting.Messaging.IMessage" /> property while creating an <see cref="T:System.Runtime.Remoting.ObjRef" /> for a <see cref="T:System.MarshalByRefObject" /> class.</summary>
	/// <returns>True if the <see cref="T:System.Runtime.Remoting.Messaging.RemotingSurrogateSelector" /> class should ignore a particular <see cref="T:System.Runtime.Remoting.Messaging.IMessage" /> property while creating an <see cref="T:System.Runtime.Remoting.ObjRef" /> for a <see cref="T:System.MarshalByRefObject" /> class.</returns>
	/// <param name="key">The key to a particular remoting message property.</param>
	/// <param name="value">The value of a particular remoting message property.</param>
	// Token: 0x02000813 RID: 2067
	// (Invoke) Token: 0x06005272 RID: 21106
	[ComVisible(true)]
	public delegate bool MessageSurrogateFilter(string key, object value);
}
