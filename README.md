# Farghadani

این مخزن پروژه‌ی "Farghadani" (سامانهٔ Part Exchange) است.

اطلاعات مهم:
- پروژه به .NET 8 ارتقاء پیدا کرده است. یک `global.json` در ریشه برای پین کردن SDK به `9.0.308` اضافه شده است.

نحوه اجرای محلی:

1. بازگرداندن بسته‌ها:

```bash
dotnet restore "Farghadani.sln"
```

2. ساخت پروژه:

```bash
dotnet build "Farghadani.sln" -c Debug
```

3. اجرای API (پروژه‌ی WebApi):

```bash
dotnet run --project FuelStation.PartExchange_package/FuelStation.PartExchange/src/FuelStation.PartExchange.WebApi -c Debug
```

تنظیمات محیطی و اتصال‌ها:
- تنظیمات JWT و Connection String در `FuelStation.PartExchange_package/FuelStation.PartExchange/src/FuelStation.PartExchange.WebApi/appsettings.json` قرار دارند — قبل از اجرا مقداردهی کنید.

نکات:
- برای توسعه بهتر و یکسان‌سازی SDK از `global.json` استفاده شده؛ چنانچه SDK مناسبی نصب ندارید، لطفاً .NET SDK مطابق `global.json` را نصب کنید یا آن را به نسخه‌ی مورد نظر خود تغییر دهید.

تماس:
- برای سوالات بیشتر می‌توانید از Issues در گیت‌هاب استفاده کنید: https://github.com/MaryamRezaei1994/Farghadani
