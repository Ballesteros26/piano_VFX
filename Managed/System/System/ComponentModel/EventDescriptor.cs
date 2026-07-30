using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides information about an event.</summary>
	// Token: 0x0200026C RID: 620
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public abstract class EventDescriptor : MemberDescriptor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EventDescriptor" /> class with the specified name and attribute array.</summary>
		/// <param name="name">The name of the event. </param>
		/// <param name="attrs">An array of type <see cref="T:System.Attribute" /> that contains the event attributes. </param>
		// Token: 0x060013C9 RID: 5065 RVA: 0x00052259 File Offset: 0x00050459
		protected EventDescriptor(string name, Attribute[] attrs)
			: base(name, attrs)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EventDescriptor" /> class with the name and attributes in the specified <see cref="T:System.ComponentModel.MemberDescriptor" />.</summary>
		/// <param name="descr">A <see cref="T:System.ComponentModel.MemberDescriptor" /> that contains the name of the event and its attributes. </param>
		// Token: 0x060013CA RID: 5066 RVA: 0x00052263 File Offset: 0x00050463
		protected EventDescriptor(MemberDescriptor descr)
			: base(descr)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EventDescriptor" /> class with the name in the specified <see cref="T:System.ComponentModel.MemberDescriptor" /> and the attributes in both the <see cref="T:System.ComponentModel.MemberDescriptor" /> and the <see cref="T:System.Attribute" /> array.</summary>
		/// <param name="descr">A <see cref="T:System.ComponentModel.MemberDescriptor" /> that has the name of the member and its attributes. </param>
		/// <param name="attrs">An <see cref="T:System.Attribute" /> array with the attributes you want to add to this event description. </param>
		// Token: 0x060013CB RID: 5067 RVA: 0x0005226C File Offset: 0x0005046C
		protected EventDescriptor(MemberDescriptor descr, Attribute[] attrs)
			: base(descr, attrs)
		{
		}

		/// <summary>When overridden in a derived class, gets the type of component this event is bound to.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the type of component the event is bound to.</returns>
		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x060013CC RID: 5068
		public abstract Type ComponentType { get; }

		/// <summary>When overridden in a derived class, gets the type of delegate for the event.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the type of delegate for the event.</returns>
		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x060013CD RID: 5069
		public abstract Type EventType { get; }

		/// <summary>When overridden in a derived class, gets a value indicating whether the event delegate is a multicast delegate.</summary>
		/// <returns>true if the event delegate is multicast; otherwise, false.</returns>
		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x060013CE RID: 5070
		public abstract bool IsMulticast { get; }

		/// <summary>When overridden in a derived class, binds the event to the component.</summary>
		/// <param name="component">A component that provides events to the delegate. </param>
		/// <param name="value">A delegate that represents the method that handles the event. </param>
		// Token: 0x060013CF RID: 5071
		public abstract void AddEventHandler(object component, Delegate value);

		/// <summary>When overridden in a derived class, unbinds the delegate from the component so that the delegate will no longer receive events from the component.</summary>
		/// <param name="component">The component that the delegate is bound to. </param>
		/// <param name="value">The delegate to unbind from the component. </param>
		// Token: 0x060013D0 RID: 5072
		public abstract void RemoveEventHandler(object component, Delegate value);
	}
}
