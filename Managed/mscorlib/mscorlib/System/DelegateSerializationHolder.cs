using System;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000211 RID: 529
	[Serializable]
	internal class DelegateSerializationHolder : ISerializable, IObjectReference
	{
		// Token: 0x0600191F RID: 6431 RVA: 0x0005EE90 File Offset: 0x0005D090
		private DelegateSerializationHolder(SerializationInfo info, StreamingContext ctx)
		{
			DelegateSerializationHolder.DelegateEntry delegateEntry = (DelegateSerializationHolder.DelegateEntry)info.GetValue("Delegate", typeof(DelegateSerializationHolder.DelegateEntry));
			int num = 0;
			DelegateSerializationHolder.DelegateEntry delegateEntry2 = delegateEntry;
			while (delegateEntry2 != null)
			{
				delegateEntry2 = delegateEntry2.delegateEntry;
				num++;
			}
			if (num == 1)
			{
				this._delegate = delegateEntry.DeserializeDelegate(info, 0);
				return;
			}
			Delegate[] array = new Delegate[num];
			delegateEntry2 = delegateEntry;
			for (int i = 0; i < num; i++)
			{
				array[i] = delegateEntry2.DeserializeDelegate(info, i);
				delegateEntry2 = delegateEntry2.delegateEntry;
			}
			this._delegate = Delegate.Combine(array);
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x0005EF20 File Offset: 0x0005D120
		public static void GetDelegateData(Delegate instance, SerializationInfo info, StreamingContext ctx)
		{
			Delegate[] invocationList = instance.GetInvocationList();
			DelegateSerializationHolder.DelegateEntry delegateEntry = null;
			for (int i = 0; i < invocationList.Length; i++)
			{
				Delegate @delegate = invocationList[i];
				string text = ((@delegate.Target != null) ? ("target" + i) : null);
				DelegateSerializationHolder.DelegateEntry delegateEntry2 = new DelegateSerializationHolder.DelegateEntry(@delegate, text);
				if (delegateEntry == null)
				{
					info.AddValue("Delegate", delegateEntry2);
				}
				else
				{
					delegateEntry.delegateEntry = delegateEntry2;
				}
				delegateEntry = delegateEntry2;
				if (@delegate.Target != null)
				{
					info.AddValue(text, @delegate.Target);
				}
				info.AddValue("method" + i, @delegate.Method);
			}
			info.SetType(typeof(DelegateSerializationHolder));
		}

		// Token: 0x06001921 RID: 6433 RVA: 0x00014B5A File Offset: 0x00012D5A
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x0005EFD1 File Offset: 0x0005D1D1
		public object GetRealObject(StreamingContext context)
		{
			return this._delegate;
		}

		// Token: 0x04000CAA RID: 3242
		private Delegate _delegate;

		// Token: 0x02000212 RID: 530
		[Serializable]
		private class DelegateEntry
		{
			// Token: 0x06001923 RID: 6435 RVA: 0x0005EFDC File Offset: 0x0005D1DC
			public DelegateEntry(Delegate del, string targetLabel)
			{
				this.type = del.GetType().FullName;
				this.assembly = del.GetType().Assembly.FullName;
				this.target = targetLabel;
				this.targetTypeAssembly = del.Method.DeclaringType.Assembly.FullName;
				this.targetTypeName = del.Method.DeclaringType.FullName;
				this.methodName = del.Method.Name;
			}

			// Token: 0x06001924 RID: 6436 RVA: 0x0005F060 File Offset: 0x0005D260
			public Delegate DeserializeDelegate(SerializationInfo info, int index)
			{
				object obj = null;
				if (this.target != null)
				{
					obj = info.GetValue(this.target.ToString(), typeof(object));
				}
				string text = "method" + index;
				MethodInfo methodInfo = (MethodInfo)info.GetValueNoThrow(text, typeof(MethodInfo));
				Type type = Assembly.Load(this.assembly).GetType(this.type);
				if (obj != null)
				{
					if (RemotingServices.IsTransparentProxy(obj) && !Assembly.Load(this.targetTypeAssembly).GetType(this.targetTypeName).IsInstanceOfType(obj))
					{
						throw new RemotingException("Unexpected proxy type.");
					}
					if (!(methodInfo == null))
					{
						return Delegate.CreateDelegate(type, obj, methodInfo);
					}
					return Delegate.CreateDelegate(type, obj, this.methodName);
				}
				else
				{
					if (methodInfo != null)
					{
						return Delegate.CreateDelegate(type, obj, methodInfo);
					}
					Type type2 = Assembly.Load(this.targetTypeAssembly).GetType(this.targetTypeName);
					return Delegate.CreateDelegate(type, type2, this.methodName);
				}
			}

			// Token: 0x04000CAB RID: 3243
			private string type;

			// Token: 0x04000CAC RID: 3244
			private string assembly;

			// Token: 0x04000CAD RID: 3245
			private object target;

			// Token: 0x04000CAE RID: 3246
			private string targetTypeAssembly;

			// Token: 0x04000CAF RID: 3247
			private string targetTypeName;

			// Token: 0x04000CB0 RID: 3248
			private string methodName;

			// Token: 0x04000CB1 RID: 3249
			public DelegateSerializationHolder.DelegateEntry delegateEntry;
		}
	}
}
