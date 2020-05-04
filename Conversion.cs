using System;

namespace ACH2JSON
{
    class Conversion : mainWindow
    {
        CreateJSON cjn = new CreateJSON();
        public int PaymentCount { get; set; }
        public int AddendaCount { get; set; }
        public void getFilePath(string o)
        {
            cjn.damnFilePath = o;
            //Console.WriteLine("your file path is: " + o);
        }
        public string SECResult { get; set; }
        public void FHRConv(string p)
        {
            //begin the redundancy!
            //TODO just get rid of the string declarations and directly call the createjson getsets
            string FHRLine = p;
            string FHRRTC = p.Substring(0, 1);
            cjn.FHRRecordTypeCode = FHRRTC;
            string FHRPC = p.Substring(1, 2);
            cjn.FHRPriorityCode = FHRPC;
            string FHRID = p.Substring(3, 10);
            cjn.FHRImmediateDestination = FHRID;
            string FHRIO = p.Substring(13, 10);
            cjn.FHRImmediateOrigin = FHRIO;
            string FHRFCD = p.Substring(23, 6);
            cjn.FHRFileCreationDate = FHRFCD;
            string FHRFCT = p.Substring(29, 4);
            cjn.FHRFileCreationTime = FHRFCT;
            string FHRFIM = p.Substring(33, 1);
            cjn.FHRFileIDModifier = FHRFIM;
            string FHRRS = p.Substring(34, 3);
            cjn.FHRRecordSize = FHRRS;
            string FHRBF = p.Substring(37, 2);
            cjn.FHRBlockingFactor = FHRBF;
            string FHRFC = p.Substring(39, 1);
            cjn.FHRFormatCode = FHRFC;
            string FHRIDN = p.Substring(40, 23);
            cjn.FHRImmediateDestinationName = FHRIDN;
            string FHRION = p.Substring(63, 23);
            cjn.FHRImmediateOriginName = FHRION;
            string FHRRC = p.Substring(86, 8);
            cjn.FHRReferenceCode = FHRRC;
            cjn.CreateThatFile(1);
        }
        public int BHRConv(string q)
        {
            string BHRLine = q;
            string SEC = q.Substring(50, 3);
            int result;
            cjn.BHRRecordTypeCode = q.Substring(0, 1);
            cjn.BHRServiceClassCode = q.Substring(1, 3);
            cjn.BHRCompanyName = q.Substring(4, 16);
            cjn.BHRCompanyDiscretionaryData = q.Substring(20, 20);
            cjn.BHRCompanyIdentification = q.Substring(40, 10);
            cjn.BHRStandardEntryClassCode = q.Substring(50, 3);
            cjn.BHRCompanyEntryDescription = q.Substring(53, 10);
            cjn.BHRCompanyDescriptiveDate = q.Substring(63, 6);
            cjn.BHREffectiveEntryDate = q.Substring(69, 6);
            cjn.BHRSettlementDate = q.Substring(75, 3);
            cjn.BHROriginatorStatusCode = q.Substring(78, 1);
            cjn.BHROriginatingDFIIdentification = q.Substring(79, 8);
            cjn.BHRBatchNumber = q.Substring(87, 7);
            cjn.CreateThatFile(2);
            //here's the determination of further conversion...
            if (SEC == "CTX")
            {
                Console.WriteLine("CTX file found...");
                SECResult = "CTX";
                result = 1;
                return result;
            } else if(SEC == "CCD")
            {
                Console.WriteLine("CCD file found...");
                SECResult = "CCD";
                result = 2;
                return result;
            } else if(SEC == "PPD")
            {
                SECResult = "PPD";
                Console.WriteLine("PPD file found...");
                result = 3;
                return result;
            } else if(SEC == "TEL")
            {
                Console.WriteLine("TEL file found...");
                SECResult = "TEL";
                result = 4;
                return result;
            } else if(SEC == "WEB")
            {
                Console.WriteLine("WEB file found...");
                SECResult = "WEB";
                result = 5;
                return result;
            } else
            {
                return 9;
            }
        }
        public void PEConv(string r)
        {
            if (SECResult == "PPD")
            {
                Console.WriteLine("PPD Conversion Started...");
                cjn.PaymentCount = PaymentCount;
                cjn.PPDRecordTypeCode = r.Substring(0, 1);
                cjn.PPDTransactionCode = r.Substring(1, 2);
                cjn.PPDReceivingDFIIdentification = r.Substring(3, 8);
                cjn.PPDCheckDigit = r.Substring(11, 1);
                cjn.PPDDFIAccountNumber = r.Substring(12, 17);
                cjn.PPDAmount = r.Substring(29, 10);
                cjn.PPDIndividualIdentificationNumber = r.Substring(39, 15);
                cjn.PPDIndividualName = r.Substring(54, 22);
                cjn.PPDDiscretionaryData = r.Substring(76, 2);
                cjn.PPDAddendaRecordIndicator = r.Substring(78, 1);
                cjn.PPDTraceNumber = r.Substring(79, 15);
                cjn.CreateThatFile(3);

            } else if(SECResult == "CTX")
            {
                Console.WriteLine("CTX Conversion Started...");
                cjn.PPDRecordTypeCode = r.Substring(0, 1); //RecordTypeCode
                cjn.PPDTransactionCode = r.Substring(1, 2); //Transaction Code
                cjn.PPDReceivingDFIIdentification = r.Substring(3, 8); //ReceivingDFIIdentification
                cjn.PPDCheckDigit = r.Substring(11, 1); //CheckDigit
                cjn.PPDDFIAccountNumber = r.Substring(12, 17); //DFIAccountNumber
                cjn.PPDAmount = r.Substring(29, 10); //TotalAmount
                cjn.PPDIndividualIdentificationNumber = r.Substring(39, 15); //IdentificationNumber
                cjn.PPDIndividualName = r.Substring(54, 4); //NumberOfAddendaRecords
                cjn.PPDDiscretionaryData = r.Substring(58, 16); //ReceivingCompanyName
                cjn.PPDAddendaRecordIndicator = r.Substring(74, 2); //Reserved
                cjn.PPDTraceNumber = r.Substring(76, 2); //DiscretionaryData
                cjn.CTXField12 = r.Substring(78, 1); //AddendaRecordIndicator
                cjn.CTXField13 = r.Substring(79, 15); //TraceNumber
                cjn.CreateThatFile(4);
            } else if(SECResult == "CCD")
            {
                Console.WriteLine("CCD Conversion Started...");
                cjn.PPDRecordTypeCode = r.Substring(0, 1); //RecordTypeCode
                cjn.PPDTransactionCode = r.Substring(1, 2); //Transaction Code
                cjn.PPDReceivingDFIIdentification = r.Substring(3, 8); //ReceivingDFIIdentification
                cjn.PPDCheckDigit = r.Substring(11, 1); //CheckDigit
                cjn.PPDDFIAccountNumber = r.Substring(12, 17); //DFIAccountNumber
                cjn.PPDAmount = r.Substring(29, 10); //Amount
                cjn.PPDIndividualIdentificationNumber = r.Substring(39, 15); //IdentificationNumber
                cjn.PPDIndividualName = r.Substring(54, 22); //ReceivingCompanyName
                cjn.PPDDiscretionaryData = r.Substring(76, 2); //DiscretionaryData
                cjn.PPDAddendaRecordIndicator = r.Substring(78, 1); //AddendaRecordIndicator
                cjn.PPDTraceNumber = r.Substring(79, 15); //TraceNumber
                cjn.CreateThatFile(5);
            } else if(SECResult == "TEL")
            {
                Console.WriteLine("TEL conversion started...");
                cjn.PPDRecordTypeCode = r.Substring(0, 1); //RecordTypeCode
                cjn.PPDTransactionCode = r.Substring(1, 2); //Transaction Code
                cjn.PPDReceivingDFIIdentification = r.Substring(3, 8); //ReceivingDFIIdentification
                cjn.PPDCheckDigit = r.Substring(11, 1); //CheckDigit
                cjn.PPDDFIAccountNumber = r.Substring(12, 17); //DFIAccountNumber
                cjn.PPDAmount = r.Substring(29, 10); //Amount
                cjn.PPDIndividualIdentificationNumber = r.Substring(39, 15); //IndividualIdentificationNumber
                cjn.PPDIndividualName = r.Substring(54, 22); //IndividualName
                cjn.PPDDiscretionaryData = r.Substring(76, 2); //DiscretionaryData
                cjn.PPDAddendaRecordIndicator = r.Substring(78, 1); //AddendaRecordIndicator
                cjn.PPDTraceNumber = r.Substring(79, 15); //TraceNumber
                cjn.CreateThatFile(6);
            } else if(SECResult == "WEB")
            {
                Console.WriteLine("WEB conversion started...");
                cjn.PPDRecordTypeCode = r.Substring(0, 1); //RecordTypeCode
                cjn.PPDTransactionCode = r.Substring(1, 2); //Transaction Code
                cjn.PPDReceivingDFIIdentification = r.Substring(3, 8); //ReceivingDFIIdentification
                cjn.PPDCheckDigit = r.Substring(11, 1); //CheckDigit
                cjn.PPDDFIAccountNumber = r.Substring(12, 17); //DFIAccountNumber
                cjn.PPDAmount = r.Substring(29, 10); //Amount
                cjn.PPDIndividualIdentificationNumber = r.Substring(39, 15); //IdentificationNumber
                cjn.PPDIndividualName = r.Substring(54, 22); //ReceivingCompanyName
                cjn.PPDDiscretionaryData = r.Substring(76, 2); //PaymentTypeCode
                cjn.PPDAddendaRecordIndicator = r.Substring(78, 1); //AddendaRecordIndicator
                cjn.PPDTraceNumber = r.Substring(79, 15); //TraceNumber
                cjn.CreateThatFile(7);
            } else
            {
                Console.WriteLine("FORMAT NOT DEFINED");
                //catch them errors boi
            }
        }
        public void ARConv(string s) //TODO - parse addenda records
        {
            Console.WriteLine("Addenda Conversion Started...");
            cjn.AddendaCount = AddendaCount;
            cjn.AddendaRecordTypeCode = s.Substring(0, 1);
            cjn.AddendaAddendaTypeCode = s.Substring(1, 2);
            cjn.AddendaPaymentRelatedInformation = s.Substring(3, 80);
            cjn.AddendaAddendaSequenceNumber = s.Substring(83, 4);
            cjn.AddendaEntrySequenceNumber = s.Substring(87, 7);
            cjn.CreateThatFile(10);
        }
        public void BCRConv(string t)
        {
            Console.WriteLine("Batch Control Record Conversion Started...");
            cjn.BCRRecordTypeCode = t.Substring(0, 1);
            cjn.BCRServiceClassCode = t.Substring(1, 3);
            cjn.BCREntryAddendaCount = t.Substring(4, 6);
            cjn.BCREntryHash = t.Substring(10, 10);
            cjn.BCRTotalDebitEntryAmt = t.Substring(20, 12);
            cjn.BCRTotalCreditEntryAmt = t.Substring(32, 12);
            cjn.BCRCompanyIdentification = t.Substring(44, 10);
            cjn.BCRMessageAuthCode = t.Substring(54, 19);
            cjn.BCRReserved = t.Substring(73, 6);
            cjn.BCROriginatingDFIIdentification = t.Substring(79, 8);
            cjn.BCRBatchNumber = t.Substring(87, 7);
            cjn.CreateThatFile(8);
        }
        public void FCRConv(string u)
        {
            Console.WriteLine("File Control Record Conversion Started...");
            cjn.FCRRecordTypeCode = u.Substring(0, 1);
            cjn.FCRBatchCount = u.Substring(1, 6);
            cjn.FCRBlockCount = u.Substring(7, 6);
            cjn.FCREntryAddendaCount = u.Substring(13, 8);
            cjn.FCREntryHash = u.Substring(21, 10);
            cjn.FCRTotalDebitEntryAmt = u.Substring(31, 12);
            cjn.FCRTotalCreditEntryAmt = u.Substring(43, 12);
            cjn.FCRReserved = u.Substring(55, 39);
            cjn.CreateThatFile(9);
        }
    }
}
