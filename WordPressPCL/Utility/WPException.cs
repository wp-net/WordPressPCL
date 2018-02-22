using System;

namespace WordPressPCL.Utility
{
    /// <summary>
    /// 
    /// </summary>
    public class WPException:Exception
    {
        /// <summary>
        /// code
        /// </summary>
        public string Code { get;private set; }

        /// <summary>
        /// status
        /// </summary>
        public int Status { get;private set; }

        internal WPException(string msg,string code,int status):base(msg)
        {
            Code = code;
            Status = status;
        }
    }
}
