using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Represents an attribute that specifies that values for model binding are provided by session state.</summary>
	// Token: 0x02000734 RID: 1844
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
	public sealed class SessionAttribute : ValueProviderSourceAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.SessionAttribute" /> class.</summary>
		// Token: 0x06004C56 RID: 19542 RVA: 0x0000393A File Offset: 0x00001B3A
		public SessionAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.SessionAttribute" /> class, using the session state name.</summary>
		/// <param name="name">The name.</param>
		// Token: 0x06004C57 RID: 19543 RVA: 0x0000393A File Offset: 0x00001B3A
		public SessionAttribute(string name)
		{
		}

		/// <summary>Gets the session state name.</summary>
		/// <returns>The session state name.</returns>
		// Token: 0x17001783 RID: 6019
		// (get) Token: 0x06004C58 RID: 19544 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string Name
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the model name.</summary>
		/// <returns>The model name.</returns>
		// Token: 0x06004C59 RID: 19545 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override string GetModelName()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets the value provider.</summary>
		/// <returns>A new instance of the <see cref="T:System.Web.ModelBinding.DictionaryValueProvider`1" /> that contains session state values.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		// Token: 0x06004C5A RID: 19546 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override IValueProvider GetValueProvider(ModelBindingExecutionContext modelBindingExecutionContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
