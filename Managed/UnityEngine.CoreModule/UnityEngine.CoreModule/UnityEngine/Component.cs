using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x020001A2 RID: 418
	[RequiredByNativeCode]
	[NativeHeader("Runtime/Export/Scripting/Component.bindings.h")]
	[NativeClass("Unity::Component")]
	public class Component : Object
	{
		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06001310 RID: 4880
		public extern Transform transform
		{
			[FreeFunction("GetTransform", HasExplicitThis = true, ThrowsException = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06001311 RID: 4881
		public extern GameObject gameObject
		{
			[FreeFunction("GetGameObject", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x0001F430 File Offset: 0x0001D630
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponent(Type type)
		{
			return this.gameObject.GetComponent(type);
		}

		// Token: 0x06001313 RID: 4883
		[FreeFunction(HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		internal extern void GetComponentFastPath(Type type, IntPtr oneFurtherThanResultValue);

		// Token: 0x06001314 RID: 4884 RVA: 0x0001F450 File Offset: 0x0001D650
		[SecuritySafeCritical]
		public unsafe T GetComponent<T>()
		{
			CastHelper<T> castHelper = default(CastHelper<T>);
			this.GetComponentFastPath(typeof(T), new IntPtr((void*)(&castHelper.onePointerFurtherThanT)));
			return castHelper.t;
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x0001F490 File Offset: 0x0001D690
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public bool TryGetComponent(Type type, out Component component)
		{
			return this.gameObject.TryGetComponent(type, out component);
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x0001F4B0 File Offset: 0x0001D6B0
		[SecuritySafeCritical]
		public bool TryGetComponent<T>(out T component)
		{
			return this.gameObject.TryGetComponent<T>(out component);
		}

		// Token: 0x06001317 RID: 4887
		[FreeFunction(HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern Component GetComponent(string type);

		// Token: 0x06001318 RID: 4888 RVA: 0x0001F4D0 File Offset: 0x0001D6D0
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponentInChildren(Type t, bool includeInactive)
		{
			return this.gameObject.GetComponentInChildren(t, includeInactive);
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x0001F4F0 File Offset: 0x0001D6F0
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponentInChildren(Type t)
		{
			return this.GetComponentInChildren(t, false);
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x0001F50C File Offset: 0x0001D70C
		public T GetComponentInChildren<T>([DefaultValue("false")] bool includeInactive)
		{
			return (T)((object)this.GetComponentInChildren(typeof(T), includeInactive));
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x0001F534 File Offset: 0x0001D734
		[ExcludeFromDocs]
		public T GetComponentInChildren<T>()
		{
			return (T)((object)this.GetComponentInChildren(typeof(T), false));
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x0001F55C File Offset: 0x0001D75C
		public Component[] GetComponentsInChildren(Type t, bool includeInactive)
		{
			return this.gameObject.GetComponentsInChildren(t, includeInactive);
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x0001F57C File Offset: 0x0001D77C
		[ExcludeFromDocs]
		public Component[] GetComponentsInChildren(Type t)
		{
			return this.gameObject.GetComponentsInChildren(t, false);
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x0001F59C File Offset: 0x0001D79C
		public T[] GetComponentsInChildren<T>(bool includeInactive)
		{
			return this.gameObject.GetComponentsInChildren<T>(includeInactive);
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x0001F5BA File Offset: 0x0001D7BA
		public void GetComponentsInChildren<T>(bool includeInactive, List<T> result)
		{
			this.gameObject.GetComponentsInChildren<T>(includeInactive, result);
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x0001F5CC File Offset: 0x0001D7CC
		public T[] GetComponentsInChildren<T>()
		{
			return this.GetComponentsInChildren<T>(false);
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x0001F5E5 File Offset: 0x0001D7E5
		public void GetComponentsInChildren<T>(List<T> results)
		{
			this.GetComponentsInChildren<T>(false, results);
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x0001F5F4 File Offset: 0x0001D7F4
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponentInParent(Type t)
		{
			return this.gameObject.GetComponentInParent(t);
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x0001F614 File Offset: 0x0001D814
		public T GetComponentInParent<T>()
		{
			return (T)((object)this.GetComponentInParent(typeof(T)));
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x0001F63C File Offset: 0x0001D83C
		public Component[] GetComponentsInParent(Type t, [DefaultValue("false")] bool includeInactive)
		{
			return this.gameObject.GetComponentsInParent(t, includeInactive);
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x0001F65C File Offset: 0x0001D85C
		[ExcludeFromDocs]
		public Component[] GetComponentsInParent(Type t)
		{
			return this.GetComponentsInParent(t, false);
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x0001F678 File Offset: 0x0001D878
		public T[] GetComponentsInParent<T>(bool includeInactive)
		{
			return this.gameObject.GetComponentsInParent<T>(includeInactive);
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x0001F696 File Offset: 0x0001D896
		public void GetComponentsInParent<T>(bool includeInactive, List<T> results)
		{
			this.gameObject.GetComponentsInParent<T>(includeInactive, results);
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x0001F6A8 File Offset: 0x0001D8A8
		public T[] GetComponentsInParent<T>()
		{
			return this.GetComponentsInParent<T>(false);
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x0001F6C4 File Offset: 0x0001D8C4
		public Component[] GetComponents(Type type)
		{
			return this.gameObject.GetComponents(type);
		}

		// Token: 0x0600132A RID: 4906
		[FreeFunction(HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private extern void GetComponentsForListInternal(Type searchType, object resultList);

		// Token: 0x0600132B RID: 4907 RVA: 0x0001F6E2 File Offset: 0x0001D8E2
		public void GetComponents(Type type, List<Component> results)
		{
			this.GetComponentsForListInternal(type, results);
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x0001F6EE File Offset: 0x0001D8EE
		public void GetComponents<T>(List<T> results)
		{
			this.GetComponentsForListInternal(typeof(T), results);
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x0600132D RID: 4909 RVA: 0x0001F704 File Offset: 0x0001D904
		// (set) Token: 0x0600132E RID: 4910 RVA: 0x0001F721 File Offset: 0x0001D921
		public string tag
		{
			get
			{
				return this.gameObject.tag;
			}
			set
			{
				this.gameObject.tag = value;
			}
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x0001F734 File Offset: 0x0001D934
		public T[] GetComponents<T>()
		{
			return this.gameObject.GetComponents<T>();
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x0001F754 File Offset: 0x0001D954
		public bool CompareTag(string tag)
		{
			return this.gameObject.CompareTag(tag);
		}

		// Token: 0x06001331 RID: 4913
		[FreeFunction(HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SendMessageUpwards(string methodName, [DefaultValue("null")] object value, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		// Token: 0x06001332 RID: 4914 RVA: 0x0001F772 File Offset: 0x0001D972
		[ExcludeFromDocs]
		public void SendMessageUpwards(string methodName, object value)
		{
			this.SendMessageUpwards(methodName, value, SendMessageOptions.RequireReceiver);
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x0001F77F File Offset: 0x0001D97F
		[ExcludeFromDocs]
		public void SendMessageUpwards(string methodName)
		{
			this.SendMessageUpwards(methodName, null, SendMessageOptions.RequireReceiver);
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x0001F78C File Offset: 0x0001D98C
		public void SendMessageUpwards(string methodName, SendMessageOptions options)
		{
			this.SendMessageUpwards(methodName, null, options);
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x0001F799 File Offset: 0x0001D999
		public void SendMessage(string methodName, object value)
		{
			this.SendMessage(methodName, value, SendMessageOptions.RequireReceiver);
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x0001F7A6 File Offset: 0x0001D9A6
		public void SendMessage(string methodName)
		{
			this.SendMessage(methodName, null, SendMessageOptions.RequireReceiver);
		}

		// Token: 0x06001337 RID: 4919
		[FreeFunction("SendMessage", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SendMessage(string methodName, object value, SendMessageOptions options);

		// Token: 0x06001338 RID: 4920 RVA: 0x0001F7B3 File Offset: 0x0001D9B3
		public void SendMessage(string methodName, SendMessageOptions options)
		{
			this.SendMessage(methodName, null, options);
		}

		// Token: 0x06001339 RID: 4921
		[FreeFunction("BroadcastMessage", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void BroadcastMessage(string methodName, [DefaultValue("null")] object parameter, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		// Token: 0x0600133A RID: 4922 RVA: 0x0001F7C0 File Offset: 0x0001D9C0
		[ExcludeFromDocs]
		public void BroadcastMessage(string methodName, object parameter)
		{
			this.BroadcastMessage(methodName, parameter, SendMessageOptions.RequireReceiver);
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x0001F7CD File Offset: 0x0001D9CD
		[ExcludeFromDocs]
		public void BroadcastMessage(string methodName)
		{
			this.BroadcastMessage(methodName, null, SendMessageOptions.RequireReceiver);
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x0001F7DA File Offset: 0x0001D9DA
		public void BroadcastMessage(string methodName, SendMessageOptions options)
		{
			this.BroadcastMessage(methodName, null, options);
		}
	}
}
