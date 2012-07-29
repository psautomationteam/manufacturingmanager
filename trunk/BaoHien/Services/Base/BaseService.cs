using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Reflection;
using DAL.Helper;
using System.Linq.Expressions;
namespace BaoHien.Services.Base
{
    
    public class BaseService<TItem>
    {
        private Object retrieveObjectFromEntity<TItem>(TItem entity)
        {
            Object result = null;
            if (entity == null) return result;

            try
            {
                Type myTypeObj = entity.GetType();
                MethodInfo myMethodInfo = myTypeObj.GetMethod("GetData");
                result = myMethodInfo.Invoke(entity, null);
            }
            catch (Exception)
            {
            }

            return result;
        }
        protected List<TItem> OnGetItems<TItem>()
        {
            
            List<TItem> list = null;
            try
            {
                Type typeofClassWithGenericStaticMethod = typeof(BaoHienRepository);
                MethodInfo methodInfo = typeofClassWithGenericStaticMethod.GetMethod("SelectAll", System.Reflection.BindingFlags.Static | BindingFlags.Public);
                Type[] genericArguments = new Type[] { typeof(TItem) };
                MethodInfo genericMethodInfo = methodInfo.MakeGenericMethod(genericArguments);
                list = (List<TItem>)genericMethodInfo.Invoke(null, null);

                list = list.Where(item => !checkDeletedItems<TItem>(item)).ToList<TItem>();

            }
            catch (Exception)
            {

            }
            return list;
        }
        private Boolean checkDeletedItems<TItem>(TItem item)
        {
            Type type = typeof(TItem);
            if (type.GetProperties().Any(x => x.Name == Constant.DELETED_PROPERTY_NAME))
            {
                PropertyInfo property = type.GetProperty(Constant.DELETED_PROPERTY_NAME);
                Nullable<byte> deleted = (Nullable<byte>)property.GetValue(item, null);
                if (deleted.HasValue && deleted.Value == Constant.DELETED_PROPERTY_VALUE)
                {
                    return true;
                }

            }
            return false;
        }

        protected TItem OnGetItem<TItem>(string id)
        {
            List<string> ids = new List<string>();
            ids.Add(id);
            TItem item = default(TItem);
            try
            {
                Type typeofClassWithGenericStaticMethod = typeof(BaoHienRepository);
                MethodInfo methodInfo = typeofClassWithGenericStaticMethod.GetMethod("SelectByPK", System.Reflection.BindingFlags.Static | BindingFlags.Public);
                Type[] genericArguments = new Type[] { typeof(TItem) };
                MethodInfo genericMethodInfo = methodInfo.MakeGenericMethod(genericArguments);
                item = (TItem)genericMethodInfo.Invoke(null, new object[] { ids });
                


            }
            catch (Exception)
            {

            }
            return item;
        }
        protected TItem OnGetItem<TItem>(List<string> ids)
        {
           
            TItem item = default(TItem);
            try
            {
                Type typeofClassWithGenericStaticMethod = typeof(BaoHienRepository);
                MethodInfo methodInfo = typeofClassWithGenericStaticMethod.GetMethod("SelectByPK", System.Reflection.BindingFlags.Static | BindingFlags.Public);
                Type[] genericArguments = new Type[] { typeof(TItem) };
                MethodInfo genericMethodInfo = methodInfo.MakeGenericMethod(genericArguments);
                item = (TItem)genericMethodInfo.Invoke(null, new object[] { ids });



            }
            catch (Exception)
            {

            }
            return item;
        }

        protected bool OnAddItem<TItem>(TItem initialValue)
        {
            int id = int.MaxValue;
            try
            {
                Type typeofClassWithGenericStaticMethod = typeof(BaoHienRepository);
                MethodInfo methodInfo = typeofClassWithGenericStaticMethod.GetMethod("GetMaxId", System.Reflection.BindingFlags.Static | BindingFlags.Public);
                Type[] genericArguments = new Type[] { typeof(TItem) };
                MethodInfo genericMethodInfo = methodInfo.MakeGenericMethod(genericArguments);
                id = (int)genericMethodInfo.Invoke(null, null);
            }
            catch (Exception)
            {
            }

            if (id == int.MaxValue || id == int.MinValue)
            {
                return false;
            }


           
            try
            {
                Type type = typeof(TItem);
                //if (type.GetProperties().Any(x => x.Name == Constant.PRIMARYKEY_PROPERTY_NAME))
                //{
                    //PropertyInfo property = type.GetProperty(Constant.PRIMARYKEY_PROPERTY_NAME);
                    //property.SetValue(initialValue, id, null);
               // }

                //if (type.GetProperties().Any(x => x.Name == Constant.CREATED_DATE_PROPERTY_NAME))
                //{
                    //PropertyInfo property = type.GetProperty(Constant.CREATED_DATE_PROPERTY_NAME);
                    //property.SetValue(initialValue, DateTime.Now, null);
                //}


                Type typeofClassWithGenericStaticMethod = typeof(BaoHienRepository);

                MethodInfo methodInfo = typeofClassWithGenericStaticMethod.GetMethod("Insert", System.Reflection.BindingFlags.Static | BindingFlags.Public);
                Type[] genericArguments = new Type[] { typeof(TItem) };
                MethodInfo genericMethodInfo = methodInfo.MakeGenericMethod(genericArguments);
                genericMethodInfo.Invoke(null, new object[] { initialValue });
               

            }
            catch (Exception)
            {
                
                return false;
            }
            return true;
        }

        protected bool OnUpdateItem<TItem>(TItem newValue, string id)
        {
            
            try
            {



                Type typeofClassWithGenericStaticMethod = typeof(BaoHienRepository);
                MethodInfo methodInfo = typeofClassWithGenericStaticMethod.GetMethod("Update", System.Reflection.BindingFlags.Static | BindingFlags.Public);
                Type[] genericArguments = new Type[] { typeof(TItem) };
                MethodInfo genericMethodInfo = methodInfo.MakeGenericMethod(genericArguments);
                genericMethodInfo.Invoke(null, new object[] { newValue, id });
                
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }
        protected bool OnUpdateItem<TItem>(TItem newValue, List<string> ids)
        {

            try
            {



                Type typeofClassWithGenericStaticMethod = typeof(BaoHienRepository);
                MethodInfo methodInfo = typeofClassWithGenericStaticMethod.GetMethod("Update", System.Reflection.BindingFlags.Static | BindingFlags.Public);
                Type[] genericArguments = new Type[] { typeof(TItem) };
                MethodInfo genericMethodInfo = methodInfo.MakeGenericMethod(genericArguments);
                genericMethodInfo.Invoke(null, new object[] { newValue, ids });

            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }
        protected bool OnDeleteItem<TItem>(string id)
        {
            
            try
            {
                Type type = typeof(TItem);
                TItem item = (TItem)OnGetItem<TItem>(id);
                if (type.GetProperties().Any(x => x.Name == Constant.DELETED_PROPERTY_NAME))
                {
                    //soft delete
                    PropertyInfo property = type.GetProperty(Constant.DELETED_PROPERTY_NAME);
                    
                    if (type.GetProperties().Any(x => x.Name == Constant.DELETED_PROPERTY_NAME))
                    {
                        property = type.GetProperty(Constant.DELETED_PROPERTY_NAME);
                        property.SetValue(item, new Nullable<byte>(Constant.DELETED_PROPERTY_VALUE), null);
                    }
                    OnUpdateItem<TItem>(item, id);
                }
                else
                {
                    //hard delete
                    Type typeofClassWithGenericStaticMethod = typeof(BaoHienRepository);
                    MethodInfo methodInfo = typeofClassWithGenericStaticMethod.GetMethod("Remove", System.Reflection.BindingFlags.Static | BindingFlags.Public);
                    Type[] genericArguments = new Type[] { typeof(TItem) };
                    MethodInfo genericMethodInfo = methodInfo.MakeGenericMethod(genericArguments);
                    genericMethodInfo.Invoke(null, new object[] { item, id });
                    
                }


            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        protected bool OnDeleteItem<TItem>(List<string> ids)
        {

            try
            {
                Type type = typeof(TItem);
                TItem item = (TItem)OnGetItem<TItem>(ids);
                if (type.GetProperties().Any(x => x.Name == Constant.DELETED_PROPERTY_NAME))
                {
                    //soft delete
                    PropertyInfo property = type.GetProperty(Constant.DELETED_PROPERTY_NAME);

                    if (type.GetProperties().Any(x => x.Name == Constant.DELETED_PROPERTY_NAME))
                    {
                        property = type.GetProperty(Constant.DELETED_PROPERTY_NAME);
                        property.SetValue(item, new Nullable<byte>(Constant.DELETED_PROPERTY_VALUE), null);
                    }
                    OnUpdateItem<TItem>(item, ids);
                }
                else
                {
                    //hard delete
                    Type typeofClassWithGenericStaticMethod = typeof(BaoHienRepository);
                    MethodInfo methodInfo = typeofClassWithGenericStaticMethod.GetMethod("Remove", System.Reflection.BindingFlags.Static | BindingFlags.Public);
                    Type[] genericArguments = new Type[] { typeof(TItem) };
                    MethodInfo genericMethodInfo = methodInfo.MakeGenericMethod(genericArguments);
                    genericMethodInfo.Invoke(null, new object[] { item, "" });

                }


            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public List<TItem> SelectItemByWhere<TItem>(Expression<Func<TItem, bool>> func)
        {
            List<TItem> list = null;
            try
            {
                
                
                Type typeofClassWithGenericStaticMethod = typeof(BaoHienRepository);
                MethodInfo methodInfo = typeofClassWithGenericStaticMethod.GetMethod("SelectByWhere", System.Reflection.BindingFlags.Static | BindingFlags.Public);
                var expressionKey = typeof(Expression<>).MakeGenericType(typeof(Func<,>).MakeGenericType(typeof(TItem), typeof(bool)));

                Type[] genericArguments = new[] { typeof(TItem) };
                
                MethodInfo genericMethodInfo = methodInfo.MakeGenericMethod(genericArguments);
                list = (List<TItem>)genericMethodInfo.Invoke(null, new [] { func });
                
                list = list.Where(item => !checkDeletedItems<TItem>(item)).ToList<TItem>();

            }
            catch (Exception)
            {

            }
            return list;
        }
    }
}
