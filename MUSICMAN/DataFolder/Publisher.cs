//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MUSICMAN.DataFolder
{
    using System;
    using System.Collections.Generic;
    
    public partial class Publisher
    {
        public Publisher()
        {
            this.Plates = new HashSet<Plates>();
        }
    
        public int IdPublisher { get; set; }
        public string PublisherName { get; set; }
        public System.DateTime PublisherContractTil { get; set; }
        public string Rating { get; set; }
    
        public virtual ICollection<Plates> Plates { get; set; }
    }
}
