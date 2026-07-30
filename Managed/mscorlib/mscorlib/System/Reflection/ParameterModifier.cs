using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Attaches a modifier to parameters so that binding can work with parameter signatures in which the types have been modified.</summary>
	// Token: 0x020002FA RID: 762
	[ComVisible(true)]
	[Serializable]
	public struct ParameterModifier
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.ParameterModifier" /> structure representing the specified number of parameters.</summary>
		/// <param name="parameterCount">The number of parameters. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="parameterCount" /> is negative. </exception>
		// Token: 0x060020FB RID: 8443 RVA: 0x0007ED69 File Offset: 0x0007CF69
		public ParameterModifier(int parameterCount)
		{
			if (parameterCount <= 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Must specify one or more parameters."));
			}
			this._byRef = new bool[parameterCount];
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x060020FC RID: 8444 RVA: 0x0007ED8B File Offset: 0x0007CF8B
		internal bool[] IsByRefArray
		{
			get
			{
				return this._byRef;
			}
		}

		/// <summary>Gets or sets a value that specifies whether the parameter at the specified index position is to be modified by the current <see cref="T:System.Reflection.ParameterModifier" />.</summary>
		/// <returns>true if the parameter at this index position is to be modified by this <see cref="T:System.Reflection.ParameterModifier" />; otherwise, false.</returns>
		/// <param name="index">The index position of the parameter whose modification status is being examined or set. </param>
		// Token: 0x170004BE RID: 1214
		public bool this[int index]
		{
			get
			{
				return this._byRef[index];
			}
			set
			{
				this._byRef[index] = value;
			}
		}

		// Token: 0x0400129D RID: 4765
		private bool[] _byRef;
	}
}
