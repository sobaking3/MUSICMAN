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
    
    public partial class Workers
    {
        public Workers()
        {
            this.Shop = new HashSet<Shop>();
        }
    
        public int IdWorkerInfo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string DateOfBirth { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public int IdShop { get; set; }
        public int IdUser { get; set; }
        public byte[] PhotoStaff { get; set; }
    
        public virtual ICollection<Shop> Shop { get; set; }
        public virtual Shop Shop1 { get; set; }
        public virtual User User { get; set; }
    }
}
