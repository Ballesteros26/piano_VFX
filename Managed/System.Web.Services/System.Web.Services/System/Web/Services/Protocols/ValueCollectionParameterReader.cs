using System;
using System.Collections.Specialized;
using System.Reflection;

namespace System.Web.Services.Protocols
{
	/// <summary>Serves as a base class for readers of incoming request parameters for Web services implemented using HTTP but without SOAP.</summary>
	// Token: 0x02000088 RID: 136
	public abstract class ValueCollectionParameterReader : MimeParameterReader
	{
		/// <summary>Initializes an instance.</summary>
		/// <param name="o">A <see cref="T:System.Reflection.ParameterInfo" /> array, obtained through the <see cref="P:System.Web.Services.Protocols.LogicalMethodInfo.InParameters" /> property of the <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> class.</param>
		// Token: 0x0600039B RID: 923 RVA: 0x00010FF9 File Offset: 0x0000F1F9
		public override void Initialize(object o)
		{
			this.paramInfos = (ParameterInfo[])o;
		}

		/// <summary>Returns an initializer for the specified method.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> representing the Web method.</returns>
		/// <param name="methodInfo">A <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> that specifies the Web method for which the initializer is obtained.</param>
		// Token: 0x0600039C RID: 924 RVA: 0x00010E8D File Offset: 0x0000F08D
		public override object GetInitializer(LogicalMethodInfo methodInfo)
		{
			if (!ValueCollectionParameterReader.IsSupported(methodInfo))
			{
				return null;
			}
			return methodInfo.InParameters;
		}

		/// <summary>Translates a collection of name/value pairs into an array of objects representing method parameter values.</summary>
		/// <returns>An array of <see cref="T:System.Object" /> objects representing method parameter values.</returns>
		/// <param name="collection">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> object that specifies the collection of name/value pairs containing method parameter names and values.</param>
		// Token: 0x0600039D RID: 925 RVA: 0x00011008 File Offset: 0x0000F208
		protected object[] Read(NameValueCollection collection)
		{
			object[] array = new object[this.paramInfos.Length];
			for (int i = 0; i < this.paramInfos.Length; i++)
			{
				ParameterInfo parameterInfo = this.paramInfos[i];
				if (parameterInfo.ParameterType.IsArray)
				{
					string[] values = collection.GetValues(parameterInfo.Name);
					Type elementType = parameterInfo.ParameterType.GetElementType();
					Array array2 = Array.CreateInstance(elementType, values.Length);
					for (int j = 0; j < values.Length; j++)
					{
						string text = values[j];
						array2.SetValue(ScalarFormatter.FromString(text, elementType), j);
					}
					array[i] = array2;
				}
				else
				{
					string text2 = collection[parameterInfo.Name];
					if (text2 == null)
					{
						throw new InvalidOperationException(Res.GetString("WebMissingParameter", new object[] { parameterInfo.Name }));
					}
					array[i] = ScalarFormatter.FromString(text2, parameterInfo.ParameterType);
				}
			}
			return array;
		}

		/// <summary>Determines whether a method definition's parameter definitions are supported by the <see cref="T:System.Web.Services.Protocols.ValueCollectionParameterReader" /> class.</summary>
		/// <returns>true if a method's parameter definitions are supported by the reader; otherwise, false.</returns>
		/// <param name="methodInfo">A <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> that specifies the method to check.</param>
		// Token: 0x0600039E RID: 926 RVA: 0x000110EC File Offset: 0x0000F2EC
		public static bool IsSupported(LogicalMethodInfo methodInfo)
		{
			if (methodInfo.OutParameters.Length != 0)
			{
				return false;
			}
			ParameterInfo[] inParameters = methodInfo.InParameters;
			for (int i = 0; i < inParameters.Length; i++)
			{
				if (!ValueCollectionParameterReader.IsSupported(inParameters[i]))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Determines whether a particular parameter type is supported by the <see cref="T:System.Web.Services.Protocols.ValueCollectionParameterReader" /> class.</summary>
		/// <returns>true if a method's parameter definitions are supported by the reader; otherwise, false.</returns>
		/// <param name="paramInfo">A <see cref="T:System.Reflection.ParameterInfo" /> that specifies the parameter to check.</param>
		// Token: 0x0600039F RID: 927 RVA: 0x00011128 File Offset: 0x0000F328
		public static bool IsSupported(ParameterInfo paramInfo)
		{
			Type type = paramInfo.ParameterType;
			if (type.IsArray)
			{
				type = type.GetElementType();
			}
			return ScalarFormatter.IsTypeSupported(type);
		}

		// Token: 0x04000305 RID: 773
		private ParameterInfo[] paramInfos;
	}
}
