using IPE.SmsIrClient;
using IPE.SmsIrClient.Models.Results;
using System;
using System.Threading.Tasks;

namespace IPE.SmsIrSamples.DotNetCore.Report.Receive
{
    public static class GetReceiveArchiveReportSamples
    {
        /// <summary>
        /// گزارش پیامک‌های دریافتی آرشیو شده
        /// با فراخوانی متد زیر، گزارشی از پیامک‌های دریافتی در گذشته (تا انتهای روز قبل)، را مشاهده خواهید نمود.
        /// https://app.sms.ir/developer/help/receiveArchive
        /// </summary>
        public static async Task GetReceiveArchiveReportAsync()
        {
            try
            {
                // کلید ای‌پی‌آی ساخته‌شده در سامانه
                SmsIr smsIr = new SmsIr("uw7ppC4vGibwGFgAwLyRexHjyEb82yFFEXbbwOoOVT9GVMAQXoDO1vTkx59cOgoJ");

                // پارامتر اختیاری تاریخ شروع
                // زمان به صورت یونیکس تایم می‌باشد مثلا 1700598600 معادل 1 آذر 1402 می‌باشد
                int? fromDateUnixTime = null;

                // برای مثال برای گرفتن زمان یونیکس یک ماه پیش
                DateTime oneMonthBeforeDateTime = DateTime.UtcNow.AddMonths(-1);
                int oneMonthBeforeUnixTime = Convert.ToInt32(oneMonthBeforeDateTime.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds);

                // پارامتر اختیاری تاریخ پایان
                // زمان به صورت یونیکس تایم می‌باشد مثلا 1703190600 معادل 1 دی 1402 می‌باشد
                int? toDateUnixTime = null;

                // گزارش‌ها در صفحات حداکثر 100 تایی قابل دریافت می‌باشد.

                // شماره صفحه‌ - دارای پیشفرض 1
                int pageNumber = 1;

                // تعداد آیتم‌های هر صفحه - دارای پیشفرض 100
                // این عدد نمی‌تواند بیشتر از 100 باشد.
                int pageSize = 100;

                // انجام دریافت گزارش
                var response = await smsIr.GetArchivedReceivesAsync(pageNumber, pageSize, fromDateUnixTime, toDateUnixTime);

                // دریافت گزارش شما در اینجا با موفقیت انجام شده‌است.

                // گرفتن بخش دیتا خروجی
                ReceivedMessageResult[] messages = response.Data;

                // بررسی و نمایش پیامک‌های گزارش
                string reportDescription = string.Empty;
                foreach (var message in messages)
                {
                    // شماره موبایل فرستنده
                    long mobile = message.Mobile;

                    // خط دریافت کننده برای ارسال
                    long lineNumber = message.Number;

                    // متن پیام فرستاده شده
                    string messageText = message.MessageText;

                    // زمان دریافت
                    int receivedUnixTime = message.ReceivedDateTime;
                    DateTime receivedDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(receivedUnixTime).ToLocalTime();

                    // خلاصه‌ای از هر دریافت
                    string messageDescription = $"Mobile: {mobile}" +
                        $" - Line Number: {lineNumber}" +
                        $" - Received DateTime: {receivedDateTime}" +
                        $" - Message Text: {messageText}" +
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