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
    
    public partial class Shop
    {
        public Shop()
        {
            this.Plates = new HashSet<Plates>();
            this.Workers1 = new HashSet<Workers>();
        }
    
        public int IdShop { get; set; }
        public string ShopName { get; set; }
        public int IdShopPlace { get; set; }
        public string OpeningTime { get; set; }
        public string ClosingTime { get; set; }
        public string PhoneOfDirector { get; set; }
        public int IdWorkerInfo { get; set; }
    
        public virtual Adress Adress { get; set; }
        public virtual ICollection<Plates> Plates { get; set; }
        public virtual Workers Workers { get; set; }
        public virtual ICollection<Workers> Workers1 { get; set; }
    }
}