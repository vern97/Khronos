using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeyondTheTutor.DAL
{
    public class ContextGetter
    {
        private const string azure = "BTTContext_Azure";
        private const string local = "BeyondTheTutorContext";

        public string getContext { get; set; }
        public ContextGetter()
        {
            //if false fire local database
            //if true fire azure database
            if (false)
            {
                getContext = azure;
            }
            else
            {
                getContext = local;
            }
        }
    }
}