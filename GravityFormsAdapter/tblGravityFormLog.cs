//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GravityFormsAdapter
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblGravityFormLog
    {
        public int id { get; set; }
        public System.DateTime startTime { get; set; }
        public Nullable<System.DateTime> endTime { get; set; }
        public int isSuccess { get; set; }
        public string errorMessage { get; set; }
        public int newFormCount { get; set; }
    }
}
