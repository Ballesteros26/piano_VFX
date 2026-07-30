using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x0200003F RID: 63
	[Serializable]
	public class ParameterOverride<T> : ParameterOverride
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x00009314 File Offset: 0x00007514
		public ParameterOverride()
			: this(default(T), false)
		{
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00009331 File Offset: 0x00007531
		public ParameterOverride(T value)
			: this(value, false)
		{
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000933B File Offset: 0x0000753B
		public ParameterOverride(T value, bool overrideState)
		{
			this.value = value;
			this.overrideState = overrideState;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00009351 File Offset: 0x00007551
		internal override void Interp(ParameterOverride from, ParameterOverride to, float t)
		{
			this.Interp(from.GetValue<T>(), to.GetValue<T>(), t);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00009366 File Offset: 0x00007566
		public virtual void Interp(T from, T to, float t)
		{
			this.value = ((t > 0f) ? to : from);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000937A File Offset: 0x0000757A
		public void Override(T x)
		{
			this.overrideState = true;
			this.value = x;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000938A File Offset: 0x0000758A
		internal override void SetValue(ParameterOverride parameter)
		{
			this.value = parameter.GetValue<T>();
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00009398 File Offset: 0x00007598
		public override int GetHash()
		{
			return (17 * 23 + this.overrideState.GetHashCode()) * 23 + this.value.GetHashCode();
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000093C0 File Offset: 0x000075C0
		public static implicit operator T(ParameterOverride<T> prop)
		{
			return prop.value;
		}

		// Token: 0x040000F9 RID: 249
		public T value;
	}
}
