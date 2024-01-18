using IPE.SmsIrClient;
using System;
using System.Threading.Tasks;

namespace IPE.SmsIrSamples.DotNetCore.Report.Account
{
    public static class GetCurrentCreditSamples
    {
        /// <summary>
        /// دریافت مقدار اعتبار فعلی
        /// برای مشاهده مقدار اعتبار فعلی از متد زیر استفاده نمایید.
        /// https://app.sms.ir/developer/help/credit
        /// </summary>
        public static async Task GetCurrentCreditAsync()
        {
            try
            {
                // کلید ای‌پی‌آی ساخته‌شده در سامانه
                SmsIr smsIr = new SmsIr("uw7ppC4vGibwGFgAwLyRexHjyEb82yFFEXbbwOoOVT9GVMAQXoDO1vTkx59cOgoJ");

                // انجام دریافت اعتبار
                var response = await smsIr.GetCreditAsync();

                // دریافت اعتبار شما در اینجا با موفقیت انجام شده‌است.

                // گرفتن بخش دیتا خروجی
                decimal credit = response.Data;

                // توضیحات خروجی
                string description = $"Credit: {credit}";

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