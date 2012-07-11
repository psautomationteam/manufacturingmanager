using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using BaoHien.Services.Base;

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
        public bool AddPSystemUser(SystemUser product)
        {
            return OnAddItem<SystemUser>(product);
        }
        public bool DeleteSystemUser(System.Int32 id)
        {
            return OnDeleteItem<SystemUser>(id.ToString());
        }
        public bool UpdateSystemUser(SystemUser systemUser)
        {
            return OnUpdateItem<SystemUser>(systemUser, systemUser.Id.ToString());
        }
    }
}
