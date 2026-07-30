using System;

namespace System.Windows.Forms
{
	/// <summary>Defines a method that executes a certain action on the type that implements this interface.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001C4 RID: 452
	public interface ICommandExecutor
	{
		/// <summary>Performs a task that is determined by the type that implements this method. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DCD RID: 7629
		void Execute();
	}
}
