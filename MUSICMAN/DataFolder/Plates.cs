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
    
    public partial class Plates
    {
        public int IdPlate { get; set; }
        public string PlateName { get; set; }
        public int IdShop { get; set; }
        public int IdComposer { get; set; }
        public int IdPublisher { get; set; }
        public string Cost { get; set; }
        public string Duration { get; set; }
        public string CreationDate { get; set; }
        public int Count { get; set; }
    
        public virtual Composer Composer { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual Shop Shop { get; set; }
    }
}