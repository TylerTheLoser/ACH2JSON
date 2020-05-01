using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace ACH2JSON
{
    class CreateJSON //where the JSON will be created. should we do a get set? how will we pass the values?
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

        //********** ADDENDA START **********//
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

        public void CreateThatFile()
        {
            Console.WriteLine("FILE CREATION STARTED");
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "JSON Files | *.json";
            sfd.DefaultExt = "json";
            string sfdname = sfd.FileName;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Path.GetFileName(sfd.FileName);
            }
            using (StreamWriter writer = new StreamWriter(sfd.FileName))
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

                //********** BATCH HEADER RECORD WRITE START **********//
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
                //********** BATCH HEADER RECORD WRITE STOP **********//

                //this needs to be generic to reuse
                //********** START **********//
                writer.WriteLine("\"" + BHRStandardEntryClassCode + "\"" + ":[");
                writer.WriteLine(@"{");
                writer.WriteLine(@"""RecordTypeCode"": " + "\"" + PPDRecordTypeCode + "\"" + ",");
                writer.WriteLine(@"""TransactionCode"": " + "\"" + PPDTransactionCode + "\"" + ",");
                writer.WriteLine(@"""ReceivingDFIIdentification"": " + "\"" + PPDReceivingDFIIdentification + "\"" + ",");
                writer.WriteLine(@"""CheckDigit"": " + "\"" + PPDCheckDigit + "\"" + ",");
                writer.WriteLine(@"""DFIAccountNumber"": " + "\"" + PPDDFIAccountNumber + "\"" + ",");
                writer.WriteLine(@"""Amount"": " + "\"" + PPDAmount + "\"" + ",");
                writer.WriteLine(@"""IndividualIdentificationNumber"": " + "\"" + PPDIndividualIdentificationNumber + "\"" + ",");
                writer.WriteLine(@"""IndividualName"": " + "\"" + PPDIndividualName + "\"" + ",");
                writer.WriteLine(@"""DiscretionaryData"": " + "\"" + PPDDiscretionaryData + "\"" + ",");
                writer.WriteLine(@"""AddendaRecordIndicator"": " + "\"" + PPDAddendaRecordIndicator + "\"" + ",");
                writer.WriteLine(@"""TraceNumber"": " + "\"" + PPDTraceNumber + "\"");
                writer.WriteLine(@"}");
                writer.WriteLine(@"],");
                //********** END **********//

                //********** BATCH CONTROL HEADER START **********//
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
                //********** BATCH CONTROL HEADER END **********//

                //********** FILE CONTROL RECORD START **********//
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
                //********** FILE CONTROL RECORD END **********//

                writer.WriteLine(@"}"); //end
                
            }
        }
    }
}
