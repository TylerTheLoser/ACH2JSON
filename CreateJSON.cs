using System;
using System.IO;
//TODO:
//add identifier to each payment found. right now it will detect as a "duplicate" in the JSON file, maybe add a trans number?
//maybe add a counter to the main, pass that number along and assign it to the first record
//JSONify it
namespace ACH2JSON
{
    class CreateJSON
    {
        //********** FILE HEADER RECORDS START **********//
        public string FHRRecordTypeCode { get; set; }
        public string FHRPriorityCode { get; set; }
        public string FHRImmediateDestination { get; set; }
        public string FHRImmediateOrigin { get; set; }
        public string FHRFileCreationDate { get; set; }
        public string FHRFileCreationTime { get; set; }
        public string FHRFileIDModifier { get; set; }
        public string FHRRecordSize { get; set; }
        public string FHRBlockingFactor { get; set; }
        public string FHRFormatCode { get; set; }
        public string FHRImmediateDestinationName { get; set; }
        public string FHRImmediateOriginName { get; set; }
        public string FHRReferenceCode { get; set; }
        //********** FILE HEADER RECORDS END **********//

        //********** BATCH HEADER RECORDS START **********//
        public string BHRRecordTypeCode { get; set; }
        public string BHRServiceClassCode { get; set; }
        public string BHRCompanyName { get; set; }
        public string BHRCompanyDiscretionaryData { get; set; } //optional
        public string BHRCompanyIdentification { get; set; }
        public string BHRStandardEntryClassCode { get; set; }
        public string BHRCompanyEntryDescription { get; set; }
        public string BHRCompanyDescriptiveDate { get; set; } //optional
        public string BHREffectiveEntryDate { get; set; }
        public string BHRSettlementDate { get; set; } //julien date
        public string BHROriginatorStatusCode { get; set; }
        public string BHROriginatingDFIIdentification { get; set; }
        public string BHRBatchNumber { get; set; }
        //********** BATCH HEADER RECORDS END **********//

        //********** PPD HEADER START **********//
        public int PaymentCount { get; set; }
        public string PPDRecordTypeCode { get; set; } //always will be 6
        public string PPDTransactionCode { get; set; } //2 char
        public string PPDReceivingDFIIdentification { get; set; } //8 char
        public string PPDCheckDigit { get; set; } //1 char
        public string PPDDFIAccountNumber { get; set; } //17 char
        public string PPDAmount { get; set; } //10 char
        public string PPDIndividualIdentificationNumber { get; set; } //15 char
        public string PPDIndividualName { get; set; } //22 char
        public string PPDDiscretionaryData { get; set; } //2 char
        public string PPDAddendaRecordIndicator { get; set; } //1 char
        public string PPDTraceNumber { get; set; } //15 char
        //********** PPD HEADER END **********//

        //********** CTX ONLY **********//
        public string CTXField12 { get; set; }
        public string CTXField13 { get; set; }
        //********** CTX END **********//

        //********** ADDENDA START **********//
        public int AddendaCount { get; set; }
        public string AddendaRecordTypeCode { get; set; }
        public string AddendaAddendaTypeCode { get; set; }
        public string AddendaPaymentRelatedInformation { get; set; }
        public string AddendaAddendaSequenceNumber { get; set; }
        public string AddendaEntrySequenceNumber { get; set; }
        //********** ADDENDA END **********//

        //********** BATCH CONTROL RECORD START **********//
        public string BCRRecordTypeCode { get; set; }
        public string BCRServiceClassCode { get; set; }
        public string BCREntryAddendaCount { get; set; }
        public string BCREntryHash { get; set; }
        public string BCRTotalDebitEntryAmt { get; set; }
        public string BCRTotalCreditEntryAmt { get; set; }
        public string BCRCompanyIdentification { get; set; }
        public string BCRMessageAuthCode { get; set; }
        public string BCRReserved { get; set; }
        public string BCROriginatingDFIIdentification { get; set; }
        public string BCRBatchNumber { get; set; }
        //********** BATCH CONTROL RECORD END **********//

        //********** FILE CONTROL RECORD START **********//
        public string FCRRecordTypeCode { get; set; }
        public string FCRBatchCount { get; set; }
        public string FCRBlockCount { get; set; }
        public string FCREntryAddendaCount { get; set; }
        public string FCREntryHash { get; set; }
        public string FCRTotalDebitEntryAmt { get; set; }
        public string FCRTotalCreditEntryAmt { get; set; }
        public string FCRReserved { get; set; }
        //********** FILE CONTROL RECORD END **********//

        public string damnFilePath { get; set; }

        public void CreateThatFile(int y)
        {
            //Console.WriteLine(damnFilePath);
            Console.WriteLine("FILE CREATION STARTED");
            switch (y)
            {
                case 1: //File Header Record
                    AppendFHR(damnFilePath);
                    break;
                case 2: //Batch Header Record
                    AppendBHR(damnFilePath);
                    break;
                case 3: //PPD
                    AppendPE(damnFilePath);
                    break;
                case 4: //CTX
                    AppendCTX(damnFilePath);
                    break;
                case 5: //CCD
                    AppendCCD(damnFilePath);
                    break;
                case 6: //TEL
                    AppendTEL(damnFilePath);
                    break;
                case 7: //WEB
                    AppendWEB(damnFilePath);
                    break;
                case 8: //Batch Control Record
                    AppendBCR(damnFilePath);
                    break;
                case 9: //File Control Record
                    AppendFCR(damnFilePath);
                    break;
                case 10: //Addenda
                    AppendAddenda(damnFilePath);
                    break;

            }
        }
        public void AppendPE(string info) //PPD
        {
            using (StreamWriter sw = File.AppendText(info))
            {
                sw.WriteLine("\"" + "PPD_" + PaymentCount + "\"" + ":[");
                sw.WriteLine(@"{");
                sw.WriteLine(@"""RecordTypeCode"": " + "\"" + PPDRecordTypeCode + "\"" + ",");
                sw.WriteLine(@"""TransactionCode"": " + "\"" + PPDTransactionCode + "\"" + ",");
                sw.WriteLine(@"""ReceivingDFIIdentification"": " + "\"" + PPDReceivingDFIIdentification + "\"" + ",");
                sw.WriteLine(@"""CheckDigit"": " + "\"" + PPDCheckDigit + "\"" + ",");
                sw.WriteLine(@"""DFIAccountNumber"": " + "\"" + PPDDFIAccountNumber + "\"" + ",");
                sw.WriteLine(@"""Amount"": " + "\"" + PPDAmount + "\"" + ",");
                sw.WriteLine(@"""IndividualIdentificationNumber"": " + "\"" + PPDIndividualIdentificationNumber + "\"" + ",");
                sw.WriteLine(@"""IndividualName"": " + "\"" + PPDIndividualName + "\"" + ",");
                sw.WriteLine(@"""DiscretionaryData"": " + "\"" + PPDDiscretionaryData + "\"" + ",");
                sw.WriteLine(@"""AddendaRecordIndicator"": " + "\"" + PPDAddendaRecordIndicator + "\"" + ",");
                sw.WriteLine(@"""TraceNumber"": " + "\"" + PPDTraceNumber + "\"");
                sw.WriteLine(@"}");
                sw.WriteLine(@"],");
                sw.Close();
            }
        }
        public void AppendCCD(string info)
        {
            using (StreamWriter sw = File.AppendText(info))
            {
                sw.WriteLine(@"""CCD"":[");
                sw.WriteLine(@"{");
                sw.WriteLine(@"""RecordTypeCode"": " + "\"" + PPDRecordTypeCode + "\"" + ",");
                sw.WriteLine(@"""TransactionCode"": " + "\"" + PPDTransactionCode + "\"" + ",");
                sw.WriteLine(@"""ReceivingDFIIdentification"": " + "\"" + PPDReceivingDFIIdentification + "\"" + ",");
                sw.WriteLine(@"""CheckDigit"": " + "\"" + PPDCheckDigit + "\"" + ",");
                sw.WriteLine(@"""DFIAccountNumber"": " + "\"" + PPDDFIAccountNumber + "\"" + ",");
                sw.WriteLine(@"""Amount"": " + "\"" + PPDAmount + "\"" + ",");
                sw.WriteLine(@"""IdentificationNumber"": " + "\"" + PPDIndividualIdentificationNumber + "\"" + ",");
                sw.WriteLine(@"""ReceivingCompanyName"": " + "\"" + PPDIndividualName + "\"" + ",");
                sw.WriteLine(@"""DiscretionaryData"": " + "\"" + PPDDiscretionaryData + "\"" + ",");
                sw.WriteLine(@"""AddendaRecordIndicator"": " + "\"" + PPDAddendaRecordIndicator + "\"" + ",");
                sw.WriteLine(@"""TraceNumber"": " + "\"" + PPDTraceNumber + "\"");
                sw.WriteLine(@"}");
                sw.WriteLine(@"],");
                sw.Close();
            }
        }
        public void AppendTEL(string info)
        {
            using (StreamWriter sw = File.AppendText(info))
            {
                sw.WriteLine(@"""TEL"":[");
                sw.WriteLine(@"{");
                sw.WriteLine(@"""RecordTypeCode"": " + "\"" + PPDRecordTypeCode + "\"" + ",");
                sw.WriteLine(@"""TransactionCode"": " + "\"" + PPDTransactionCode + "\"" + ",");
                sw.WriteLine(@"""ReceivingDFIIdentification"": " + "\"" + PPDReceivingDFIIdentification + "\"" + ",");
                sw.WriteLine(@"""CheckDigit"": " + "\"" + PPDCheckDigit + "\"" + ",");
                sw.WriteLine(@"""DFIAccountNumber"": " + "\"" + PPDDFIAccountNumber + "\"" + ",");
                sw.WriteLine(@"""Amount"": " + "\"" + PPDAmount + "\"" + ",");
                sw.WriteLine(@"""IndividualIdentificationNumber"": " + "\"" + PPDIndividualIdentificationNumber + "\"" + ",");
                sw.WriteLine(@"""IndividualName"": " + "\"" + PPDIndividualName + "\"" + ",");
                sw.WriteLine(@"""DiscretionaryData"": " + "\"" + PPDDiscretionaryData + "\"" + ",");
                sw.WriteLine(@"""AddendaRecordIndicator"": " + "\"" + PPDAddendaRecordIndicator + "\"" + ",");
                sw.WriteLine(@"""TraceNumber"": " + "\"" + PPDTraceNumber + "\"");
                sw.WriteLine(@"}");
                sw.WriteLine(@"],");
                sw.Close();
            }
        }
        public void AppendCTX(string info)
        {
            using (StreamWriter sw = File.AppendText(info))
            {
                sw.WriteLine(@"""CTX"":[");
                sw.WriteLine(@"{");
                sw.WriteLine(@"""RecordTypeCode"": " + "\"" + PPDRecordTypeCode + "\"" + ",");
                sw.WriteLine(@"""TransactionCode"": " + "\"" + PPDTransactionCode + "\"" + ",");
                sw.WriteLine(@"""ReceivingDFIIdentification"": " + "\"" + PPDReceivingDFIIdentification + "\"" + ",");
                sw.WriteLine(@"""CheckDigit"": " + "\"" + PPDCheckDigit + "\"" + ",");
                sw.WriteLine(@"""DFIAccountNumber"": " + "\"" + PPDDFIAccountNumber + "\"" + ",");
                sw.WriteLine(@"""TotalAmount"": " + "\"" + PPDAmount + "\"" + ",");
                sw.WriteLine(@"""IdentificationNumber"": " + "\"" + PPDIndividualIdentificationNumber + "\"" + ",");
                sw.WriteLine(@"""NumberOfAddendaRecords"": " + "\"" + PPDIndividualName + "\"" + ",");
                sw.WriteLine(@"""ReceivingCompanyName"": " + "\"" + PPDDiscretionaryData + "\"" + ",");
                sw.WriteLine(@"""Reserved"": " + "\"" + PPDAddendaRecordIndicator + "\"" + ",");
                sw.WriteLine(@"""DiscretionaryData"": " + "\"" + PPDTraceNumber + "\"" + ",");
                sw.WriteLine(@"""AddendaRecordIndicator"": " + "\"" + CTXField12 + "\"" + ",");
                sw.WriteLine(@"""TraceNumber"": " + "\"" + CTXField13 + "\"");
                sw.WriteLine(@"}");
                sw.WriteLine(@"],");
                sw.Close();
            }
        }
        public void AppendWEB(string info)
        {
            using (StreamWriter sw = File.AppendText(info))
            {
                sw.WriteLine(@"""WEB"":[");
                sw.WriteLine(@"{");
                sw.WriteLine(@"""RecordTypeCode"": " + "\"" + PPDRecordTypeCode + "\"" + ",");
                sw.WriteLine(@"""TransactionCode"": " + "\"" + PPDTransactionCode + "\"" + ",");
                sw.WriteLine(@"""ReceivingDFIIdentification"": " + "\"" + PPDReceivingDFIIdentification + "\"" + ",");
                sw.WriteLine(@"""CheckDigit"": " + "\"" + PPDCheckDigit + "\"" + ",");
                sw.WriteLine(@"""DFIAccountNumber"": " + "\"" + PPDDFIAccountNumber + "\"" + ",");
                sw.WriteLine(@"""Amount"": " + "\"" + PPDAmount + "\"" + ",");
                sw.WriteLine(@"""IndividualIdentificationNumber"": " + "\"" + PPDIndividualIdentificationNumber + "\"" + ",");
                sw.WriteLine(@"""IndividualName"": " + "\"" + PPDIndividualName + "\"" + ",");
                sw.WriteLine(@"""PaymentTypeCode"": " + "\"" + PPDDiscretionaryData + "\"" + ",");
                sw.WriteLine(@"""AddendaRecordIndicator"": " + "\"" + PPDAddendaRecordIndicator + "\"" + ",");
                sw.WriteLine(@"""TraceNumber"": " + "\"" + PPDTraceNumber + "\"");
                sw.WriteLine(@"}");
                sw.WriteLine(@"],");
                sw.Close();
            }
        }
        public void AppendFHR(string info)
        {
            using (StreamWriter writer = new StreamWriter(info))
            {
                writer.WriteLine(@"{"); //start
                //********** FILE HEADER RECORD WRITE START **********//
                writer.WriteLine(@"""FileHeaderRecord"":[");
                writer.WriteLine(@"{");
                writer.WriteLine(@"""RecordTypeCode"": " + "\"" + FHRRecordTypeCode + "\"" + ",");
                writer.WriteLine(@"""ImmediateDestination"": " + "\"" + FHRImmediateDestination + "\"" + ",");
                writer.WriteLine(@"""ImmediateOrigin"": " + "\"" + FHRImmediateOrigin + "\"" + ",");
                writer.WriteLine(@"""FileCreationDate"": " + "\"" + FHRFileCreationDate + "\"" + ",");
                writer.WriteLine(@"""FileCreationTime"": " + "\"" + FHRFileCreationTime + "\"" + ",");
                writer.WriteLine(@"""FileIDModifier"": " + "\"" + FHRFileIDModifier + "\"" + ",");
                writer.WriteLine(@"""RecordSize"": " + "\"" + FHRRecordSize + "\"" + ",");
                writer.WriteLine(@"""BlockingFactor"": " + "\"" + FHRBlockingFactor + "\"" + ",");
                writer.WriteLine(@"""FormatCode"": " + "\"" + FHRFormatCode + "\"" + ",");
                writer.WriteLine(@"""ImmediateDestinationName"": " + "\"" + FHRImmediateDestinationName + "\"" + ",");
                writer.WriteLine(@"""ImmediateOriginName"": " + "\"" + FHRImmediateOriginName + "\"" + ",");
                writer.WriteLine(@"""ReferenceCode"": " + "\"" + FHRReferenceCode + "\"");
                writer.WriteLine(@"}");
                writer.WriteLine(@"],");
                //********** FILE HEADER RECORD WRITE END **********//
                writer.Close();
            }
        }
        public void AppendBHR(string info)
        {
            using (StreamWriter writer = File.AppendText(info))
            {
                writer.WriteLine(@"""BatchHeaderRecord"":[");
                writer.WriteLine(@"{");
                writer.WriteLine(@"""RecordTypeCode"": " + "\"" + BHRRecordTypeCode + "\"" + ",");
                writer.WriteLine(@"""ServiceClassCode"": " + "\"" + BHRServiceClassCode + "\"" + ",");
                writer.WriteLine(@"""CompanyName"": " + "\"" + BHRCompanyName + "\"" + ",");
                writer.WriteLine(@"""CompanyDiscretionaryData"": " + "\"" + BHRCompanyDiscretionaryData + "\"" + ",");
                writer.WriteLine(@"""CompanyIdentification"": " + "\"" + BHRCompanyIdentification + "\"" + ",");
                writer.WriteLine(@"""StandardEntryClassCode"": " + "\"" + BHRStandardEntryClassCode + "\"" + ",");
                writer.WriteLine(@"""CompanyEntryDescription"": " + "\"" + BHRCompanyEntryDescription + "\"" + ",");
                writer.WriteLine(@"""CompanyDescriptiveDate"": " + "\"" + BHRCompanyDescriptiveDate + "\"" + ",");
                writer.WriteLine(@"""EffectiveEntryDate"": " + "\"" + BHREffectiveEntryDate + "\"" + ",");
                writer.WriteLine(@"""SettlementDate"": " + "\"" + BHRSettlementDate + "\"" + ",");
                writer.WriteLine(@"""OriginatorStatusCode"": " + "\"" + BHROriginatorStatusCode + "\"" + ",");
                writer.WriteLine(@"""OriginatingDFIIdentification"": " + "\"" + BHROriginatingDFIIdentification + "\"" + ",");
                writer.WriteLine(@"""BatchNumber"": " + "\"" + BHRBatchNumber + "\"");
                writer.WriteLine(@"}");
                writer.WriteLine(@"],");
                writer.Close();
            }
        }
        public void AppendBCR(string info)
        {
            using (StreamWriter writer = File.AppendText(info))
            {
                writer.WriteLine(@"""BatchControlRecord"":[");
                writer.WriteLine(@"{");
                writer.WriteLine(@"""RecordTypeCode"": " + "\"" + BCRRecordTypeCode + "\"" + ",");
                writer.WriteLine(@"""ServiceClassCode"": " + "\"" + BCRServiceClassCode + "\"" + ",");
                writer.WriteLine(@"""EntryAddendaCount"": " + "\"" + BCREntryAddendaCount + "\"" + ",");
                writer.WriteLine(@"""EntryHash"": " + "\"" + BCREntryHash + "\"" + ",");
                writer.WriteLine(@"""TotalDebitEntryAmount"": " + "\"" + BCRTotalDebitEntryAmt + "\"" + ",");
                writer.WriteLine(@"""TotalCreditEntryAmount"": " + "\"" + BCRTotalCreditEntryAmt + "\"" + ",");
                writer.WriteLine(@"""CompanyIdentification"": " + "\"" + BCRCompanyIdentification + "\"" + ",");
                writer.WriteLine(@"""MessageAuthentificationCode"": " + "\"" + BCRMessageAuthCode + "\"" + ",");
                writer.WriteLine(@"""Reserved"": " + "\"" + BCRReserved + "\"" + ",");
                writer.WriteLine(@"""OriginatingDFIIdentification"": " + "\"" + BCROriginatingDFIIdentification + "\"" + ",");
                writer.WriteLine(@"""BatchNumber"": " + "\"" + BCRBatchNumber + "\"");
                writer.WriteLine(@"}");
                writer.WriteLine(@"],");
                writer.Close();
            }
        }
        public void AppendAddenda(string info)
        {
            using (StreamWriter sw = File.AppendText(info))
            {
                sw.WriteLine("\"" + BHRStandardEntryClassCode + "_Addenda_" + AddendaCount + "\"" + ":[");
                sw.WriteLine(@"{");
                sw.WriteLine(@"""RecordTypeCode"": " + "\"" + AddendaRecordTypeCode + "\"" + ",");
                sw.WriteLine(@"""AddendaTypeCode"": " + "\"" + AddendaAddendaTypeCode + "\"" + ",");
                sw.WriteLine(@"""PaymentRelatedInformation"": " + "\"" + AddendaPaymentRelatedInformation + "\"" + ",");
                sw.WriteLine(@"""AddendaSequenceNumber"": " + "\"" + AddendaAddendaSequenceNumber + "\"" + ",");
                sw.WriteLine(@"""EntryDetailSequenceNumber"": " + "\"" + AddendaEntrySequenceNumber + "\"");
                sw.WriteLine(@"}");
                sw.WriteLine(@"],");
                sw.Close();
            }
        }
        public void AppendFCR(string info)
        {
            using (StreamWriter writer = File.AppendText(info))
            {
                writer.WriteLine(@"""FileControlRecord"":[");
                writer.WriteLine(@"{");
                writer.WriteLine(@"""RecordTypeCode"": " + "\"" + FCRRecordTypeCode + "\"" + ",");
                writer.WriteLine(@"""BatchCount"": " + "\"" + FCRBatchCount + "\"" + ",");
                writer.WriteLine(@"""BlockCount"": " + "\"" + FCRBlockCount + "\"" + ",");
                writer.WriteLine(@"""EntryAddendaCount"": " + "\"" + FCREntryAddendaCount + "\"" + ",");
                writer.WriteLine(@"""EntryHash"": " + "\"" + FCREntryHash + "\"" + ",");
                writer.WriteLine(@"""TotalDebitEntryAmount"": " + "\"" + FCRTotalDebitEntryAmt + "\"" + ",");
                writer.WriteLine(@"""TotalCreditEntryAmount"": " + "\"" + FCRTotalCreditEntryAmt + "\"" + ",");
                writer.WriteLine(@"""Reserved"": " + "\"" + FCRReserved + "\"");
                writer.WriteLine(@"}");
                writer.WriteLine(@"]");
                writer.WriteLine(@"}"); //end
                writer.Close();
            }
        }
    }
}
