using System;
using System.Collections.Generic;

namespace UserApi.Data
{
    public partial class Maker
    {
        public Maker()
        {
            Cars = new HashSet<OriginalCar>();
        }

        public Guid IdMaker { get; set; }
        public string Name { get; set; } = null!;
        public string? Origin { get; set; }
        public int? Founded { get; set; }
        public string? Description { get; set; }
        public string? Related { get; set; }
        public string? WikiLink { get; set; }

        public virtual ICollection<OriginalCar> Cars { get; set; }
    }
}
