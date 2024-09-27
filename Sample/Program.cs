using System.Globalization;

namespace Sample
{
    internal class Program
    {
        [Obsolete]
        static void Main(string[] args)

        {
            //TimeZone localZone1 = TimeZone.CurrentTimeZone;

            //// Get current date and time
            //DateTime localTime1 = DateTime.Now;

            //// Get Coordinated Universal Time (UTC)
            //DateTime utcTime1 = localZone1.ToUniversalTime(localTime1);

            //// Output
            //Console.WriteLine("Local Time: " + localTime1);
            //Console.WriteLine("UTC Time: " + utcTime1);

            //Timezone info

            //Get local time zone info

            //TimeZoneInfo localZone = TimeZoneInfo.Local;

            // // Get a specific time zone (e.g., Eastern Standard Time)
            // TimeZoneInfo estZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

            // // Get current time in local time zone
            // DateTime localTime = DateTime.Now;

            // // Convert local time to UTC
            // DateTime utcTime = TimeZoneInfo.ConvertTimeToUtc(localTime, localZone);

            // // Convert UTC to Eastern Standard Time
            // DateTime estTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, estZone);

            // // Output
            // Console.WriteLine("Local Time: " + localTime);
            // Console.WriteLine("UTC Time: " + utcTime);
            // Console.WriteLine("EST Time: " + estTime);



            ////Date time offset


            //DateTimeOffset currentTimeOffset = DateTimeOffset.Now;

            //// Create a DateTimeOffset for a specific time and UTC offset
            //DateTimeOffset specificTime = new DateTimeOffset(2024, 9, 26, 12, 0, 0, TimeSpan.FromHours(-5)); // UTC-5

            //// Convert to UTC
            //DateTimeOffset utcTimeOffset = specificTime.ToUniversalTime();

            //// Output
            //Console.WriteLine("Current Time with Offset: " + currentTimeOffset);
            //Console.WriteLine("Specific Time with Offset: " + specificTime);
            //Console.WriteLine("Specific Time in UTC: " + utcTimeOffset);


            ////Date only

            //var theDate = new DateOnly(2015, 10, 21);

            //var nextDay = theDate.AddDays(1);
            //var previousDay = theDate.AddDays(-1);
            //var decadeLater = theDate.AddYears(10);
            //var lastMonth = theDate.AddMonths(-1);

            //Console.WriteLine($"Date: {theDate}");
            //Console.WriteLine($" Next day: {nextDay}");
            //Console.WriteLine($" Previous day: {previousDay}");
            //Console.WriteLine($" Decade later: {decadeLater}");
            //Console.WriteLine($" Last month: {lastMonth}");


            ////parse dateonly

            //var theDate1 = DateOnly.ParseExact("21 Oct 2015", "dd MMM yyyy", CultureInfo.InvariantCulture);  // Custom format
            //var theDate2 = DateOnly.Parse("October 21, 2015", CultureInfo.InvariantCulture);

            //Console.WriteLine(theDate1.ToString("m", CultureInfo.InvariantCulture));     // Month day pattern
            //Console.WriteLine(theDate2.ToString("o", CultureInfo.InvariantCulture));    // ISO 8601 format
            //Console.WriteLine(theDate2.ToLongDateString());


            ////Compare dateonly

            //var theDate3 = DateOnly.ParseExact("21 Oct 2015", "dd MMM yyyy", CultureInfo.InvariantCulture);  // Custom format
            //var theDate4 = DateOnly.Parse("October 21, 2015", CultureInfo.InvariantCulture);
            //var dateLater = theDate3.AddMonths(6);
            //var dateBefore = theDate4.AddDays(-10);

            //Console.WriteLine($"Consider {theDate3}...");
            //Console.WriteLine($" Is '{nameof(theDate4)}' equal? {theDate3 == theDate3}");
            //Console.WriteLine($" Is {dateLater} after? {dateLater > theDate3} ");
            //Console.WriteLine($" Is {dateLater} before? {dateLater < theDate3} ");
            //Console.WriteLine($" Is {dateBefore} after? {dateBefore > theDate3} ");
            //Console.WriteLine($" Is {dateBefore} before? {dateBefore < theDate3} ");


            ////time only
            var theTime = new TimeOnly(7, 23, 11);

            var hourLater = theTime.AddHours(1);
            var minutesBefore = theTime.AddMinutes(-12);
            var secondsAfter = theTime.Add(TimeSpan.FromSeconds(10));
            var daysLater = theTime.Add(new TimeSpan(hours: 21, minutes: 200, seconds: 83), out int wrappedDays);
            var daysBehind = theTime.AddHours(-222, out int wrappedDaysFromHours);

            Console.WriteLine($"Time: {theTime}");
            Console.WriteLine($" Hours later: {hourLater}");
            Console.WriteLine($" Minutes before: {minutesBefore}");
            Console.WriteLine($" Seconds after: {secondsAfter}");
            Console.WriteLine($" {daysLater} is the time, which is {wrappedDays} days later");
            Console.WriteLine($" {daysBehind} is the time, which is {wrappedDaysFromHours} days prior");
        }
    }
}
