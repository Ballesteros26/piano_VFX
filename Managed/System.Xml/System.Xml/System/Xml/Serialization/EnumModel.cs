using System;
using System.Collections;
using System.Reflection;

namespace System.Xml.Serialization
{
	// Token: 0x020002FE RID: 766
	internal class EnumModel : TypeModel
	{
		// Token: 0x06001C8B RID: 7307 RVA: 0x0009B6B0 File Offset: 0x000998B0
		internal EnumModel(Type type, TypeDesc typeDesc, ModelScope scope)
			: base(type, typeDesc, scope)
		{
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06001C8C RID: 7308 RVA: 0x0009BC44 File Offset: 0x00099E44
		internal ConstantModel[] Constants
		{
			get
			{
				if (this.constants == null)
				{
					ArrayList arrayList = new ArrayList();
					foreach (FieldInfo fieldInfo in base.Type.GetFields())
					{
						ConstantModel constantModel = this.GetConstantModel(fieldInfo);
						if (constantModel != null)
						{
							arrayList.Add(constantModel);
						}
					}
					this.constants = (ConstantModel[])arrayList.ToArray(typeof(ConstantModel));
				}
				return this.constants;
			}
		}

		// Token: 0x06001C8D RID: 7309 RVA: 0x0009BCB4 File Offset: 0x00099EB4
		private ConstantModel GetConstantModel(FieldInfo fieldInfo)
		{
			if (fieldInfo.IsSpecialName)
			{
				return null;
			}
			return new ConstantModel(fieldInfo, ((IConvertible)fieldInfo.GetValue(null)).ToInt64(null));
		}

		// Token: 0x04001661 RID: 5729
		private ConstantModel[] constants;
	}
}
