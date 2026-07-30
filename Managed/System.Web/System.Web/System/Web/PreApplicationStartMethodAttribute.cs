using System;

namespace System.Web
{
	/// <summary>Provides expanded support for application startup.</summary>
	// Token: 0x02000053 RID: 83
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	public sealed class PreApplicationStartMethodAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.PreApplicationStartMethodAttribute" /> class.</summary>
		/// <param name="type">An object that describes the type of the startup method..</param>
		/// <param name="methodName">An empty parameter signature that has no return value. </param>
		// Token: 0x060003D7 RID: 983 RVA: 0x00007303 File Offset: 0x00005503
		public PreApplicationStartMethodAttribute(Type type, string methodName)
		{
			this._type = type;
			this._methodName = methodName;
		}

		/// <summary>Gets the type that is returned by the associated startup method.</summary>
		/// <returns>An object that describes the type of the startup method.</returns>
		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x00007319 File Offset: 0x00005519
		public Type Type
		{
			get
			{
				return this._type;
			}
		}

		/// <summary>Gets the associated startup method.</summary>
		/// <returns>A string that contains the name of the associated startup method.</returns>
		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x00007321 File Offset: 0x00005521
		public string MethodName
		{
			get
			{
				return this._methodName;
			}
		}

		// Token: 0x04000E12 RID: 3602
		private readonly Type _type;

		// Token: 0x04000E13 RID: 3603
		private readonly string _methodName;
	}
}
