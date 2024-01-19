using IPE.SmsIrClient;
using IPE.SmsIrClient.Models.Results;
using IPE.SmsIrSamples.DotNetCore.Report.Models;
using System;
using System.Threading.Tasks;

namespace IPE.SmsIrSamples.DotNetCore.Report.Send
{
    public static class GetSendPackSamples
    {
        /// <summary>
        /// گزارش مجموعه ارسال
        /// شما می‌توانید با استفاده از شناسه مجموعه ارسال، گزارشی از پیامک‌های ارسالی در آن درخواست به‌ همراه وضعیت‌هایشان را دریافت نمایید.
        /// https://app.sms.ir/developer/help/sendPack
        /// </summary>
        public static async Task GetSendPackLiveReportAsync()
        {
            try
            {
                // کلید ای‌پی‌آی ساخته‌شده در سامانه
                SmsIr smsIr = new SmsIr("uw7ppC4vGibwGFgAwLyRexHjyEb82yFFEXbbwOoOVT9GVMAQXoDO1vTkx59cOgoJ");

                // شناسه مجموعه ارسال
                Guid packId = new Guid("86D96B0E-FD89-4C19-B303-C0B4D3874063");

                // انجام دریافت گزارش
                var response = await smsIr.GetReportAsync(packId);

                // دریافت گزارش شما در اینجا با موفقیت انجام شده‌است.

                // گرفتن بخش دیتا خروجی
                MessageReportResult[] messages = response.Data;

                // بررسی و نمایش پیامک‌های گزارش
                string reportDescription = $"Pack Id: {packId}" + Environment.NewLine + Environment.NewLine;
                foreach (var message in messages)
                {
                    // شناسه پیامک
                    int messageId = message.MessageId;

                    // خط استفاده شده برای ارسال
                    long lineNumber = message.LineNumber;

                    // شماره موبایل مقصد
                    long mobile = message.Mobile;

                    // پیام فرستاده شده
                    string messageText = message.MessageText;

                    // زمان ارسال
                    int sendDateTimeInUnix = message.SendDateTime;

                    // وضعیت دریافت
                    DeliveryState delivertState = message.DeliveryState.HasValue ? (DeliveryState)message.DeliveryState : DeliveryState.Pending;
                    string deliveryStateDescription = delivertState.ToString();

                    // زمان دریافت
                    int? deliveryUnixTime = message.DeliveryDateTime;
                    DateTime? deliveryDateTime = deliveryUnixTime.HasValue ?
                        new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(deliveryUnixTime.Value).ToLocalTime() : default(DateTime?);

                    // هزینه ارسال این پیامک
                    decimal cost = message.Cost;

                    // خلاصه‌ای از هر پیامک
                    string messageDescription = $"Mobile: {mobile}" +
                        $" - Delivery State: {deliveryStateDescription}" +
                        $" - Delivery DateTime: {deliveryDateTime?.ToString("dddd, dd MMMM yyyy HH:mm:ss") ?? "-"}" +
                        Environment.NewLine;

                    reportDescription += messageDescription;
                }

                await Console.Out.WriteLineAsync(reportDescription);
            }
            catch (Exception ex) // درخواست ناموفق
            {
                // جدول توضیحات کد وضعیت
                // https://app.sms.ir/developer/help/statusCode

                string errorName = ex.GetType().Name;
                string errorNameDescription;
                switch (errorName)
                {
                    case "UnauthorizedException":
                        errorNameDescription = "The provided token is not valid or access is denied.";
                        break;

                    case "LogicalException":
                        errorNameDescription = "Please check and correct the request parameters.";
                        break;

                    case "TooManyRequestException":
                        errorNameDescription = "The request count has exceeded the allowed limit.";
                        break;

                    case "UnexpectedException":
                    case "InvalidOperationException":
                        errorNameDescription = "An unexpected error occurred on the remote server.";
                        break;

                    default:
                        errorNameDescription = "Unable to send the request due to an unspecified error.";
                        break;
                }

                var errorDescription = "There is a problem with the request." +
                    $"\n - Error: {errorName} - {errorNameDescription} - {ex.Message}";

                await Console.Out.WriteLineAsync(errorDescription);
            }
        }
    }
}