using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NNPPoly.classes.old_version
{
    public class DataFile
    {
        public static Exception Error;

        public Decimal noOfCopies = 1, UserAccountIDManager = 0;
        public double DefualtLessDays = 10;
        public int noOfDebitNoteRows = 8, noOfDebitAdviseRows = 9;
        public long currentDebitNoteID = 0, currentDebitAdviseID = 0;


        public String printerName = "";
        public System.Drawing.Printing.PaperSource debitNotePaperSource, debitAdvisePaperSource, envelopePaperSource;

        public String sms_API = @"http://bulksms.hakimisolution.com/api/sendhttp.php?authkey=4600A09VyiPDe540d4d27&mobiles=%numbers%&message=%msg%&sender=NNPRJT&route=4";
        public Decimal sms_NoOfRequestDays = 90;

        public String mail_MyMail = "nnp.poly@gmail.com";
        public String mail_Username = "nnp.poly@gmail.com";
        public String mail_Password = "";

        public String msg_Collection = "PL. ARRANGE PAYMENT TODAY & SMS UTR NO.\r\nAMT RS. %amt%\r\nNAME: M/S. NAZARALLY NOORBHAI PATEL\r\nBANK: HDFC BANK\r\nACCT NO: 01010440000137\r\nRTGS CODE: HDFC0000101";
        public String msg_Dispatch = "YOUR FOLLOWING MATERIAL IS DESPATCHED\r\nDATE: %date%\r\nGRADE: %grade%\r\nQTY: %qty%\r\nAMT: %amt%\r\nPLEASE ARRANGE RTGS TODAY";
        public String msg_Stock = "GRADE %grade% IS IN STOCK WITH RIL.\r\nPLACE YOUR VALUED ORDER SOON SO THAT WE MAY PUNCH YOUR ORDER WITH RIL.\r\nN.B. IT DOES NOT GAURANTEE ANY SUPPLY CONFIRMATION.";
        public String msg_Request = "SIR,\r\nYOU HAVE NOT LIFTED ANY QUANTITY SINCE LAST %days% DAYS.\r\nRIL CANCELLS SAP NAME REGISTRATION OF NON-ACTIVE CUSTOMER.\r\nTO REMAIN REGISTRED WITH RIL, PLEASE PLACE ORDER, EVEN OF SMALL QUANTITY, SOON.\r\nWITH REGARDS.";

        public String mail_From = "FROM: M/S. NAZARALLY NOORBHAI PATEL, RAJKOT.";
        public String mail_Collection = "AN AMOUNT OF RS. %amt% IS OVERDUE, TODAY, AS PER DETAILS GIVEN BELOW<br/><br/>PLEASE ARRANGE PAYMENT THROUGH RTGS TODAY AND GIVE UTR NO. THROUGH MAIL TO GET PROPER & TIMELY CREDIT.";
        public String mail_Despatch = "PLEASE ARRANGE PAYMENT THROUGH RTGS TODAY TO AVAIL CASH DISCOUNT.";
        public String mail_Stock = "<b>%grade% GRADE IS AVAILABLE.</b> PLEASE PLACE YOUR VALUED ORDER, BY RETURN OF MAIL.<br/>N.B.<br/>THIS MESSAGE DOES NOT CONFIRM DELIVERY OF MATERIAL, AS ALLOCATION IS SUBJECT TO CONFIRMATION BY RIL.";
        public String mail_Request = "Sir,<br/>There is NIL order from your unit, since last %days% days. Please place an order of even small quantity, to revive our business relation.";


        public String titleOfaDNote = "M/S NAZARALLY NOORBHAI PATEL";
        public String addressOfaDNote = "POLYMERS DIVISION \"EXCEL HOUSE\", OPP. RAJ BANK,\r\nPANCHNATH MAIN ROAD, RAJKOT - 360001,\r\nPHONE: 2242195, 2242196 FAX: (0281) 3046100\r\nEMAIL: nnp.poly@yahoo.com";
        public String addressOfaDAdvise = "\"EXCEL HOUSE\", OPP. RAJ BANK, PANCHNATH MAIN ROAD, RAJKOT - 360001. PHONE: 2242195, 2242196 FAX: (0281) 3046100 EMAIL: nnp.poly@yahoo.com";
        public String descriptionRowOfaDNote = "Being amount DEBITED, for Cash Discount allowed in Invoices, Which is reversed, due to preferring Credit Terms by you.";

        public List<Grade> Grades = new List<Grade>();
        public List<String> SpecialTypes = new List<String>();
        public List<String> PriorityTypes = new List<String>();
        public List<UserAccount> UserAccounts = new List<UserAccount>();
        public List<Record> Records = new List<Record>();
        public List<DebitNotePrint> DebitNotes = new List<DebitNotePrint>();
        public List<DebitNotePrint> DebitAdvises = new List<DebitNotePrint>();


        public bool Save(String file)
        {
            eXML xml = new eXML();
            bool fl = xml.Write(file, typeof(DataFile), this, true);
            Error = xml.Error;
            return fl;
        }

        public static DataFile Read(String file)
        {
            DataFile df = null;
            eXML xml = new eXML();
            xml.Read<DataFile>(file, typeof(DataFile), ref df, true);
            Error = xml.Error;
            return df;
        }
    }
}
