using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using BaoHien.Services.Base;
using System.Linq.Expressions;

namespace BaoHien.Services.SystemUsers
{
    public class SystemUserService : BaseService<SystemUser>
    {
        public SystemUser GetSystemUser(System.Int32 id)
        {
            SystemUser systemUser = OnGetItem<SystemUser>(id.ToString());

            return systemUser;
        }
        public List<SystemUser> GetSystemUsers()
        {
            List<SystemUser> systemUsers = OnGetItems<SystemUser>();

            return systemUsers;
        }
        public bool AddPSystemUser(SystemUser systemUser)
        {
            return OnAddItem<SystemUser>(systemUser);
        }
        public bool DeleteSystemUser(System.Int32 id)
        {
            return OnDeleteItem<SystemUser>(id.ToString());
        }
        public bool UpdateSystemUser(SystemUser systemUser)
        {
            return OnUpdateItem<SystemUser>(systemUser, systemUser.Id.ToString());
        }
        public List<SystemUser> SelectSystemUserByWhere(Expression<Func<SystemUser, bool>> func)
        {

            return SelectItemByWhere<SystemUser>(func);
        }
    }
}
