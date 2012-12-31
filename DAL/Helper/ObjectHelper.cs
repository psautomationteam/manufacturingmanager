using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;

namespace DAL.Helper
{
    public class ObjectHelper
    {
        public static T GetValueFromAnonymousType<T>(object dataitem, string itemkey)
        {
            System.Type type = dataitem.GetType();
            T itemvalue = (T)type.GetProperty(itemkey).GetValue(dataitem, null);
            return itemvalue;
        }

        public static void ConvertObjectFrom<T1, T2>(T1 sourceObject, ref T2 destObject)
        {
            //	If either the source, or destination is null, return
            if (sourceObject == null || destObject == null)
                return;

            //	Get the type of each object
            Type sourceType = sourceObject.GetType();
            Type targetType = destObject.GetType();
            foreach (PropertyInfo p in sourceType.GetProperties())
            {
                try
                {
                    //	Get the matching property in the destination object
                    PropertyInfo targetObj = targetType.GetProperty(p.Name);
                    Type srcType = p.GetType();
                    Type type = targetObj.GetType();
                    String obj = (String)p.GetValue(sourceObject, null);


                    if (targetObj.PropertyType is String && p.PropertyType is String)
                    {

                        targetObj.SetValue(destObject, obj, null);

                    }
                    else
                    {

                        var converter = TypeDescriptor.GetConverter(targetObj.PropertyType);
                        if (converter != null)
                        {
                            Object parsedValue = converter.ConvertFromString(obj);
                            targetObj.SetValue(destObject, parsedValue, null);
                        }
                    }




                }
                catch (Exception)
                {
                    continue;
                }

            }
        }

        public static void ConvertObjectTo<T1, T2>(T1 sourceObject, ref T2 destObject)
        {
            //	If either the source, or destination is null, return
            if (sourceObject == null || destObject == null)
                return;

            //	Get the type of each object
            Type sourceType = sourceObject.GetType();
            Type targetType = destObject.GetType();
            foreach (PropertyInfo p in sourceType.GetProperties())
            {
                try
                {
                    //	Get the matching property in the destination object
                    PropertyInfo targetObj = targetType.GetProperty(p.Name);
                    Type srcType = p.GetType();
                    Type type = targetObj.GetType();
                    Object obj = p.GetValue(sourceObject, null);

                    if (targetObj.PropertyType is String && p.PropertyType is String)
                    {

                        targetObj.SetValue(destObject, obj, null);

                    }
                    else
                    {

                        var converter = TypeDescriptor.GetConverter(p.PropertyType);
                        if (converter != null)
                        {
                            String parsedValue = converter.ConvertToString(obj);
                            targetObj.SetValue(destObject, parsedValue, null);
                        }
                    }




                }
                catch (Exception)
                {
                    continue;
                }

            }
        }

        public static void CopyObject<T1, T2>(T1 sourceObject, ref T2 destObject)
        {
            //	If either the source, or destination is null, return
            if (sourceObject == null || destObject == null)
                return;

            //	Get the type of each object
            Type sourceType = sourceObject.GetType();
            Type targetType = destObject.GetType();

            //	Loop through the source properties
            foreach (PropertyInfo p in sourceType.GetProperties())
            {
                try
                {
                    //	Get the matching property in the destination object
                    PropertyInfo targetObj = targetType.GetProperty(p.Name);
                    //	If there is none, skip
                    if (targetObj == null)
                        continue;
                    object obj = p.GetValue(sourceObject, null);
                    if (obj != null)
                    {
                        if (obj.GetType() == typeof(long) && ((long)obj == long.MaxValue || (long)obj == long.MinValue))
                            continue;
                        if (obj.GetType() == typeof(double) && ((double)obj == double.MaxValue || (double)obj == double.MinValue))
                            continue;
                        //	Set the value in the destination
                        targetObj.SetValue(destObject, obj, null);
                    }

                }
                catch (Exception)
                {
                    continue;
                }

            }
        }

        public static void ConvertObjectListFrom<T1, T2>(List<T1> sourceObjectList, ref List<T2> destObjectList)
        {
            if (sourceObjectList == null || destObjectList == null)
                return;
            if (sourceObjectList == null || destObjectList == null)
                return;
            foreach (T1 obj in sourceObjectList)
            {
                try
                {
                    Type type = typeof(T2); //or "TaskB"
                    Type[] typeArgs = { typeof(T2) };
                    //Type makeme = type.MakeGenericType(typeArgs);
                    T2 t = (T2)Activator.CreateInstance(type);
                    ConvertObjectFrom<T1, T2>(obj, ref t);
                    destObjectList.Add(t);
                }
                catch (Exception)
                {
                    continue;
                }

            }
        }

        public static void ConvertObjectListTo<T1, T2>(List<T1> sourceObjectList, ref List<T2> destObjectList)
        {
            if (sourceObjectList == null || destObjectList == null)
                return;
            if (sourceObjectList == null || destObjectList == null)
                return;
            foreach (T1 obj in sourceObjectList)
            {
                try
                {
                    Type type = typeof(T2); //or "TaskB"
                    Type[] typeArgs = { typeof(T2) };
                    //Type makeme = type.MakeGenericType(typeArgs);
                    T2 t = (T2)Activator.CreateInstance(type);
                    ConvertObjectTo<T1, T2>(obj, ref t);
                    destObjectList.Add(t);
                }
                catch (Exception)
                {
                    continue;
                }

            }
        }

        public static void CopyObjectList<T1, T2>(List<T1> sourceObjectList, ref List<T2> destObjectList)
        {
            if (sourceObjectList == null || destObjectList == null)
                return;
            foreach (T1 obj in sourceObjectList)
            {
                try
                {
                    Type type = typeof(T2); //or "TaskB"
                    Type[] typeArgs = { typeof(T2) };
                    //Type makeme = type.MakeGenericType(typeArgs);
                    T2 t = (T2)Activator.CreateInstance(type);
                    CopyObject<T1, T2>(obj, ref t);
                    destObjectList.Add(t);
                }
                catch (Exception)
                {
                    continue;
                }

            }
        }
    }
}
