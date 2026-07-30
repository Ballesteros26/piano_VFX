using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000508 RID: 1288
	internal interface ITaskCompletionAction
	{
		// Token: 0x06003B12 RID: 15122
		void Invoke(Task completingTask);
	}
}
