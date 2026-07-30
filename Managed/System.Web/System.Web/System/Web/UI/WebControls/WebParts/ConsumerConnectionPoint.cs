using System;
using System.Reflection;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Defines a connection point object that enables a server control acting as a consumer to form a connection with a provider.</summary>
	// Token: 0x02000482 RID: 1154
	public class ConsumerConnectionPoint : ConnectionPoint
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.ConsumerConnectionPoint" /> class.</summary>
		/// <param name="callbackMethod">The method in the consumer control that returns an interface instance to consumers to establish a connection.</param>
		/// <param name="interfaceType">The <see cref="T:System.Type" /> of the interface that the consumer receives from a provider. </param>
		/// <param name="controlType">The <see cref="T:System.Type" /> of the consumer control with which the consumer connection point is associated.</param>
		/// <param name="displayName">A friendly display name for the consumer connection point that appears to users in the connection user interface (UI).</param>
		/// <param name="id">A unique identifier for the consumer connection point.</param>
		/// <param name="allowsMultipleConnections">A Boolean value indicating whether the consumer connection point can have multiple simultaneous connections with providers.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="callbackMethod" /> is null.- or -<paramref name="interfaceType" /> is null. - or -<paramref name="controlType" /> is null.- or - <paramref name="displayName" /> is null or an empty string ("").</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="controlType " />is not the same type as the consumer control (or a valid class derived from it).</exception>
		// Token: 0x06003459 RID: 13401 RVA: 0x0008AAAA File Offset: 0x00088CAA
		public ConsumerConnectionPoint(MethodInfo callbackMethod, Type interfaceType, Type controlType, string displayName, string id, bool allowsMultipleConnections)
			: base(callbackMethod, interfaceType, controlType, displayName, id, allowsMultipleConnections)
		{
		}

		/// <summary>Invokes the callback method in a consumer control and retrieves the interface instance from a provider control.</summary>
		/// <param name="control">The consumer control associated with a consumer connection point.</param>
		/// <param name="data">The interface instance returned from a provider control.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="control" /> is null.</exception>
		// Token: 0x0600345A RID: 13402 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public virtual void SetObject(Control control, object data)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600345B RID: 13403 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public virtual bool SupportsConnection(Control control, TypeCollection interfaces)
		{
			throw new NotImplementedException();
		}

		/// <summary>Determines whether a consumer connection point is currently capable of establishing a connection.</summary>
		/// <returns>true if a connection point can currently establish a connection; otherwise, false. The default is true.</returns>
		/// <param name="control">The consumer control associated with the <see cref="T:System.Web.UI.WebControls.WebParts.ConsumerConnectionPoint" />. </param>
		/// <param name="secondaryInterfaces">A <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionInterfaceCollection" /> of any secondary interfaces that participate in a connection.  </param>
		// Token: 0x0600345C RID: 13404 RVA: 0x0008AABC File Offset: 0x00088CBC
		public virtual bool SupportsConnection(Control control, ConnectionInterfaceCollection secondaryInterfaces)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}
	}
}
