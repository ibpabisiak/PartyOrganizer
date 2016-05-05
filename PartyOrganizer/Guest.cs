using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyOrganizer
{
    public class Guest
    {
        public int ID_GUEST { get; set; }
        public int ID_EVENT { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Sex { get; set; }
        public bool Confirmed { get; set; }
        public bool UpdateChanges { get; set; }
    }
}
