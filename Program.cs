using System;
using System.Threading.Tasks;
using WTelegram; 
using TL;

class Program
{
    // دالة الإعدادات لاستقبال البيانات والتحقق
    static string Config(string what)
    {
        switch (what)
        {
            case "api_id": return "29335651";
            case "api_hash": return "317d8b6c91313ae27c7ef9c5d961677f";
            case "phone_number": return "+9647801095271";
            case "verification_code":
                Console.Write("أدخل كود التحقق الذي وصلك في تليجرام: ");
                return Console.ReadLine();
            default: return null;
        }
    }

    static async Task Main(string[] args)
    {
        // استخدام الصيغة المتوافقة مع C# 7.3 (باستخدام الأقواس)
        using (var client = new Client(Config))
        {
            try
            {
                // بدء عملية تسجيل الدخول
                await client.LoginUserIfNeeded();

                Console.Clear();
                Console.WriteLine("✅ تم تسجيل الدخول بنجاح!");
                Console.WriteLine("🚀 البوت يعمل الآن ويحدث الوقت كل 30 ثانية...");
                Console.WriteLine("⚠️ لا تغلق هذه النافذة لضمان استمرار عمل البوت.");

                while (true)
                {
                    try
                    {
                        // جلب الوقت الحالي بتنسيق جميل
                        string timeLabel = "" + DateTime.Now.ToString("hh:mm");

                        // تحديث الاسم الأول في حسابك
                        await client.Account_UpdateProfile(first_name: timeLabel);

                        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] تم تحديث الاسم إلى: {timeLabel}");

                        // الانتظار لمدة 30 ثانية لتجنب حظر الحساب (Flood)
                        await Task.Delay(30000);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"⚠️ خطأ أثناء التحديث: {ex.Message}");
                        await Task.Delay(10000); // انتظر 10 ثواني قبل المحاولة مجدداً
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ خطأ في تسجيل الدخول: {ex.Message}");
                Console.WriteLine("اضغط أي مفتاح للخروج...");
                Console.ReadKey();
            }
        }
    }
}
