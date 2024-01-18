using IPE.SmsIrClient;
using System;
using System.Threading.Tasks;

namespace IPE.SmsIrSamples.DotNetCore.Report.Account
{
    public static class GetLinesSamples
    {
        /// <summary>
        /// دریافت لیست خطوط
        /// با استفاده از این متد، لیست خطوط آماده استفاده برای ارسال، قابل مشاهده است.
        /// https://app.sms.ir/developer/help/line
        /// </summary>
        public static async Task GetLinesAsync()
        {
            try
            {
                // کلید ای‌پی‌آی ساخته‌شده در سامانه
                SmsIr smsIr = new SmsIr("uw7ppC4vGibwGFgAwLyRexHjyEb82yFFEXbbwOoOVT9GVMAQXoDO1vTkx59cOgoJ");

                // انجام دریافت خطوط
                var response = await smsIr.GetLinesAsync();

                // دریافت خطوط شما در اینجا با موفقیت انجام شده‌است.

                // گرفتن بخش دیتا خروجی
                long[] lines = response.Data;

                // توضیحات خروجی
                string description = $"Lines: [{string.Join(", ", lines)}]";

                await Console.Out.WriteLineAsync(description);
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