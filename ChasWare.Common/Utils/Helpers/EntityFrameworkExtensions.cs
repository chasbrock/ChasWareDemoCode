// --------------------------------------------------------------------------------------------------------------------
// <copyright file=EntityFrameworkExtensions.cs company="chas.brock@outlook.com">
//      copyright charlie brock 2018 
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace ChasWare.Common.Utils.Helpers
{
    /// <summary>
    ///     The entity framework extensions.
    /// </summary>
    public static class EntityFrameworkExtensions
    {
        #region public methods

        /// <summary>
        ///     update all changed records with current timestamp
        /// </summary>
        /// <param name="context">
        ///     The context.
        /// </param>
        public static void UpdateTimeStamps(this DbContext context)
        {
            // timestamp all changed 
            DateTime now = DateTime.Now;
            foreach (DbEntityEntry entry in context.ChangeTracker.Entries())
            {
                if ((entry.State & EntityState.Unchanged) == EntityState.Unchanged)
                {
                    continue;
                }

                DbPropertyEntry property = entry.Property("ModifiedDate");
                if (property != null)
                {
                    property.CurrentValue = now;
                }
            }
        }

        #endregion
    }
}