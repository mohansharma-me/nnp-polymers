using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NNPPoly.classes
{
    public class MessageHolder
    {
        public String client_name;
        public String mobiles;
        public String emails;
        public Types type;

        public String sms_content;

        public String mail_subject;
        public String mail_content;

        public String sms_status = "";
        public String email_status = "";

        public enum Types
        {
            COLLECTION,
            STOCK,
            DESPATCH,
            REQUESTORDER,
            CUSTOM,
            APP_MOU
        }
    }
}
