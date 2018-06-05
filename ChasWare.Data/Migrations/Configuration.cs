// --------------------------------------------------------------------------------------------------------------------
// <copyright file=Configuration.cs company="chas.brock@outlook.com">
//      copyright charlie brock 2018 
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.Data.Entity.Migrations;

namespace ChasWare.Data.Migrations
{
    /// <inheritdoc />
    /// <summary>
    ///     The configuration.
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Configuration" /> class.
        /// </summary>
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "ChasWare.Data.DataContext";
        }

        #endregion

        #region other methods

        /// <summary>
        ///     The original data context.
        /// </summary>
        /// <param name="context">
        ///     The context.
        /// </param>
        protected override void Seed(DataContext context)
        {
            // This method will be called after migrating to the latest version.

            // You can use the DbSet<T>.AddOrUpdate() helper extension method 
            // to avoid creating duplicate seed data.
        }

        #endregion
    }
}