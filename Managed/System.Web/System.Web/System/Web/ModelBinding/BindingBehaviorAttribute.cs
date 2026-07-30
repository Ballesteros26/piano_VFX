using System;

namespace System.Web.ModelBinding
{
	/// <summary>Provides a base class for model-binding behavior attributes. </summary>
	// Token: 0x02000518 RID: 1304
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class BindingBehaviorAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.BindingBehaviorAttribute" /> class.</summary>
		/// <param name="behavior">The model-binding behavior.</param>
		// Token: 0x060039D0 RID: 14800 RVA: 0x0009CCBF File Offset: 0x0009AEBF
		public BindingBehaviorAttribute(BindingBehavior behavior)
		{
			this.Behavior = behavior;
		}

		/// <summary>Gets the model-binding behavior value.</summary>
		/// <returns>The model-binding behavior value.</returns>
		// Token: 0x170011E1 RID: 4577
		// (get) Token: 0x060039D1 RID: 14801 RVA: 0x0009CCCE File Offset: 0x0009AECE
		// (set) Token: 0x060039D2 RID: 14802 RVA: 0x0009CCD6 File Offset: 0x0009AED6
		public BindingBehavior Behavior { get; private set; }

		/// <summary>Gets the unique identifier for this attribute.</summary>
		/// <returns>The unique identifier for this attribute.</returns>
		// Token: 0x170011E2 RID: 4578
		// (get) Token: 0x060039D3 RID: 14803 RVA: 0x0009CCDF File Offset: 0x0009AEDF
		public override object TypeId
		{
			get
			{
				return BindingBehaviorAttribute._typeId;
			}
		}

		// Token: 0x04001F45 RID: 8005
		private static readonly object _typeId = new object();
	}
}
