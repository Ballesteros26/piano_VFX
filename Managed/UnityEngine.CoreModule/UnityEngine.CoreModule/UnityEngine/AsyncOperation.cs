using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000191 RID: 401
	[NativeHeader("Runtime/Misc/AsyncOperation.h")]
	[NativeHeader("Runtime/Export/Scripting/AsyncOperation.bindings.h")]
	[RequiredByNativeCode]
	[StructLayout(0)]
	public class AsyncOperation : YieldInstruction
	{
		// Token: 0x060012DD RID: 4829
		[StaticAccessor("AsyncOperationBindings", StaticAccessorType.DoubleColon)]
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern void InternalDestroy(IntPtr ptr);

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x060012DE RID: 4830
		public extern bool isDone
		{
			[NativeMethod("IsDone")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x060012DF RID: 4831
		public extern float progress
		{
			[NativeMethod("GetProgress")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x060012E0 RID: 4832
		// (set) Token: 0x060012E1 RID: 4833
		public extern int priority
		{
			[NativeMethod("GetPriority")]
			[MethodImpl(4096)]
			get;
			[NativeMethod("SetPriority")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x060012E2 RID: 4834
		// (set) Token: 0x060012E3 RID: 4835
		public extern bool allowSceneActivation
		{
			[NativeMethod("GetAllowSceneActivation")]
			[MethodImpl(4096)]
			get;
			[NativeMethod("SetAllowSceneActivation")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x0001EF30 File Offset: 0x0001D130
		~AsyncOperation()
		{
			AsyncOperation.InternalDestroy(this.m_Ptr);
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x0001EF68 File Offset: 0x0001D168
		[RequiredByNativeCode]
		internal void InvokeCompletionEvent()
		{
			bool flag = this.m_completeCallback != null;
			if (flag)
			{
				this.m_completeCallback.Invoke(this);
				this.m_completeCallback = null;
			}
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060012E6 RID: 4838 RVA: 0x0001EF9C File Offset: 0x0001D19C
		// (remove) Token: 0x060012E7 RID: 4839 RVA: 0x0001EFD9 File Offset: 0x0001D1D9
		public event Action<AsyncOperation> completed
		{
			add
			{
				bool isDone = this.isDone;
				if (isDone)
				{
					value.Invoke(this);
				}
				else
				{
					this.m_completeCallback = (Action<AsyncOperation>)Delegate.Combine(this.m_completeCallback, value);
				}
			}
			remove
			{
				this.m_completeCallback = (Action<AsyncOperation>)Delegate.Remove(this.m_completeCallback, value);
			}
		}

		// Token: 0x04000636 RID: 1590
		internal IntPtr m_Ptr;

		// Token: 0x04000637 RID: 1591
		private Action<AsyncOperation> m_completeCallback;
	}
}
