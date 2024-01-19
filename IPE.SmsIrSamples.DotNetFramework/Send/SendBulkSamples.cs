using IPE.SmsIrClient;
using IPE.SmsIrClient.Models.Results;
using System;
using System.Threading.Tasks;

namespace IPE.SmsIrSamples.DotNetCore.Send
{
    public static class SendBulkSamples
    {
        /// <summary>
        /// ارسال گروهی
        /// این متد برای ارسال یک متن پیامک به گروهی از شماره موبایل ها مورد استفاده قرار میگیرد.
        /// https://app.sms.ir/developer/help/bulk
        /// </summary>
        public static async Task SendBulkAsync()
        {
            try
            {
                // کلید ای‌پی‌آی ساخته‌شده در سامانه
                SmsIr smsIr = new SmsIr("uw7ppC4vGibwGFgAwLyRexHjyEb82yFFEXbbwOoOVT9GVMAQXoDO1vTkx59cOgoJ");

                // شماره خط ارسالی - موجود در قسمت شماره‌های من
                long lineNumber = 95007079000006;

                // متن ارسال گروهی
                string messageText = "سرویس پیامکی ایده پردازان با بیش از یک دهه سابقه همراه شماست" +
                    "\nSMS.ir";

                // شماره موبایل‌های مقصد
                string[] mobiles = { "9120000000", "9120000001" };

                // پارامتر اختیاری زمان ارسال
                // زمان ارسال به صورت یونیکس تایم می‌باشد مثلا 1704094200
                int? sendDateTime = null;

                // انجام ارسال گروهی
                var response = await smsIr.BulkSendAsync(lineNumber, messageText, mobiles, sendDateTime);

                // ارسال شما در اینجا با موفقیت انجام شده‌است.

                // گرفتن بخش دیتا خروجی
                SendResult sendResult = response.Data;

                // شناسه مجموعه ارسال ارسال شده
                Guid packId = sendResult.PackId;

                // لیست شناسه پیامک‌های ارسال شده
                int?[] messageIds = sendResult.MessageIds;

                // هزینه ارسال
                decimal cost = sendResult.Cost;

                string resultDescription = "Your message was sent successfully." +
                    $"\n - Pack Id: {packId} " +
                    $"\n - Message Ids: [{string.Join(", ", messageIds)}] " +
                    $"\n - Cost: {cost}";

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

        /// <summary>
        /// ارسال گروهی
        /// این متد برای ارسال یک متن پیامک به گروهی از شماره موبایل ها مورد استفاده قرار میگیرد.
        /// https://app.sms.ir/developer/help/bulk
        /// </summary>
        public static void SendBulk()
        {
            try
            {
                // کلید ای‌پی‌آی ساخته‌شده در سامانه
                SmsIr smsIr = new SmsIr("uw7ppC4vGibwGFgAwLyRexHjyEb82yFFEXbbwOoOVT9GVMAQXoDO1vTkx59cOgoJ");

                // شماره خط ارسالی - موجود در قسمت شماره‌های من
                long lineNumber = 95007079000006;

                // متن ارسال گروهی
                string messageText = "سرویس پیامکی ایده پردازان با بیش از یک دهه سابقه همراه شماست" +
                    "\nSMS.ir";

                // شماره موبایل‌های مقصد
                string[] mobiles = { "9120000000", "9120000001" };

                // پارامتر اختیاری زمان ارسال
                // زمان ارسال به صورت یونیکس تایم می‌باشد مثلا 1704094200
                int? sendDateTime = null;

                // انجام ارسال گروهی
                var response = smsIr.BulkSend(lineNumber, messageText, mobiles, sendDateTime);

                // ارسال شما در اینجا با موفقیت انجام شده‌است.

                // گرفتن بخش دیتا خروجی
                SendResult sendResult = response.Data;

                // شناسه مجموعه ارسال ارسال شده
                Guid packId = sendResult.PackId;

                // لیست شناسه پیامک‌های ارسال شده
                int?[] messageIds = sendResult.MessageIds;

                // هزینه ارسال
                decimal cost = sendResult.Cost;

                string resultDescription = "Your message was sent successfully." +
                    $"\n - Pack Id: {packId} " +
                    $"\n - Message Ids: [{string.Join(", ", messageIds)}] " +
                    $"\n - Cost: {cost}";

                Console.Out.WriteLine(resultDescription);
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

                Console.Out.WriteLine(errorDescription);
            }
        }
    }
}