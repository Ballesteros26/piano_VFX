using System;
using System.Text;

namespace UnityEngine
{
	// Token: 0x02000007 RID: 7
	public class AndroidJavaObject : IDisposable
	{
		// Token: 0x06000018 RID: 24 RVA: 0x00002546 File Offset: 0x00000746
		public AndroidJavaObject(string className, string[] args)
			: this()
		{
			this._AndroidJavaObject(className, new object[] { args });
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002546 File Offset: 0x00000746
		public AndroidJavaObject(string className, AndroidJavaObject[] args)
			: this()
		{
			this._AndroidJavaObject(className, new object[] { args });
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002546 File Offset: 0x00000746
		public AndroidJavaObject(string className, AndroidJavaClass[] args)
			: this()
		{
			this._AndroidJavaObject(className, new object[] { args });
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002546 File Offset: 0x00000746
		public AndroidJavaObject(string className, AndroidJavaProxy[] args)
			: this()
		{
			this._AndroidJavaObject(className, new object[] { args });
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002546 File Offset: 0x00000746
		public AndroidJavaObject(string className, AndroidJavaRunnable[] args)
			: this()
		{
			this._AndroidJavaObject(className, new object[] { args });
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002562 File Offset: 0x00000762
		public AndroidJavaObject(string className, params object[] args)
			: this()
		{
			this._AndroidJavaObject(className, args);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002575 File Offset: 0x00000775
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002587 File Offset: 0x00000787
		public void Call<T>(string methodName, T[] args)
		{
			this._Call(methodName, new object[] { args });
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000259C File Offset: 0x0000079C
		public void Call(string methodName, params object[] args)
		{
			this._Call(methodName, args);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000025A8 File Offset: 0x000007A8
		public void CallStatic<T>(string methodName, T[] args)
		{
			this._CallStatic(methodName, new object[] { args });
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000025BD File Offset: 0x000007BD
		public void CallStatic(string methodName, params object[] args)
		{
			this._CallStatic(methodName, args);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000025CC File Offset: 0x000007CC
		public FieldType Get<FieldType>(string fieldName)
		{
			return this._Get<FieldType>(fieldName);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000025E5 File Offset: 0x000007E5
		public void Set<FieldType>(string fieldName, FieldType val)
		{
			this._Set<FieldType>(fieldName, val);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000025F4 File Offset: 0x000007F4
		public FieldType GetStatic<FieldType>(string fieldName)
		{
			return this._GetStatic<FieldType>(fieldName);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000260D File Offset: 0x0000080D
		public void SetStatic<FieldType>(string fieldName, FieldType val)
		{
			this._SetStatic<FieldType>(fieldName, val);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000261C File Offset: 0x0000081C
		public IntPtr GetRawObject()
		{
			return this._GetRawObject();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002634 File Offset: 0x00000834
		public IntPtr GetRawClass()
		{
			return this._GetRawClass();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000264C File Offset: 0x0000084C
		public ReturnType Call<ReturnType, T>(string methodName, T[] args)
		{
			return this._Call<ReturnType>(methodName, new object[] { args });
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002670 File Offset: 0x00000870
		public ReturnType Call<ReturnType>(string methodName, params object[] args)
		{
			return this._Call<ReturnType>(methodName, args);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000268C File Offset: 0x0000088C
		public ReturnType CallStatic<ReturnType, T>(string methodName, T[] args)
		{
			return this._CallStatic<ReturnType>(methodName, new object[] { args });
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000026B0 File Offset: 0x000008B0
		public ReturnType CallStatic<ReturnType>(string methodName, params object[] args)
		{
			return this._CallStatic<ReturnType>(methodName, args);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000026CC File Offset: 0x000008CC
		protected void DebugPrint(string msg)
		{
			bool flag = !AndroidJavaObject.enableDebugPrints;
			if (!flag)
			{
				Debug.Log(msg);
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000026F0 File Offset: 0x000008F0
		protected void DebugPrint(string call, string methodName, string signature, object[] args)
		{
			bool flag = !AndroidJavaObject.enableDebugPrints;
			if (!flag)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (object obj in args)
				{
					stringBuilder.Append(", ");
					stringBuilder.Append((obj == null) ? "<null>" : obj.GetType().ToString());
				}
				Debug.Log(string.Concat(new object[] { call, "(\"", methodName, "\"", stringBuilder, ") = ", signature }));
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002794 File Offset: 0x00000994
		private void _AndroidJavaObject(string className, params object[] args)
		{
			this.DebugPrint("Creating AndroidJavaObject from " + className);
			bool flag = args == null;
			if (flag)
			{
				args = new object[1];
			}
			IntPtr intPtr = AndroidJNISafe.FindClass(className.Replace('.', '/'));
			this.m_jclass = new GlobalJavaObjectRef(intPtr);
			jvalue[] array = AndroidJNIHelper.CreateJNIArgArray(args);
			try
			{
				IntPtr constructorID = AndroidJNIHelper.GetConstructorID(this.m_jclass, args);
				IntPtr intPtr2 = AndroidJNISafe.NewObject(this.m_jclass, constructorID, array);
				this.m_jobject = new GlobalJavaObjectRef(intPtr2);
				AndroidJNISafe.DeleteLocalRef(intPtr2);
			}
			finally
			{
				AndroidJNIHelper.DeleteJNIArgArray(args, array);
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002844 File Offset: 0x00000A44
		internal AndroidJavaObject(IntPtr jobject)
			: this()
		{
			bool flag = jobject == IntPtr.Zero;
			if (flag)
			{
				throw new Exception("JNI: Init'd AndroidJavaObject with null ptr!");
			}
			IntPtr objectClass = AndroidJNISafe.GetObjectClass(jobject);
			this.m_jobject = new GlobalJavaObjectRef(jobject);
			this.m_jclass = new GlobalJavaObjectRef(objectClass);
			AndroidJNISafe.DeleteLocalRef(objectClass);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000289A File Offset: 0x00000A9A
		internal AndroidJavaObject()
		{
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000028A4 File Offset: 0x00000AA4
		~AndroidJavaObject()
		{
			this.Dispose(true);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000028D8 File Offset: 0x00000AD8
		protected virtual void Dispose(bool disposing)
		{
			bool flag = this.m_jobject != null;
			if (flag)
			{
				this.m_jobject.Dispose();
				this.m_jobject = null;
			}
			bool flag2 = this.m_jclass != null;
			if (flag2)
			{
				this.m_jclass.Dispose();
				this.m_jclass = null;
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000292C File Offset: 0x00000B2C
		protected void _Call(string methodName, params object[] args)
		{
			bool flag = args == null;
			if (flag)
			{
				args = new object[1];
			}
			IntPtr methodID = AndroidJNIHelper.GetMethodID(this.m_jclass, methodName, args, false);
			jvalue[] array = AndroidJNIHelper.CreateJNIArgArray(args);
			try
			{
				AndroidJNISafe.CallVoidMethod(this.m_jobject, methodID, array);
			}
			finally
			{
				AndroidJNIHelper.DeleteJNIArgArray(args, array);
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002998 File Offset: 0x00000B98
		protected ReturnType _Call<ReturnType>(string methodName, params object[] args)
		{
			bool flag = args == null;
			if (flag)
			{
				args = new object[1];
			}
			IntPtr methodID = AndroidJNIHelper.GetMethodID<ReturnType>(this.m_jclass, methodName, args, false);
			jvalue[] array = AndroidJNIHelper.CreateJNIArgArray(args);
			ReturnType returnType;
			try
			{
				bool flag2 = AndroidReflection.IsPrimitive(typeof(ReturnType));
				if (flag2)
				{
					bool flag3 = typeof(ReturnType) == typeof(int);
					if (flag3)
					{
						returnType = (ReturnType)((object)AndroidJNISafe.CallIntMethod(this.m_jobject, methodID, array));
					}
					else
					{
						bool flag4 = typeof(ReturnType) == typeof(bool);
						if (flag4)
						{
							returnType = (ReturnType)((object)AndroidJNISafe.CallBooleanMethod(this.m_jobject, methodID, array));
						}
						else
						{
							bool flag5 = typeof(ReturnType) == typeof(byte);
							if (flag5)
							{
								Debug.LogWarning("Return type <Byte> for Java method call is obsolete, use return type <SByte> instead");
								returnType = (ReturnType)((object)((byte)AndroidJNISafe.CallSByteMethod(this.m_jobject, methodID, array)));
							}
							else
							{
								bool flag6 = typeof(ReturnType) == typeof(sbyte);
								if (flag6)
								{
									returnType = (ReturnType)((object)AndroidJNISafe.CallSByteMethod(this.m_jobject, methodID, array));
								}
								else
								{
									bool flag7 = typeof(ReturnType) == typeof(short);
									if (flag7)
									{
										returnType = (ReturnType)((object)AndroidJNISafe.CallShortMethod(this.m_jobject, methodID, array));
									}
									else
									{
										bool flag8 = typeof(ReturnType) == typeof(long);
										if (flag8)
										{
											returnType = (ReturnType)((object)AndroidJNISafe.CallLongMethod(this.m_jobject, methodID, array));
										}
										else
										{
											bool flag9 = typeof(ReturnType) == typeof(float);
											if (flag9)
											{
												returnType = (ReturnType)((object)AndroidJNISafe.CallFloatMethod(this.m_jobject, methodID, array));
											}
											else
											{
												bool flag10 = typeof(ReturnType) == typeof(double);
												if (flag10)
												{
													returnType = (ReturnType)((object)AndroidJNISafe.CallDoubleMethod(this.m_jobject, methodID, array));
												}
												else
												{
													bool flag11 = typeof(ReturnType) == typeof(char);
													if (flag11)
													{
														returnType = (ReturnType)((object)AndroidJNISafe.CallCharMethod(this.m_jobject, methodID, array));
													}
													else
													{
														returnType = default(ReturnType);
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
				else
				{
					bool flag12 = typeof(ReturnType) == typeof(string);
					if (flag12)
					{
						returnType = (ReturnType)((object)AndroidJNISafe.CallStringMethod(this.m_jobject, methodID, array));
					}
					else
					{
						bool flag13 = typeof(ReturnType) == typeof(AndroidJavaClass);
						if (flag13)
						{
							IntPtr intPtr = AndroidJNISafe.CallObjectMethod(this.m_jobject, methodID, array);
							returnType = ((intPtr == IntPtr.Zero) ? default(ReturnType) : ((ReturnType)((object)AndroidJavaObject.AndroidJavaClassDeleteLocalRef(intPtr))));
						}
						else
						{
							bool flag14 = typeof(ReturnType) == typeof(AndroidJavaObject);
							if (flag14)
							{
								IntPtr intPtr2 = AndroidJNISafe.CallObjectMethod(this.m_jobject, methodID, array);
								returnType = ((intPtr2 == IntPtr.Zero) ? default(ReturnType) : ((ReturnType)((object)AndroidJavaObject.AndroidJavaObjectDeleteLocalRef(intPtr2))));
							}
							else
							{
								bool flag15 = AndroidReflection.IsAssignableFrom(typeof(Array), typeof(ReturnType));
								if (!flag15)
								{
									throw new Exception("JNI: Unknown return type '" + typeof(ReturnType) + "'");
								}
								IntPtr intPtr3 = AndroidJNISafe.CallObjectMethod(this.m_jobject, methodID, array);
								returnType = ((intPtr3 == IntPtr.Zero) ? default(ReturnType) : ((ReturnType)((object)AndroidJNIHelper.ConvertFromJNIArray<ReturnType>(intPtr3))));
							}
						}
					}
				}
			}
			finally
			{
				AndroidJNIHelper.DeleteJNIArgArray(args, array);
			}
			return returnType;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002DE4 File Offset: 0x00000FE4
		protected FieldType _Get<FieldType>(string fieldName)
		{
			IntPtr fieldID = AndroidJNIHelper.GetFieldID<FieldType>(this.m_jclass, fieldName, false);
			bool flag = AndroidReflection.IsPrimitive(typeof(FieldType));
			FieldType fieldType;
			if (flag)
			{
				bool flag2 = typeof(FieldType) == typeof(int);
				if (flag2)
				{
					fieldType = (FieldType)((object)AndroidJNISafe.GetIntField(this.m_jobject, fieldID));
				}
				else
				{
					bool flag3 = typeof(FieldType) == typeof(bool);
					if (flag3)
					{
						fieldType = (FieldType)((object)AndroidJNISafe.GetBooleanField(this.m_jobject, fieldID));
					}
					else
					{
						bool flag4 = typeof(FieldType) == typeof(byte);
						if (flag4)
						{
							Debug.LogWarning("Field type <Byte> for Java get field call is obsolete, use field type <SByte> instead");
							fieldType = (FieldType)((object)((byte)AndroidJNISafe.GetSByteField(this.m_jobject, fieldID)));
						}
						else
						{
							bool flag5 = typeof(FieldType) == typeof(sbyte);
							if (flag5)
							{
								fieldType = (FieldType)((object)AndroidJNISafe.GetSByteField(this.m_jobject, fieldID));
							}
							else
							{
								bool flag6 = typeof(FieldType) == typeof(short);
								if (flag6)
								{
									fieldType = (FieldType)((object)AndroidJNISafe.GetShortField(this.m_jobject, fieldID));
								}
								else
								{
									bool flag7 = typeof(FieldType) == typeof(long);
									if (flag7)
									{
										fieldType = (FieldType)((object)AndroidJNISafe.GetLongField(this.m_jobject, fieldID));
									}
									else
									{
										bool flag8 = typeof(FieldType) == typeof(float);
										if (flag8)
										{
											fieldType = (FieldType)((object)AndroidJNISafe.GetFloatField(this.m_jobject, fieldID));
										}
										else
										{
											bool flag9 = typeof(FieldType) == typeof(double);
											if (flag9)
											{
												fieldType = (FieldType)((object)AndroidJNISafe.GetDoubleField(this.m_jobject, fieldID));
											}
											else
											{
												bool flag10 = typeof(FieldType) == typeof(char);
												if (flag10)
												{
													fieldType = (FieldType)((object)AndroidJNISafe.GetCharField(this.m_jobject, fieldID));
												}
												else
												{
													fieldType = default(FieldType);
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			else
			{
				bool flag11 = typeof(FieldType) == typeof(string);
				if (flag11)
				{
					fieldType = (FieldType)((object)AndroidJNISafe.GetStringField(this.m_jobject, fieldID));
				}
				else
				{
					bool flag12 = typeof(FieldType) == typeof(AndroidJavaClass);
					if (flag12)
					{
						IntPtr objectField = AndroidJNISafe.GetObjectField(this.m_jobject, fieldID);
						fieldType = ((objectField == IntPtr.Zero) ? default(FieldType) : ((FieldType)((object)AndroidJavaObject.AndroidJavaClassDeleteLocalRef(objectField))));
					}
					else
					{
						bool flag13 = typeof(FieldType) == typeof(AndroidJavaObject);
						if (flag13)
						{
							IntPtr objectField2 = AndroidJNISafe.GetObjectField(this.m_jobject, fieldID);
							fieldType = ((objectField2 == IntPtr.Zero) ? default(FieldType) : ((FieldType)((object)AndroidJavaObject.AndroidJavaObjectDeleteLocalRef(objectField2))));
						}
						else
						{
							bool flag14 = AndroidReflection.IsAssignableFrom(typeof(Array), typeof(FieldType));
							if (!flag14)
							{
								throw new Exception("JNI: Unknown field type '" + typeof(FieldType) + "'");
							}
							IntPtr objectField3 = AndroidJNISafe.GetObjectField(this.m_jobject, fieldID);
							fieldType = ((objectField3 == IntPtr.Zero) ? default(FieldType) : ((FieldType)((object)AndroidJNIHelper.ConvertFromJNIArray<FieldType>(objectField3))));
						}
					}
				}
			}
			return fieldType;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000031D4 File Offset: 0x000013D4
		protected void _Set<FieldType>(string fieldName, FieldType val)
		{
			IntPtr fieldID = AndroidJNIHelper.GetFieldID<FieldType>(this.m_jclass, fieldName, false);
			bool flag = AndroidReflection.IsPrimitive(typeof(FieldType));
			if (flag)
			{
				bool flag2 = typeof(FieldType) == typeof(int);
				if (flag2)
				{
					AndroidJNISafe.SetIntField(this.m_jobject, fieldID, (int)((object)val));
				}
				else
				{
					bool flag3 = typeof(FieldType) == typeof(bool);
					if (flag3)
					{
						AndroidJNISafe.SetBooleanField(this.m_jobject, fieldID, (bool)((object)val));
					}
					else
					{
						bool flag4 = typeof(FieldType) == typeof(byte);
						if (flag4)
						{
							Debug.LogWarning("Field type <Byte> for Java set field call is obsolete, use field type <SByte> instead");
							AndroidJNISafe.SetSByteField(this.m_jobject, fieldID, (sbyte)((byte)((object)val)));
						}
						else
						{
							bool flag5 = typeof(FieldType) == typeof(sbyte);
							if (flag5)
							{
								AndroidJNISafe.SetSByteField(this.m_jobject, fieldID, (sbyte)((object)val));
							}
							else
							{
								bool flag6 = typeof(FieldType) == typeof(short);
								if (flag6)
								{
									AndroidJNISafe.SetShortField(this.m_jobject, fieldID, (short)((object)val));
								}
								else
								{
									bool flag7 = typeof(FieldType) == typeof(long);
									if (flag7)
									{
										AndroidJNISafe.SetLongField(this.m_jobject, fieldID, (long)((object)val));
									}
									else
									{
										bool flag8 = typeof(FieldType) == typeof(float);
										if (flag8)
										{
											AndroidJNISafe.SetFloatField(this.m_jobject, fieldID, (float)((object)val));
										}
										else
										{
											bool flag9 = typeof(FieldType) == typeof(double);
											if (flag9)
											{
												AndroidJNISafe.SetDoubleField(this.m_jobject, fieldID, (double)((object)val));
											}
											else
											{
												bool flag10 = typeof(FieldType) == typeof(char);
												if (flag10)
												{
													AndroidJNISafe.SetCharField(this.m_jobject, fieldID, (char)((object)val));
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			else
			{
				bool flag11 = typeof(FieldType) == typeof(string);
				if (flag11)
				{
					AndroidJNISafe.SetStringField(this.m_jobject, fieldID, (string)((object)val));
				}
				else
				{
					bool flag12 = typeof(FieldType) == typeof(AndroidJavaClass);
					if (flag12)
					{
						AndroidJNISafe.SetObjectField(this.m_jobject, fieldID, (val == null) ? IntPtr.Zero : ((AndroidJavaClass)((object)val)).m_jclass);
					}
					else
					{
						bool flag13 = typeof(FieldType) == typeof(AndroidJavaObject);
						if (flag13)
						{
							AndroidJNISafe.SetObjectField(this.m_jobject, fieldID, (val == null) ? IntPtr.Zero : ((AndroidJavaObject)((object)val)).m_jobject);
						}
						else
						{
							bool flag14 = AndroidReflection.IsAssignableFrom(typeof(Array), typeof(FieldType));
							if (!flag14)
							{
								throw new Exception("JNI: Unknown field type '" + typeof(FieldType) + "'");
							}
							IntPtr intPtr = AndroidJNIHelper.ConvertToJNIArray((Array)((object)val));
							AndroidJNISafe.SetObjectField(this.m_jclass, fieldID, intPtr);
						}
					}
				}
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003598 File Offset: 0x00001798
		protected void _CallStatic(string methodName, params object[] args)
		{
			bool flag = args == null;
			if (flag)
			{
				args = new object[1];
			}
			IntPtr methodID = AndroidJNIHelper.GetMethodID(this.m_jclass, methodName, args, true);
			jvalue[] array = AndroidJNIHelper.CreateJNIArgArray(args);
			try
			{
				AndroidJNISafe.CallStaticVoidMethod(this.m_jclass, methodID, array);
			}
			finally
			{
				AndroidJNIHelper.DeleteJNIArgArray(args, array);
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003604 File Offset: 0x00001804
		protected ReturnType _CallStatic<ReturnType>(string methodName, params object[] args)
		{
			bool flag = args == null;
			if (flag)
			{
				args = new object[1];
			}
			IntPtr methodID = AndroidJNIHelper.GetMethodID<ReturnType>(this.m_jclass, methodName, args, true);
			jvalue[] array = AndroidJNIHelper.CreateJNIArgArray(args);
			ReturnType returnType;
			try
			{
				bool flag2 = AndroidReflection.IsPrimitive(typeof(ReturnType));
				if (flag2)
				{
					bool flag3 = typeof(ReturnType) == typeof(int);
					if (flag3)
					{
						returnType = (ReturnType)((object)AndroidJNISafe.CallStaticIntMethod(this.m_jclass, methodID, array));
					}
					else
					{
						bool flag4 = typeof(ReturnType) == typeof(bool);
						if (flag4)
						{
							returnType = (ReturnType)((object)AndroidJNISafe.CallStaticBooleanMethod(this.m_jclass, methodID, array));
						}
						else
						{
							bool flag5 = typeof(ReturnType) == typeof(byte);
							if (flag5)
							{
								Debug.LogWarning("Return type <Byte> for Java method call is obsolete, use return type <SByte> instead");
								returnType = (ReturnType)((object)((byte)AndroidJNISafe.CallStaticSByteMethod(this.m_jclass, methodID, array)));
							}
							else
							{
								bool flag6 = typeof(ReturnType) == typeof(sbyte);
								if (flag6)
								{
									returnType = (ReturnType)((object)AndroidJNISafe.CallStaticSByteMethod(this.m_jclass, methodID, array));
								}
								else
								{
									bool flag7 = typeof(ReturnType) == typeof(short);
									if (flag7)
									{
										returnType = (ReturnType)((object)AndroidJNISafe.CallStaticShortMethod(this.m_jclass, methodID, array));
									}
									else
									{
										bool flag8 = typeof(ReturnType) == typeof(long);
										if (flag8)
										{
											returnType = (ReturnType)((object)AndroidJNISafe.CallStaticLongMethod(this.m_jclass, methodID, array));
										}
										else
										{
											bool flag9 = typeof(ReturnType) == typeof(float);
											if (flag9)
											{
												returnType = (ReturnType)((object)AndroidJNISafe.CallStaticFloatMethod(this.m_jclass, methodID, array));
											}
											else
											{
												bool flag10 = typeof(ReturnType) == typeof(double);
												if (flag10)
												{
													returnType = (ReturnType)((object)AndroidJNISafe.CallStaticDoubleMethod(this.m_jclass, methodID, array));
												}
												else
												{
													bool flag11 = typeof(ReturnType) == typeof(char);
													if (flag11)
													{
														returnType = (ReturnType)((object)AndroidJNISafe.CallStaticCharMethod(this.m_jclass, methodID, array));
													}
													else
													{
														returnType = default(ReturnType);
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
				else
				{
					bool flag12 = typeof(ReturnType) == typeof(string);
					if (flag12)
					{
						returnType = (ReturnType)((object)AndroidJNISafe.CallStaticStringMethod(this.m_jclass, methodID, array));
					}
					else
					{
						bool flag13 = typeof(ReturnType) == typeof(AndroidJavaClass);
						if (flag13)
						{
							IntPtr intPtr = AndroidJNISafe.CallStaticObjectMethod(this.m_jclass, methodID, array);
							returnType = ((intPtr == IntPtr.Zero) ? default(ReturnType) : ((ReturnType)((object)AndroidJavaObject.AndroidJavaClassDeleteLocalRef(intPtr))));
						}
						else
						{
							bool flag14 = typeof(ReturnType) == typeof(AndroidJavaObject);
							if (flag14)
							{
								IntPtr intPtr2 = AndroidJNISafe.CallStaticObjectMethod(this.m_jclass, methodID, array);
								returnType = ((intPtr2 == IntPtr.Zero) ? default(ReturnType) : ((ReturnType)((object)AndroidJavaObject.AndroidJavaObjectDeleteLocalRef(intPtr2))));
							}
							else
							{
								bool flag15 = AndroidReflection.IsAssignableFrom(typeof(Array), typeof(ReturnType));
								if (!flag15)
								{
									throw new Exception("JNI: Unknown return type '" + typeof(ReturnType) + "'");
								}
								IntPtr intPtr3 = AndroidJNISafe.CallStaticObjectMethod(this.m_jclass, methodID, array);
								returnType = ((intPtr3 == IntPtr.Zero) ? default(ReturnType) : ((ReturnType)((object)AndroidJNIHelper.ConvertFromJNIArray<ReturnType>(intPtr3))));
							}
						}
					}
				}
			}
			finally
			{
				AndroidJNIHelper.DeleteJNIArgArray(args, array);
			}
			return returnType;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003A50 File Offset: 0x00001C50
		protected FieldType _GetStatic<FieldType>(string fieldName)
		{
			IntPtr fieldID = AndroidJNIHelper.GetFieldID<FieldType>(this.m_jclass, fieldName, true);
			bool flag = AndroidReflection.IsPrimitive(typeof(FieldType));
			FieldType fieldType;
			if (flag)
			{
				bool flag2 = typeof(FieldType) == typeof(int);
				if (flag2)
				{
					fieldType = (FieldType)((object)AndroidJNISafe.GetStaticIntField(this.m_jclass, fieldID));
				}
				else
				{
					bool flag3 = typeof(FieldType) == typeof(bool);
					if (flag3)
					{
						fieldType = (FieldType)((object)AndroidJNISafe.GetStaticBooleanField(this.m_jclass, fieldID));
					}
					else
					{
						bool flag4 = typeof(FieldType) == typeof(byte);
						if (flag4)
						{
							Debug.LogWarning("Field type <Byte> for Java get field call is obsolete, use field type <SByte> instead");
							fieldType = (FieldType)((object)((byte)AndroidJNISafe.GetStaticSByteField(this.m_jclass, fieldID)));
						}
						else
						{
							bool flag5 = typeof(FieldType) == typeof(sbyte);
							if (flag5)
							{
								fieldType = (FieldType)((object)AndroidJNISafe.GetStaticSByteField(this.m_jclass, fieldID));
							}
							else
							{
								bool flag6 = typeof(FieldType) == typeof(short);
								if (flag6)
								{
									fieldType = (FieldType)((object)AndroidJNISafe.GetStaticShortField(this.m_jclass, fieldID));
								}
								else
								{
									bool flag7 = typeof(FieldType) == typeof(long);
									if (flag7)
									{
										fieldType = (FieldType)((object)AndroidJNISafe.GetStaticLongField(this.m_jclass, fieldID));
									}
									else
									{
										bool flag8 = typeof(FieldType) == typeof(float);
										if (flag8)
										{
											fieldType = (FieldType)((object)AndroidJNISafe.GetStaticFloatField(this.m_jclass, fieldID));
										}
										else
										{
											bool flag9 = typeof(FieldType) == typeof(double);
											if (flag9)
											{
												fieldType = (FieldType)((object)AndroidJNISafe.GetStaticDoubleField(this.m_jclass, fieldID));
											}
											else
											{
												bool flag10 = typeof(FieldType) == typeof(char);
												if (flag10)
												{
													fieldType = (FieldType)((object)AndroidJNISafe.GetStaticCharField(this.m_jclass, fieldID));
												}
												else
												{
													fieldType = default(FieldType);
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			else
			{
				bool flag11 = typeof(FieldType) == typeof(string);
				if (flag11)
				{
					fieldType = (FieldType)((object)AndroidJNISafe.GetStaticStringField(this.m_jclass, fieldID));
				}
				else
				{
					bool flag12 = typeof(FieldType) == typeof(AndroidJavaClass);
					if (flag12)
					{
						IntPtr staticObjectField = AndroidJNISafe.GetStaticObjectField(this.m_jclass, fieldID);
						fieldType = ((staticObjectField == IntPtr.Zero) ? default(FieldType) : ((FieldType)((object)AndroidJavaObject.AndroidJavaClassDeleteLocalRef(staticObjectField))));
					}
					else
					{
						bool flag13 = typeof(FieldType) == typeof(AndroidJavaObject);
						if (flag13)
						{
							IntPtr staticObjectField2 = AndroidJNISafe.GetStaticObjectField(this.m_jclass, fieldID);
							fieldType = ((staticObjectField2 == IntPtr.Zero) ? default(FieldType) : ((FieldType)((object)AndroidJavaObject.AndroidJavaObjectDeleteLocalRef(staticObjectField2))));
						}
						else
						{
							bool flag14 = AndroidReflection.IsAssignableFrom(typeof(Array), typeof(FieldType));
							if (!flag14)
							{
								throw new Exception("JNI: Unknown field type '" + typeof(FieldType) + "'");
							}
							IntPtr staticObjectField3 = AndroidJNISafe.GetStaticObjectField(this.m_jclass, fieldID);
							fieldType = ((staticObjectField3 == IntPtr.Zero) ? default(FieldType) : ((FieldType)((object)AndroidJNIHelper.ConvertFromJNIArray<FieldType>(staticObjectField3))));
						}
					}
				}
			}
			return fieldType;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003E40 File Offset: 0x00002040
		protected void _SetStatic<FieldType>(string fieldName, FieldType val)
		{
			IntPtr fieldID = AndroidJNIHelper.GetFieldID<FieldType>(this.m_jclass, fieldName, true);
			bool flag = AndroidReflection.IsPrimitive(typeof(FieldType));
			if (flag)
			{
				bool flag2 = typeof(FieldType) == typeof(int);
				if (flag2)
				{
					AndroidJNISafe.SetStaticIntField(this.m_jclass, fieldID, (int)((object)val));
				}
				else
				{
					bool flag3 = typeof(FieldType) == typeof(bool);
					if (flag3)
					{
						AndroidJNISafe.SetStaticBooleanField(this.m_jclass, fieldID, (bool)((object)val));
					}
					else
					{
						bool flag4 = typeof(FieldType) == typeof(byte);
						if (flag4)
						{
							Debug.LogWarning("Field type <Byte> for Java set field call is obsolete, use field type <SByte> instead");
							AndroidJNISafe.SetStaticSByteField(this.m_jclass, fieldID, (sbyte)((byte)((object)val)));
						}
						else
						{
							bool flag5 = typeof(FieldType) == typeof(sbyte);
							if (flag5)
							{
								AndroidJNISafe.SetStaticSByteField(this.m_jclass, fieldID, (sbyte)((object)val));
							}
							else
							{
								bool flag6 = typeof(FieldType) == typeof(short);
								if (flag6)
								{
									AndroidJNISafe.SetStaticShortField(this.m_jclass, fieldID, (short)((object)val));
								}
								else
								{
									bool flag7 = typeof(FieldType) == typeof(long);
									if (flag7)
									{
										AndroidJNISafe.SetStaticLongField(this.m_jclass, fieldID, (long)((object)val));
									}
									else
									{
										bool flag8 = typeof(FieldType) == typeof(float);
										if (flag8)
										{
											AndroidJNISafe.SetStaticFloatField(this.m_jclass, fieldID, (float)((object)val));
										}
										else
										{
											bool flag9 = typeof(FieldType) == typeof(double);
											if (flag9)
											{
												AndroidJNISafe.SetStaticDoubleField(this.m_jclass, fieldID, (double)((object)val));
											}
											else
											{
												bool flag10 = typeof(FieldType) == typeof(char);
												if (flag10)
												{
													AndroidJNISafe.SetStaticCharField(this.m_jclass, fieldID, (char)((object)val));
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			else
			{
				bool flag11 = typeof(FieldType) == typeof(string);
				if (flag11)
				{
					AndroidJNISafe.SetStaticStringField(this.m_jclass, fieldID, (string)((object)val));
				}
				else
				{
					bool flag12 = typeof(FieldType) == typeof(AndroidJavaClass);
					if (flag12)
					{
						AndroidJNISafe.SetStaticObjectField(this.m_jclass, fieldID, (val == null) ? IntPtr.Zero : ((AndroidJavaClass)((object)val)).m_jclass);
					}
					else
					{
						bool flag13 = typeof(FieldType) == typeof(AndroidJavaObject);
						if (flag13)
						{
							AndroidJNISafe.SetStaticObjectField(this.m_jclass, fieldID, (val == null) ? IntPtr.Zero : ((AndroidJavaObject)((object)val)).m_jobject);
						}
						else
						{
							bool flag14 = AndroidReflection.IsAssignableFrom(typeof(Array), typeof(FieldType));
							if (!flag14)
							{
								throw new Exception("JNI: Unknown field type '" + typeof(FieldType) + "'");
							}
							IntPtr intPtr = AndroidJNIHelper.ConvertToJNIArray((Array)((object)val));
							AndroidJNISafe.SetStaticObjectField(this.m_jclass, fieldID, intPtr);
						}
					}
				}
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00004204 File Offset: 0x00002404
		internal static AndroidJavaObject AndroidJavaObjectDeleteLocalRef(IntPtr jobject)
		{
			AndroidJavaObject androidJavaObject;
			try
			{
				androidJavaObject = new AndroidJavaObject(jobject);
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(jobject);
			}
			return androidJavaObject;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00004238 File Offset: 0x00002438
		internal static AndroidJavaClass AndroidJavaClassDeleteLocalRef(IntPtr jclass)
		{
			AndroidJavaClass androidJavaClass;
			try
			{
				androidJavaClass = new AndroidJavaClass(jclass);
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(jclass);
			}
			return androidJavaClass;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000426C File Offset: 0x0000246C
		protected IntPtr _GetRawObject()
		{
			return this.m_jobject;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000428C File Offset: 0x0000248C
		protected IntPtr _GetRawClass()
		{
			return this.m_jclass;
		}

		// Token: 0x04000009 RID: 9
		private static bool enableDebugPrints = false;

		// Token: 0x0400000A RID: 10
		internal GlobalJavaObjectRef m_jobject;

		// Token: 0x0400000B RID: 11
		internal GlobalJavaObjectRef m_jclass;
	}
}
