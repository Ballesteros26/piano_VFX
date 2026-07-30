using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	/// <summary>Provides an interface for managing designer transactions and components.</summary>
	// Token: 0x0200032A RID: 810
	[ComVisible(true)]
	public interface IDesignerHost : IServiceContainer, IServiceProvider
	{
		/// <summary>Gets a value indicating whether the designer host is currently loading the document.</summary>
		/// <returns>true if the designer host is currently loading the document; otherwise, false.</returns>
		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x060019A8 RID: 6568
		bool Loading { get; }

		/// <summary>Gets a value indicating whether the designer host is currently in a transaction.</summary>
		/// <returns>true if a transaction is in progress; otherwise, false.</returns>
		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x060019A9 RID: 6569
		bool InTransaction { get; }

		/// <summary>Gets the container for this designer host.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.IContainer" /> for this host.</returns>
		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x060019AA RID: 6570
		IContainer Container { get; }

		/// <summary>Gets the instance of the base class used as the root component for the current design.</summary>
		/// <returns>The instance of the root component class.</returns>
		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x060019AB RID: 6571
		IComponent RootComponent { get; }

		/// <summary>Gets the fully qualified name of the class being designed.</summary>
		/// <returns>The fully qualified name of the base component class.</returns>
		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x060019AC RID: 6572
		string RootComponentClassName { get; }

		/// <summary>Gets the description of the current transaction.</summary>
		/// <returns>A description of the current transaction.</returns>
		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x060019AD RID: 6573
		string TransactionDescription { get; }

		/// <summary>Occurs when this designer is activated.</summary>
		// Token: 0x14000036 RID: 54
		// (add) Token: 0x060019AE RID: 6574
		// (remove) Token: 0x060019AF RID: 6575
		event EventHandler Activated;

		/// <summary>Occurs when this designer is deactivated.</summary>
		// Token: 0x14000037 RID: 55
		// (add) Token: 0x060019B0 RID: 6576
		// (remove) Token: 0x060019B1 RID: 6577
		event EventHandler Deactivated;

		/// <summary>Occurs when this designer completes loading its document.</summary>
		// Token: 0x14000038 RID: 56
		// (add) Token: 0x060019B2 RID: 6578
		// (remove) Token: 0x060019B3 RID: 6579
		event EventHandler LoadComplete;

		/// <summary>Adds an event handler for the <see cref="E:System.ComponentModel.Design.IDesignerHost.TransactionClosed" /> event.</summary>
		// Token: 0x14000039 RID: 57
		// (add) Token: 0x060019B4 RID: 6580
		// (remove) Token: 0x060019B5 RID: 6581
		event DesignerTransactionCloseEventHandler TransactionClosed;

		/// <summary>Adds an event handler for the <see cref="E:System.ComponentModel.Design.IDesignerHost.TransactionClosing" /> event.</summary>
		// Token: 0x1400003A RID: 58
		// (add) Token: 0x060019B6 RID: 6582
		// (remove) Token: 0x060019B7 RID: 6583
		event DesignerTransactionCloseEventHandler TransactionClosing;

		/// <summary>Adds an event handler for the <see cref="E:System.ComponentModel.Design.IDesignerHost.TransactionOpened" /> event.</summary>
		// Token: 0x1400003B RID: 59
		// (add) Token: 0x060019B8 RID: 6584
		// (remove) Token: 0x060019B9 RID: 6585
		event EventHandler TransactionOpened;

		/// <summary>Adds an event handler for the <see cref="E:System.ComponentModel.Design.IDesignerHost.TransactionOpening" /> event.</summary>
		// Token: 0x1400003C RID: 60
		// (add) Token: 0x060019BA RID: 6586
		// (remove) Token: 0x060019BB RID: 6587
		event EventHandler TransactionOpening;

		/// <summary>Activates the designer that this host is hosting.</summary>
		// Token: 0x060019BC RID: 6588
		void Activate();

		/// <summary>Creates a component of the specified type and adds it to the design document.</summary>
		/// <returns>The newly created component.</returns>
		/// <param name="componentClass">The type of the component to create. </param>
		// Token: 0x060019BD RID: 6589
		IComponent CreateComponent(Type componentClass);

		/// <summary>Creates a component of the specified type and name, and adds it to the design document.</summary>
		/// <returns>The newly created component.</returns>
		/// <param name="componentClass">The type of the component to create. </param>
		/// <param name="name">The name for the component. </param>
		// Token: 0x060019BE RID: 6590
		IComponent CreateComponent(Type componentClass, string name);

		/// <summary>Creates a <see cref="T:System.ComponentModel.Design.DesignerTransaction" /> that can encapsulate event sequences to improve performance and enable undo and redo support functionality.</summary>
		/// <returns>A new instance of <see cref="T:System.ComponentModel.Design.DesignerTransaction" />. When you complete the steps in your transaction, you should call <see cref="M:System.ComponentModel.Design.DesignerTransaction.Commit" /> on this object.</returns>
		// Token: 0x060019BF RID: 6591
		DesignerTransaction CreateTransaction();

		/// <summary>Creates a <see cref="T:System.ComponentModel.Design.DesignerTransaction" /> that can encapsulate event sequences to improve performance and enable undo and redo support functionality, using the specified transaction description.</summary>
		/// <returns>A new <see cref="T:System.ComponentModel.Design.DesignerTransaction" />. When you have completed the steps in your transaction, you should call <see cref="M:System.ComponentModel.Design.DesignerTransaction.Commit" /> on this object.</returns>
		/// <param name="description">A title or description for the newly created transaction. </param>
		// Token: 0x060019C0 RID: 6592
		DesignerTransaction CreateTransaction(string description);

		/// <summary>Destroys the specified component and removes it from the designer container.</summary>
		/// <param name="component">The component to destroy. </param>
		// Token: 0x060019C1 RID: 6593
		void DestroyComponent(IComponent component);

		/// <summary>Gets the designer instance that contains the specified component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.Design.IDesigner" />, or null if there is no designer for the specified component.</returns>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to retrieve the designer for. </param>
		// Token: 0x060019C2 RID: 6594
		IDesigner GetDesigner(IComponent component);

		/// <summary>Gets an instance of the specified, fully qualified type name.</summary>
		/// <returns>The type object for the specified type name, or null if the type cannot be found.</returns>
		/// <param name="typeName">The name of the type to load. </param>
		// Token: 0x060019C3 RID: 6595
		Type GetType(string typeName);
	}
}
