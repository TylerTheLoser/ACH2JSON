using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;

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
                StringBuilder sb = new StringBuilder();
                StringWriter ssw = new StringWriter(sb);
                JsonWriter writer = new JsonTextWriter(sw);
                writer.Formatting = Formatting.Indented;
                writer.WriteStartObject();
                writer.WritePropertyName("PPD_" + PaymentCount);
                writer.WriteStartArray();
                writer.WriteStartObject();
                writer.WritePropertyName("RecordTypeCode");
                writer.WriteValue(PPDRecordTypeCode);
                writer.WritePropertyName("TransactionCode");
                writer.WriteValue(PPDTransactionCode);
                writer.WritePropertyName("ReceivingDFIIdentification");
                writer.WriteValue(PPDReceivingDFIIdentification);
                writer.WritePropertyName("CheckDigit");
                writer.WriteValue(PPDCheckDigit);
                writer.WritePropertyName("DFIAccountNumber");
                writer.WriteValue(PPDDFIAccountNumber);
                writer.WritePropertyName("Amount");
                writer.WriteValue(PPDAmount);
                writer.WritePropertyName("IndividualIdentificationNumber");
                writer.WriteValue(PPDIndividualIdentificationNumber);
                writer.WritePropertyName("IndividualName");
                writer.WriteValue(PPDIndividualName);
                writer.WritePropertyName("DiscretionaryData");
                writer.WriteValue(PPDDiscretionaryData);
                writer.WritePropertyName("AddendaRecordIndicator");
                writer.WriteValue(PPDAddendaRecordIndicator);
                writer.WritePropertyName("TraceNumber");
                writer.WriteValue(PPDTraceNumber);
                writer.WriteEndObject();
                writer.WriteEndArray();
                writer.WriteEndObject();
                sw.WriteLine(sb.ToString());
                sw.Close();
            }
        }
        public void AppendCCD(string info)
        {
            using (StreamWriter sw = File.AppendText(info))
            {
                StringBuilder sb = new StringBuilder();
                StringWriter ssw = new StringWriter(sb);
                JsonWriter writer = new JsonTextWriter(sw);
                writer.Formatting = Formatting.Indented;
                writer.WriteStartObject();
                writer.WritePropertyName("CCD_" + PaymentCount);
                writer.WriteStartArray();
                writer.WriteStartObject();
                writer.WritePropertyName("RecordTypeCode");
                writer.WriteValue(PPDRecordTypeCode);
                writer.WritePropertyName("TransactionCode");
                writer.WriteValue(PPDTransactionCode);
                writer.WritePropertyName("ReceivingDFIIdentification");
                writer.WriteValue(PPDReceivingDFIIdentification);
                writer.WritePropertyName("CheckDigit");
                writer.WriteValue(PPDCheckDigit);
                writer.WritePropertyName("DFIAccountNumber");
                writer.WriteValue(PPDDFIAccountNumber);
                writer.WritePropertyName("Amount");
                writer.WriteValue(PPDAmount);
                writer.WritePropertyName("IdentificationNumber");
                writer.WriteValue(PPDIndividualIdentificationNumber);
                writer.WritePropertyName("ReceivingCompanyName");
                writer.WriteValue(PPDIndividualName);
                writer.WritePropertyName("DiscretionaryData");
                writer.WriteValue(PPDDiscretionaryData);
                writer.WritePropertyName("AddendaRecordIndicator");
                writer.WriteValue(PPDAddendaRecordIndicator);
                writer.WritePropertyName("TraceNumber");
                writer.WriteValue(PPDTraceNumber);
                writer.WriteEndObject();
                writer.WriteEndArray();
                writer.WriteEndObject();
                sw.WriteLine(sb.ToString());
                sw.Close();
            }
        }
        public void AppendTEL(string info)
        {
            using (StreamWriter sw = File.AppendText(info))
            {
                StringBuilder sb = new StringBuilder();
                StringWriter ssw = new StringWriter(sb);
                JsonWriter writer = new JsonTextWriter(sw);
                writer.Formatting = Formatting.Indented;
                writer.WriteStartObject();
                writer.WritePropertyName("TEL_" + PaymentCount);
                writer.WriteStartArray();
                writer.WriteStartObject();
                writer.WritePropertyName("RecordTypeCode");
                writer.WriteValue(PPDRecordTypeCode);
                writer.WritePropertyName("TransactionCode");
                writer.WriteValue(PPDTransactionCode);
                writer.WritePropertyName("ReceivingDFIIdentification");
                writer.WriteValue(PPDReceivingDFIIdentification);
                writer.WritePropertyName("CheckDigit");
                writer.WriteValue(PPDCheckDigit);
                writer.WritePropertyName("DFIAccountNumber");
                writer.WriteValue(PPDDFIAccountNumber);
                writer.WritePropertyName("Amount");
                writer.WriteValue(PPDAmount);
                writer.WritePropertyName("IndividualIdentificationNumber");
                writer.WriteValue(PPDIndividualIdentificationNumber);
                writer.WritePropertyName("IndividualName");
                writer.WriteValue(PPDIndividualName);
                writer.WritePropertyName("DiscretionaryData");
                writer.WriteValue(PPDDiscretionaryData);
                writer.WritePropertyName("AddendaRecordIndicator");
                writer.WriteValue(PPDAddendaRecordIndicator);
                writer.WritePropertyName("TraceNumber");
                writer.WriteValue(PPDTraceNumber);
                writer.WriteEndObject();
                writer.WriteEndArray();
                writer.WriteEndObject();
                sw.WriteLine(sb.ToString());
                sw.Close();
            }
        }
        public void AppendCTX(string info)
        {
            using (StreamWriter sw = File.AppendText(info))
            {
                StringBuilder sb = new StringBuilder();
                StringWriter ssw = new StringWriter(sb);
                JsonWriter writer = new JsonTextWriter(sw);
                writer.Formatting = Formatting.Indented;
                writer.WriteStartObject();
                writer.WritePropertyName("CTX_" + PaymentCount);
                writer.WriteStartArray();
                writer.WriteStartObject();
                writer.WritePropertyName("RecordTypeCode");
                writer.WriteValue(PPDRecordTypeCode);
                writer.WritePropertyName("TransactionCode");
                writer.WriteValue(PPDTransactionCode);
                writer.WritePropertyName("ReceivingDFIIdentification");
                writer.WriteValue(PPDReceivingDFIIdentification);
                writer.WritePropertyName("CheckDigit");
                writer.WriteValue(PPDCheckDigit);
                writer.WritePropertyName("DFIAccountNumber");
                writer.WriteValue(PPDDFIAccountNumber);
                writer.WritePropertyName("TotalAmount");
                writer.WriteValue(PPDAmount);
                writer.WritePropertyName("IdentificationNumber");
                writer.WriteValue(PPDIndividualIdentificationNumber);
                writer.WritePropertyName("NumberOfAddendaRecords");
                writer.WriteValue(PPDIndividualName);
                writer.WritePropertyName("ReceivingCompanyName");
                writer.WriteValue(PPDDiscretionaryData);
                writer.WritePropertyName("Reserved");
                writer.WriteValue(PPDAddendaRecordIndicator);
                writer.WritePropertyName("DiscretionaryData");
                writer.WriteValue(PPDTraceNumber);
                writer.WritePropertyName("AddendaRecordIndicator");
                writer.WriteValue(CTXField12);
                writer.WritePropertyName("TraceNumber");
                writer.WriteValue(CTXField13);
                writer.WriteEndObject();
                writer.WriteEndArray();
                writer.WriteEndObject();
                sw.WriteLine(sb.ToString());
                sw.Close();
            }
        }
        public void AppendWEB(string info)
        {
            using (StreamWriter sw = File.AppendText(info))
            {
                StringBuilder sb = new StringBuilder();
                StringWriter ssw = new StringWriter(sb);
                JsonWriter writer = new JsonTextWriter(sw);
                writer.Formatting = Formatting.Indented;
                writer.WriteStartObject();
                writer.WritePropertyName("WEB_" + PaymentCount);
                writer.WriteStartArray();
                writer.WriteStartObject();
                writer.WritePropertyName("RecordTypeCode");
                writer.WriteValue(PPDRecordTypeCode);
                writer.WritePropertyName("TransactionCode");
                writer.WriteValue(PPDTransactionCode);
                writer.WritePropertyName("ReceivingDFIIdentification");
                writer.WriteValue(PPDReceivingDFIIdentification);
                writer.WritePropertyName("CheckDigit");
                writer.WriteValue(PPDCheckDigit);
                writer.WritePropertyName("DFIAccountNumber");
                writer.WriteValue(PPDDFIAccountNumber);
                writer.WritePropertyName("Amount");
                writer.WriteValue(PPDAmount);
                writer.WritePropertyName("IndividualIdentificationNumber");
                writer.WriteValue(PPDIndividualIdentificationNumber);
                writer.WritePropertyName("IndividualName");
                writer.WriteValue(PPDIndividualName);
                writer.WritePropertyName("PaymentTypeCode");
                writer.WriteValue(PPDDiscretionaryData);
                writer.WritePropertyName("AddendaRecordIndicator");
                writer.WriteValue(PPDAddendaRecordIndicator);
                writer.WritePropertyName("TraceNumber");
                writer.WriteValue(PPDTraceNumber);
                writer.WriteEndObject();
                writer.WriteEndArray();
                writer.WriteEndObject();
                sw.WriteLine(sb.ToString());
                sw.Close();
            }
        }
        public void AppendFHR(string info)
        {
            using (StreamWriter sw = new StreamWriter(info))
            {
                StringBuilder sb = new StringBuilder();
                StringWriter ssw = new StringWriter(sb);
                JsonWriter writer = new JsonTextWriter(sw);
                writer.Formatting = Formatting.Indented;
                writer.WriteStartObject();
                writer.WritePropertyName("FileHeaderRecord");
                writer.WriteStartArray();
                writer.WriteStartObject();
                writer.WritePropertyName("RecordTypeCode");
                writer.WriteValue(FHRRecordTypeCode);
                writer.WritePropertyName("ImmediateDestination");
                writer.WriteValue(FHRImmediateDestination);
                writer.WritePropertyName("ImmediateOrigin");
                writer.WriteValue(FHRImmediateOrigin);
                writer.WritePropertyName("FileCreationDate");
                writer.WriteValue(FHRFileCreationDate);
                writer.WritePropertyName("FileCreationTime");
                writer.WriteValue(FHRFileCreationTime);
                writer.WritePropertyName("FileIDModifier");
                writer.WriteValue(FHRFileIDModifier);
                writer.WritePropertyName("RecordSize");
                writer.WriteValue(FHRRecordSize);
                writer.WritePropertyName("BlockingFactor");
                writer.WriteValue(FHRBlockingFactor);
                writer.WritePropertyName("FormatCode");
                writer.WriteValue(FHRFormatCode);
                writer.WritePropertyName("ImmediateDestinationName");
                writer.WriteValue(FHRImmediateDestinationName);
                writer.WritePropertyName("ImmediateOriginName");
                writer.WriteValue(FHRImmediateOriginName);
                writer.WritePropertyName("ReferenceCode");
                writer.WriteValue(FHRReferenceCode);
                writer.WriteEndObject();
                writer.WriteEndArray();
                writer.WriteEndObject(); 
                sw.WriteLine(sb.ToString());
                sw.Close();
            }
        }
        public void AppendBHR(string info)
        {
            using (StreamWriter sw = File.AppendText(info))
            {
                StringBuilder sb = new StringBuilder();
                StringWriter ssw = new StringWriter(sb);
                JsonWriter writer = new JsonTextWriter(sw);
                writer.Formatting = Formatting.Indented;
                writer.WriteStartObject();
                writer.WritePropertyName("BatchHeaderRecord");
                writer.WriteStartArray();
                writer.WriteStartObject();
                writer.WritePropertyName("RecordTypeCode");
                writer.WriteValue(BHRRecordTypeCode);
                writer.WritePropertyName("ServiceClassCode");
                writer.WriteValue(BHRServiceClassCode);
                writer.WritePropertyName("CompanyName");
                writer.WriteValue(BHRCompanyName);
                writer.WritePropertyName("CompanyDiscretionaryData");
                writer.WriteValue(BHRCompanyDiscretionaryData);
                writer.WritePropertyName("CompanyIdentification");
                writer.WriteValue(BHRCompanyIdentification);
                writer.WritePropertyName("StandardEntryClassCode");
                writer.WriteValue(BHRStandardEntryClassCode);
                writer.WritePropertyName("CompanyEntryDescription");
                writer.WriteValue(BHRCompanyEntryDescription);
                writer.WritePropertyName("CompanyDescriptiveDate");
                writer.WriteValue(BHRCompanyDescriptiveDate);
                writer.WritePropertyName("EffectiveEntryDate");
                writer.WriteValue(BHREffectiveEntryDate);
                writer.WritePropertyName("SettlementDate");
                writer.WriteValue(BHRSettlementDate);
                writer.WritePropertyName("OriginatorStatusCode");
                writer.WriteValue(BHROriginatorStatusCode);
                writer.WritePropertyName("OriginatingDFIIdentification");
                writer.WriteValue(BHROriginatingDFIIdentification);
                writer.WritePropertyName("BatchNumber");
                writer.WriteValue(BHRBatchNumber);
                writer.WriteEndObject();
                writer.WriteEndArray();
                writer.WriteEndObject();
                sw.WriteLine(sb.ToString());
                sw.Close();
            }
        }
        public void AppendBCR(string info)
        {
            using (StreamWriter sw = File.AppendText(info))
            {
                StringBuilder sb = new StringBuilder();
                StringWriter ssw = new StringWriter(sb);
                JsonWriter writer = new JsonTextWriter(sw);
                writer.Formatting = Formatting.Indented;
                writer.WriteStartObject();
                writer.WritePropertyName("BatchControlRecord");
                writer.WriteStartArray();
                writer.WriteStartObject();
                writer.WritePropertyName("RecordTypeCode");
                writer.WriteValue(BCRRecordTypeCode);
                writer.WritePropertyName("ServiceClassCode");
                writer.WriteValue(BCRServiceClassCode);
                writer.WritePropertyName("EntryAddendaCount");
                writer.WriteValue(BCREntryAddendaCount);
                writer.WritePropertyName("EntryHash");
                writer.WriteValue(BCREntryHash);
                writer.WritePropertyName("TotalDebitEntryAmount");
                writer.WriteValue(BCRTotalDebitEntryAmt);
                writer.WritePropertyName("TotalCreditEntryAmount");
                writer.WriteValue(BCRTotalCreditEntryAmt);
                writer.WritePropertyName("CompanyIdentification");
                writer.WriteValue(BCRCompanyIdentification);
                writer.WritePropertyName("MessageAuthenticationCode");
                writer.WriteValue(BCRMessageAuthCode);
                writer.WritePropertyName("Reserved");
                writer.WriteValue(BCRReserved);
                writer.WritePropertyName("OriginatingDFIIdentification");
                writer.WriteValue(BCROriginatingDFIIdentification);
                writer.WritePropertyName("BatchNumber");
                writer.WriteValue(BCRBatchNumber);
                writer.WriteEndObject();
                writer.WriteEndArray();
                writer.WriteEndObject();
                sw.WriteLine(sb.ToString());
                sw.Close();
            }
        }
        public void AppendAddenda(string info)
        {
            using (StreamWriter sw = File.AppendText(info))
            {
                StringBuilder sb = new StringBuilder();
                StringWriter ssw = new StringWriter(sb);
                JsonWriter writer = new JsonTextWriter(sw);
                writer.Formatting = Formatting.Indented;
                writer.WriteStartObject();
                writer.WritePropertyName("Addenda_" + AddendaCount);
                writer.WriteStartArray();
                writer.WriteStartObject();
                writer.WritePropertyName("RecordTypeCode");
                writer.WriteValue(AddendaRecordTypeCode);
                writer.WritePropertyName("AddendaTypeCode");
                writer.WriteValue(AddendaAddendaTypeCode);
                writer.WritePropertyName("PaymentRelatedInformation");
                writer.WriteValue(AddendaPaymentRelatedInformation);
                writer.WritePropertyName("AddendaSequenceNumber");
                writer.WriteValue(AddendaAddendaSequenceNumber);
                writer.WritePropertyName("EntryDetailSequenceNumber");
                writer.WriteValue(AddendaEntrySequenceNumber);
                writer.WriteEndObject();
                writer.WriteEndArray();
                writer.WriteEndObject();
                sw.WriteLine(sb.ToString());
                sw.Close();
            }
        }
        public void AppendFCR(string info)
        {
            using (StreamWriter sw = File.AppendText(info))
            {
                StringBuilder sb = new StringBuilder();
                StringWriter ssw = new StringWriter(sb);
                JsonWriter writer = new JsonTextWriter(sw);
                writer.Formatting = Formatting.Indented;
                writer.WriteStartObject();
                writer.WritePropertyName("FileControlRecord");
                writer.WriteStartArray();
                writer.WriteStartObject();
                writer.WritePropertyName("RecordTypeCode");
                writer.WriteValue(FCRRecordTypeCode);
                writer.WritePropertyName("BatchCount");
                writer.WriteValue(FCRBatchCount);
                writer.WritePropertyName("BlockCount");
                writer.WriteValue(FCRBlockCount);
                writer.WritePropertyName("EntryAddendaCount");
                writer.WriteValue(FCREntryAddendaCount);
                writer.WritePropertyName("EntryHash");
                writer.WriteValue(FCREntryHash);
                writer.WritePropertyName("TotalDebitEntryAmount");
                writer.WriteValue(FCRTotalDebitEntryAmt);
                writer.WritePropertyName("TotalCreditEntryAmount");
                writer.WriteValue(FCRTotalCreditEntryAmt);
                writer.WritePropertyName("Reserved");
                writer.WriteValue(FCRReserved);
                writer.WriteEndObject();
                writer.WriteEndArray();
                //writer.WriteEndObject();
                writer.WriteEnd();
                sw.WriteLine(sb.ToString());
                sw.Close();
            }
        }
    }
}
