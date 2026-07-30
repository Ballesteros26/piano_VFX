using System;
using System.Collections;
using System.Reflection;

namespace System.Diagnostics
{
	/// <summary>Identifies a switch used in an assembly, class, or member.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001BC RID: 444
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event)]
	public sealed class SwitchAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.SwitchAttribute" /> class, specifying the name and the type of the switch. </summary>
		/// <param name="switchName">The display name of the switch.</param>
		/// <param name="switchType">The type of the switch.</param>
		// Token: 0x06000D20 RID: 3360 RVA: 0x0003F2FC File Offset: 0x0003D4FC
		public SwitchAttribute(string switchName, Type switchType)
		{
			this.SwitchName = switchName;
			this.SwitchType = switchType;
		}

		/// <summary>Gets or sets the display name of the switch.</summary>
		/// <returns>The display name of the switch.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Diagnostics.SwitchAttribute.SwitchName" /> is set to null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Diagnostics.SwitchAttribute.SwitchName" /> is set to an empty string.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000D21 RID: 3361 RVA: 0x0003F312 File Offset: 0x0003D512
		// (set) Token: 0x06000D22 RID: 3362 RVA: 0x0003F31C File Offset: 0x0003D51C
		public string SwitchName
		{
			get
			{
				return this.name;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length == 0)
				{
					throw new ArgumentException(global::SR.GetString("Argument {0} cannot be null or zero-length.", new object[] { "value" }), "value");
				}
				this.name = value;
			}
		}

		/// <summary>Gets or sets the type of the switch.</summary>
		/// <returns>The type of the switch.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Diagnostics.SwitchAttribute.SwitchType" /> is set to null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000D23 RID: 3363 RVA: 0x0003F369 File Offset: 0x0003D569
		// (set) Token: 0x06000D24 RID: 3364 RVA: 0x0003F371 File Offset: 0x0003D571
		public Type SwitchType
		{
			get
			{
				return this.type;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.type = value;
			}
		}

		/// <summary>Gets or sets the description of the switch.</summary>
		/// <returns>The description of the switch.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000D25 RID: 3365 RVA: 0x0003F38E File Offset: 0x0003D58E
		// (set) Token: 0x06000D26 RID: 3366 RVA: 0x0003F396 File Offset: 0x0003D596
		public string SwitchDescription
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		/// <summary>Returns all switch attributes for the specified assembly.</summary>
		/// <returns>An array that contains all the switch attributes for the assembly.</returns>
		/// <param name="assembly">The assembly to check for switch attributes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assembly" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D27 RID: 3367 RVA: 0x0003F3A0 File Offset: 0x0003D5A0
		public static SwitchAttribute[] GetAll(Assembly assembly)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			ArrayList arrayList = new ArrayList();
			object[] customAttributes = assembly.GetCustomAttributes(typeof(SwitchAttribute), false);
			arrayList.AddRange(customAttributes);
			Type[] types = assembly.GetTypes();
			for (int i = 0; i < types.Length; i++)
			{
				SwitchAttribute.GetAllRecursive(types[i], arrayList);
			}
			SwitchAttribute[] array = new SwitchAttribute[arrayList.Count];
			arrayList.CopyTo(array, 0);
			return array;
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x0003F418 File Offset: 0x0003D618
		private static void GetAllRecursive(Type type, ArrayList switchAttribs)
		{
			SwitchAttribute.GetAllRecursive(type, switchAttribs);
			MemberInfo[] members = type.GetMembers(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			for (int i = 0; i < members.Length; i++)
			{
				if (!(members[i] is Type))
				{
					SwitchAttribute.GetAllRecursive(members[i], switchAttribs);
				}
			}
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x0003F458 File Offset: 0x0003D658
		private static void GetAllRecursive(MemberInfo member, ArrayList switchAttribs)
		{
			object[] customAttributes = member.GetCustomAttributes(typeof(SwitchAttribute), false);
			switchAttribs.AddRange(customAttributes);
		}

		// Token: 0x0400103E RID: 4158
		private Type type;

		// Token: 0x0400103F RID: 4159
		private string name;

		// Token: 0x04001040 RID: 4160
		private string description;
	}
}
