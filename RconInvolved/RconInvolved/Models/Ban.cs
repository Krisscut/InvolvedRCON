using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RconInvolved.Models
{
    class Ban
    {
        int id;
        String guid;
        String duration;
        String comment;

        public Ban(int id, String guid, String duration, String comment)
        {
            this.id = id;
            this.guid = guid;
            this.duration = duration;
            this.comment = comment;
        }
    }
}
