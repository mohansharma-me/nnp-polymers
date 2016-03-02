using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NNPPoly
{
    public class Record
    {
        public Decimal ID;
        public DateTime Date;
        public Payment payment;
        public Decimal ClientID;
        public String ClientName;

        public static void RecordNow(UserAccount client,Payment payment,bool saveDB=false)
        {
            Record record = new Record();
            record.ClientID = client.ID;
            record.ClientName = client.ClientName;
            record.Date = DateTime.Now;
            record.payment = payment;
            record.ID = 0;
            bool alreadyExists = false;
            foreach (Record rec in Datastore.dataFile.Records)
            {
                if (rec.ClientID == record.ClientID && record.payment.ID == rec.payment.ID)
                    alreadyExists = true;
                if (rec.ID > record.ID)
                    record.ID = rec.ID;
            }
            record.ID++;
            if (!alreadyExists)
            {
                Datastore.dataFile.Records.Add(record);
                if (saveDB)
                    Datastore.dataFile.Save();
            }
        }
    }
}
