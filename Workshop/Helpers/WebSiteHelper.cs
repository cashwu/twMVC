using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Workshop.Models;

namespace Workshop.Helpers
{
    public class WebSiteHelper
    {
        /// <summary>
        /// Gets the name of the system user.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static string SystemUserName(Object id)
        {
            Guid systemUserID;
            if (Guid.TryParse(id.ToString(), out systemUserID))
            {
                if (systemUserID.Equals(Guid.Empty))
                {
                    return "系統預設";
                }
                else
                {
                    using (WorkshopEntities db = new WorkshopEntities())
                    {
                        var user = db.SystemUser.FirstOrDefault(x => x.ID == systemUserID);
                        return (user != null) ? user.Name : "";
                    }
                }
            }
            return "";
        }
    }
}