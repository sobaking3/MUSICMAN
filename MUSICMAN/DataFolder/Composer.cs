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
    
    public partial class Composer
    {
        public Composer()
        {
            this.Plates = new HashSet<Plates>();
        }
    
        public int IdComposer { get; set; }
        public string ComposerName { get; set; }
    
        public virtual ICollection<Plates> Plates { get; set; }
    }
}
