using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WordPressPCL.Models
{
    public class CommentThreaded : Comment
    {
        public int Depth { get; set; }

    }
}
