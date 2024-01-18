using IPE.SmsIrClient;
using IPE.SmsIrClient.Models.Results;
using System;
using System.Threading.Tasks;

namespace IPE.SmsIrSamples.DotNetCore.Send
{
    public static class RemoveScheduledPackSamples
    {
        /// <summary>
        /// حذف ارسال زمانبندی شده
        /// به‌منظور حذف و انصراف از ارسال زمانبندی شده می‌توانید از متد زیر استفاده نمایید.
        /// در این متد، از شناسه مجموعه ارسال استفاده می‌شود.
        /// https://app.sms.ir/developer/help/deleteScheduled
        /// </summary>
        public static async Task RemoveScheduledPackAsync()
        {
            try
            {
                // کلید ای‌پی‌آی ساخته‌شده در سامانه
                SmsIr smsIr = new SmsIr("uw7ppC4vGibwGFgAwLyRexHjyEb82yFFEXbbwOoOVT9GVMAQXoDO1vTkx59cOgoJ");

                // شناسه مجموعه ارسال
                Guid packId = new Guid("86D96B0E-FD89-4C19-B303-C0B4D3874063");

                // انجام ارسال گروهی
                var response = await smsIr.RemoveScheduledMessagesAsync(packId);

                // ارسال شما در اینجا با موفقیت انجام شده‌است.

                // گرفتن بخش دیتا خروجی
                RemoveScheduledMessagesResult result = response.Data;

                // مقدار اعتبار بازگشتی
                decimal returnedCreditCount = result.ReturnedCreditCount;

                // تعداد پیامک‌ها
                decimal smsCount = result.SmsCount;

                string resultDescription = "Your scheduled pack was successfully removed." +
                    $"\n - Returned Credit Count: {returnedCreditCount} " +
                    $"\n - Sms Count: {smsCount} ";

                await Console.Out.WriteLineAsync(resultDescription);
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