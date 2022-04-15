using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using MVC_EF_SQLSERVE.DAL;

namespace MVC_EF_SQLSERVE.UTIL
{
    public class Common
    {
        private SalesContext db = new SalesContext();
        public void UpdateFilter(int id)
        {
            Models.Filter filter = new Models.Filter();
            filter.CustomerID = id;
            if (db.Filter.ToList().Any())
            {
                filter.FilterID = db.Filter.FirstOrDefault().FilterID;
                if (!db.Filter.ToList().Any(r => r.CustomerID == filter.CustomerID))
                {
                    RemoveHoldingEntityInContext(filter);
                    db.Entry(filter).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            else
            {
                db.Filter.Add(filter);
                db.SaveChanges();
            }
        }

        private bool RemoveHoldingEntityInContext(Models.Filter entity)
        {
            ObjectContext objContext = ((IObjectContextAdapter)db).ObjectContext;
            var objSet = objContext.CreateObjectSet<Models.Filter>();
            var entityKey = objContext.CreateEntityKey(objSet.EntitySet.Name, entity);
            object foundEntity;
            var exists = objContext.TryGetObjectByKey(entityKey, out foundEntity);
            if (exists)
            {
                objContext.Detach(foundEntity);
            }
            return (exists);
        }
    }
}