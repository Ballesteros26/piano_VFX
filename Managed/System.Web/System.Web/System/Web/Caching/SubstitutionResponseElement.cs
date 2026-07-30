using System;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Web.Caching
{
	/// <summary>Represents a managed delegate that can be called to insert dynamically generated output into an output-cache response. </summary>
	// Token: 0x02000697 RID: 1687
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Unrestricted)]
	[Serializable]
	public class SubstitutionResponseElement : ResponseElement
	{
		/// <summary>Gets a reference to the substitution callback method.</summary>
		/// <returns>A callback method reference.</returns>
		// Token: 0x17001616 RID: 5654
		// (get) Token: 0x060047B3 RID: 18355 RVA: 0x000C9CC1 File Offset: 0x000C7EC1
		// (set) Token: 0x060047B4 RID: 18356 RVA: 0x000C9CC9 File Offset: 0x000C7EC9
		public HttpResponseSubstitutionCallback Callback { get; private set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.SubstitutionResponseElement" /> class.</summary>
		/// <param name="callback">The static substitution callback that was set as part of the response for an output-cached page.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="callback" /> is null. </exception>
		// Token: 0x060047B5 RID: 18357 RVA: 0x000C9CD4 File Offset: 0x000C7ED4
		public SubstitutionResponseElement(HttpResponseSubstitutionCallback callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			this.Callback = callback;
			MethodInfo method = callback.Method;
			this.typeName = method.DeclaringType.AssemblyQualifiedName;
			this.methodName = method.Name;
		}

		// Token: 0x060047B6 RID: 18358 RVA: 0x000C9D20 File Offset: 0x000C7F20
		[OnDeserialized]
		private void ObjectDeserialized(StreamingContext context)
		{
			Type type = Type.GetType(this.typeName, true);
			this.Callback = Delegate.CreateDelegate(typeof(HttpResponseSubstitutionCallback), type, this.methodName, false, true) as HttpResponseSubstitutionCallback;
		}

		// Token: 0x040025C5 RID: 9669
		private string typeName;

		// Token: 0x040025C6 RID: 9670
		private string methodName;
	}
}
