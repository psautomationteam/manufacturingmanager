using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using DAL;
using System.Reflection;
using System.Data.Linq.Mapping;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;

namespace DAL.Helper
{
    public class BaoHienRepository
    {
        //static string baseUri = "http://maps.googleapis.com/maps/api/" +
        //                  "geocode/xml?latlng={0},{1}&sensor=false";

        //public static string addr;
        private static BaoHienDBDataContext context = null;

        public static void ResetDBDataContext()
        {
            context.Connection.Close();
            context = null;
            context = GetBaoHienDBDataContext();
        }

        public static BaoHienDBDataContext GetBaoHienDBDataContext()
        {
            if (checkExistingContext())
            {
                return context;
            }
            return null;
        }

        static bool checkExistingContext()
        {
            if (context == null)
            {
                context = new BaoHienDBDataContext(SettingManager.BuildStringConnection());
                
            }
            if (context != null)
            {
                if (context.Connection != null)
                {
                    if (!context.DatabaseExists())
                    {
                        context = null;
                        return false;
                    }
                    else
                    {
                        return true;
                    }

                }
                else
                {
                    context = null;
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static bool testCurrentDBConnection()
        {
            return checkExistingContext();
        }

        public static bool testDBConnection(string DBServerName, string Port, string NetworkLibrary, string DatabaseName,
            string DatabaseUserID, string DatabasePwd)
        {
            BaoHienDBDataContext contextForTest = null;
            try
            {
                contextForTest = new BaoHienDBDataContext(SettingManager.BuildStringConnectionForTest(DBServerName, Port, NetworkLibrary,
                    DatabaseName, DatabaseUserID, DatabasePwd));
                if (contextForTest != null)
                {
                    if (contextForTest.Connection != null)
                    {
                        if (contextForTest.DatabaseExists())
                        {
                            contextForTest.Connection.Close();
                            return true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                context = null;
            }
            return false;
        }

        static Expression<Func<T, T2>> CreateLambdaForDeletedField<T, T2>()
        {
            var param = Expression.Parameter(typeof(T), "item");

            var expression = Expression.Lambda<Func<T, T2>>(Expression.Convert(Expression.Property(param, Constant.DELETED_PROPERTY_NAME), typeof(T2)), param);
            return expression;
        }

        static Expression<Func<T, T2>> CreateLambdaForIdField<T, T2>()
        {
            var param = Expression.Parameter(typeof(T), "item");

            var expression = Expression.Lambda<Func<T, T2>>(Expression.Convert(Expression.Property(param, Constant.PRIMARYKEY_PROPERTY_NAME), typeof(T2)), param);

            return expression;
        }

        public static int GetMaxId<T>() where T : class
        {

            int value = int.MaxValue;
            try
            {
                if (checkExistingContext())
                {
                    // get the table matching the type
                    // and return all of it as typed list
                    T objectWithMaxId = context.GetTable<T>().OrderByDescending(CreateLambdaForIdField<T, long>()).FirstOrDefault();
                    Type type = typeof(T);
                    if (objectWithMaxId != null && type.GetProperties().Any(x => x.Name == Constant.PRIMARYKEY_PROPERTY_NAME))
                    {
                        PropertyInfo property = type.GetProperty(Constant.PRIMARYKEY_PROPERTY_NAME);
                        value = (int)property.GetValue(objectWithMaxId, null);

                    }
                    if (value == int.MaxValue) value = 0;
                    
                    return value;
                }else
                    return value;
            }
            catch (Exception)
            {
                if (value == int.MaxValue) value = 0;
                else value++;
                return value;
            }

        }

        public static IList<T> SelectByWhere<T>(Expression<Func<T, bool>> func) where T : class
        {
            try
            {
                if (checkExistingContext())
                {
                    var table = context.GetTable<T>().Where(func).ToList<T>();
                    return table;
                }
                else
                    throw new Exception();
            }
            catch (Exception)
            {
                return null;
            }


        }

        public static T SelectByPK<T>(List<String> ids) where T : class
        {
            try
            {
                if (checkExistingContext())
                {
                    // get the table by the type passed in
                    var table = context.GetTable<T>();

                    // get the metamodel mappings (database to
                    // domain objects)
                    MetaModel modelMap = table.Context.Mapping;

                    // get the data members for this type
                    ReadOnlyCollection<MetaDataMember> dataMembers = modelMap.GetMetaType(typeof(T)).DataMembers;

                    // find the primary key field name
                    // by checking for IsPrimaryKey
                    List<string> pks = new List<string>();
                    foreach (string partKey in ids)
                    {
                        List<MetaDataMember> metaDataPKs = dataMembers.Where<MetaDataMember>(m => m.IsPrimaryKey).Distinct().ToList(); ;
                        
                        if (metaDataPKs != null)
                        {
                            foreach (MetaDataMember mdm in metaDataPKs)
                            {
                                if (!pks.Contains(mdm.Name))
                                {
                                    pks.Add(mdm.Name);
                                }
                                
                            }
                        }
                        
                    }
                    

                    // return a single object where the id argument
                    // matches the primary key field value
                    
                    return table.SingleOrDefault<T>(delegate(T t)
                    {
                        
                        List<string> memberIds = new List<string>();
                        foreach (string partKey in pks)
                        {
                            String memberId = t.GetType().GetProperty(partKey).GetValue(t, null).ToString();
                            memberIds.Add(memberId);
                            
                            
                        }
                        int index = 0;
                        bool isKey = true;
                        foreach (string memberId in memberIds)
                        {
                            if (!(memberId.ToString() == ids[index++].ToString()))
                            {
                                isKey = false;
                                break;
                            }
                        }
                        return isKey;
                    });
                }
                else
                    throw new Exception();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static IList<T> SelectAll<T>() where T : class
        {
            try
            {
                if (checkExistingContext())
                {

                    // get the table matching the type
                    // and return all of it as typed list
                    var table = context.GetTable<T>().ToList<T>();
                    return table;
                }
                else
                    throw new Exception();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static Type GetPrimaryKeyType<T>() where T : class
        {
            try
            {
                if (checkExistingContext())
                {
                    // get the table by the type passed in
                    var table = context.GetTable<T>();

                    // get the metamodel mappings (database to
                    // domain objects)
                    MetaModel modelMapping = table.Context.Mapping;

                    // get the data members for this type                   
                    ReadOnlyCollection<MetaDataMember> dataMembers
                    = modelMapping.GetMetaType(typeof(T))
                    .DataMembers;

                    // find the primary key and return its type
                    return (dataMembers.Single<MetaDataMember>(m =>
                    m.IsPrimaryKey)).Type;
                }
                else
                    throw new Exception();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string GetPrimaryKeyName<T>() where T : class
        {
            try
            {
                if (checkExistingContext())
                {
                    // get the table by the type passed in
                    var table = context.GetTable<T>();

                    // get the metamodel mappings (database to
                    // domain objects)
                    MetaModel modelMapping = table.Context.Mapping;

                    // get the data members for this type
                    ReadOnlyCollection<MetaDataMember> dataMembers
                    = modelMapping.GetMetaType(typeof(T))
                    .DataMembers;

                    // find the primary key field and return its
                    // name
                    return (dataMembers.Single<MetaDataMember>(m
                    => m.IsPrimaryKey)).Name;
                }
                else
                    throw new Exception();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void Insert<T>(T item) where T : class
        {
            try
            {
                // get the approapriate table by type and then
                // insert the passed in object and submit
                // that change to the data context
                if (checkExistingContext())
                {
                    // get the table by the type passed in
                    var table = context.GetTable<T>();

                    // pass in the object with insert on submit
                    // and then submit changes
                    table.InsertOnSubmit(item);
                    context.SubmitChanges();
                }
                else
                    throw new Exception();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void Update<T>(T item, string id) where T : class
        {
            try
            {
                context.SubmitChanges();
                //Type type = typeof(T);

                ////if (item != null && type.GetProperties().Any(x => x.Name == Constant.DELETED_PROPERTY_NAME))
                ////{
                ////    T origilItem = SelectByPK<T>(id);
                ////    PropertyInfo property = type.GetProperty(Constant.DELETED_PROPERTY_NAME);
                ////    bool deleted = (bool)property.GetValue(origilItem, null);
                ////    if (deleted) return;

                ////}
                //if (item == null || id == null || id == "") return;
                //// create a new instance of the object
                //Object newObj = Activator.CreateInstance(typeof(T), new object[0]);

                //PropertyDescriptorCollection originalProps = TypeDescriptor.GetProperties(item);

                //// set the new object to match the passed in object
                //foreach (PropertyDescriptor currentProp in originalProps)
                //{
                //    if (currentProp.Attributes[typeof(System.Data.Linq.Mapping.ColumnAttribute)] != null)
                //    {
                //        object val = currentProp.GetValue(item);
                //        currentProp.SetValue(newObj, val);
                //    }
                //}

                //// attach the new object to a new data context and
                //// call submit changes on the context
                //T originObject = SelectByPK<T>(id);
                //if (checkExistingContext())
                //{
                //    var table = context.GetTable<T>();
                //    table.Attach((T)newObj, originObject);
                //    //table.

                //    context.SubmitChanges();
                //}else
                //    throw new Exception();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void Remove<T>(T item, string id) where T : class
        {
            try
            {
                /*
                Type tType = item.GetType();
                Object newObj = Activator.CreateInstance(tType, new object[0]);

                // get the properties for the passed in object
                PropertyDescriptorCollection originalProps = TypeDescriptor.GetProperties(item);


                foreach (PropertyDescriptor currentProp in originalProps)
                {
                    if (currentProp.Attributes[typeof(System.Data.Linq.Mapping.ColumnAttribute)] != null)
                    {
                        object val = currentProp.GetValue(item);
                        currentProp.SetValue(newObj, val);
                    }
                }

                T originObject = SelectByPK<T>(id);*/
                if (checkExistingContext())
                {
                    var table = context.GetTable<T>();
                    //table.Attach((T)newObj, originObject);
                    //table.DeleteOnSubmit((T)newObj);
                    table.DeleteOnSubmit(item);
                    context.SubmitChanges();
                }
                else
                    throw new Exception();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
