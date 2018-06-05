// --------------------------------------------------------------------------------------------------------------------
// <copyright file=CommonQueries.cs company="chas.brock@outlook.com">
//      copyright charlie brock 2018 
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChasWare.Data
{
    /// <summary>
    ///     Selection of commonly used queries
    /// </summary>
    public static class CommonQueries
    {
        #region public methods

        /// <summary>
        ///     The get known titles.
        /// </summary>
        /// <param name="context">
        ///     The db context.
        /// </param>
        /// <returns>
        ///     list of known titles <see cref="ObservableCollection{String}" />.
        /// </returns>
        public static ObservableCollection<string> GetKnownTitles(DataContext context)
        {
            List<string> found = (from p in context.Persons
                where p.Title != null
                select p.Title).Distinct().ToList();
            found.Sort();
            if (!found.Contains(string.Empty))
            {
                found.Insert(0, string.Empty);
            }

            return new ObservableCollection<string>(found);
        }

        #endregion
    }
}