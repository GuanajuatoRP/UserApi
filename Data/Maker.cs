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

        public virtual ICollection<OriginalCar> Cars { get; set; }
    }
}
