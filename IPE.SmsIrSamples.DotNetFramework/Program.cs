using IPE.SmsIrSamples.DotNetCore.Report.Account;
using IPE.SmsIrSamples.DotNetCore.Report.Receive;
using IPE.SmsIrSamples.DotNetCore.Report.Send;
using IPE.SmsIrSamples.DotNetCore.Send;
using System;
using System.Threading.Tasks;

namespace IPE.SmsIrSamples.DotNetFramework
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            ///////////////// sends

            //await SendVerifySamples.SendVerifyAsync();
            //await SendBulkSamples.SendBulkAsync();
            //await SendLikeToLikeSamples.SendLikeToLikeAsync();

            //await RemoveScheduledPackSamples.RemoveScheduledPackAsync();


            ///////////////// send reports

            ////// get send reports
            //await GetSendLiveReportSamples.GetSendLiveReportAsync();
            //await GetSendArchiveReportSamples.GetSendArchiveReportAsync();

            ////// get single message
            //await GetSingleMessageSamples.GetSingleMessageReportAsync();

            ////// get send packs
            //await GetSendPackSamples.GetSendPackLiveReportAsync();
            //await GetSendPacksSamples.GetSendPacksLiveReportAsync();


            ///////////////// receive reports

            //await GetLatestReceivesSamples.GetLatestReceivesAsync();
            //await GetReceiveLiveReportSamples.GetReceiveLiveReportAsync();
            //await GetReceiveArchiveReportSamples.GetReceiveArchiveReportAsync();


            ///////////////// account

            //await GetCurrentCreditSamples.GetCurrentCreditAsync();
            //await GetLinesSamples.GetLinesAsync();


            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}