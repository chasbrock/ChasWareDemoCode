// --------------------------------------------------------------------------------------------------------------------
// <copyright file=Store.cs company="chas.brock@outlook.com">
//      copyright charlie brock 2018 
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using ChasWare.Common.Utils.Transform;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChasWare.Data.Classes
{
    [ExportToTs]
    public class Store : EntityMapped
    {
        #region public properties

        public virtual SalesPerson SalesPerson { get; set; }

        #endregion

        #region Entity Framework Mapping

        [ForeignKey(nameof(Classes.SalesPerson))]
        public int? SalesPersonId { get; set; }

        [StringLength(256)]
        public string StoreName { get; set; }

        #endregion
    }
}