using System;
using System.Collections.Generic;

namespace GraphQLDemo.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class Order
    {
        #region public  Properties
        /// <summary>
        /// Unique Id of the order
        /// </summary>
		public int Id { get; set; }
        /// <summary>
        /// The Service related to the order
        /// </summary>
		public Service Service { get; set; }
        public int ServiceId { get; set; }
        public decimal Fee { get; set; }
       
        public Login OrderedBy { get; set; }
       
        public int LoginId { get; set; }
        #endregion
    }
}