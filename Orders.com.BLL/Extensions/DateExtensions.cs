using System;

namespace Orders.com.Extensions
{
    public static class DateExtensions
    {
        public static MinuteResult Minutes(this int minutes)
        {
            return new MinuteResult(minutes);
        }

        public static HourResult Hours(this int hours)
        {
            return new HourResult(hours);
        }

        public static DayResult Days(this int days)
        {
            return new DayResult(days);
        }

        public static YearResult Years(this int years)
        {
            return new YearResult(years);
        }

        public static DateTime Ago(this MinuteResult result)
        {
            return DateTime.Now.AddMinutes(-result.NumberOfMinutes);
        }

        public static DateTime Ago(this DayResult result)
        {
            return DateTime.Now.AddDays(-result.NumberOfDays);
        }

        public static DateTime Ago(this YearResult result)
        {
            return DateTime.Now.AddYears(-result.NumberOfYears);
        }

        public static FromMinuteResult From(this MinuteResult result)
        {
            return new FromMinuteResult(result);
        }

        public static FromHourResult From(this HourResult result)
        {
            return new FromHourResult(result);
        }

        public static FromDayResult From(this DayResult result)
        {
            return new FromDayResult(result);
        }

        public static FromYearResult From(this YearResult result)
        {
            return new FromYearResult(result);
        }

        public static DateTime Now(this FromMinuteResult result)
        {
            return DateTime.Now.AddMinutes(result.Result.NumberOfMinutes);
        }

        public static DateTime Now(this FromHourResult result)
        {
            return DateTime.Now.AddHours(result.Result.NumberOfHours);
        }

        public static DateTime Now(this FromDayResult result)
        {
            return DateTime.Now.AddDays(result.Result.NumberOfDays);
        }

        public static DateTime Now(this FromYearResult result)
        {
            return DateTime.Now.AddYears(result.Result.NumberOfYears);
        }

        public static DateTime Yesterday(this FromMinuteResult result)
        {
            return DateTime.Now.AddDays(-1).AddMinutes(result.Result.NumberOfMinutes);
        }

        public static DateTime Yesterday(this FromHourResult result)
        {
            return DateTime.Now.AddDays(-1).AddHours(result.Result.NumberOfHours);
        }

        public static DateTime Yesterday(this FromDayResult result)
        {
            return DateTime.Now.AddDays(result.Result.NumberOfDays);
        }

        public static DateTime Yesterday(this FromYearResult result)
        {
            return DateTime.Now.AddYears(result.Result.NumberOfYears);
        }

        public static DateTime Tomorrow(this FromMinuteResult result)
        {
            return DateTime.Now.AddMinutes(result.Result.NumberOfMinutes);
        }

        public static DateTime Tomorrow(this FromHourResult result)
        {
            return DateTime.Now.AddHours(result.Result.NumberOfHours);
        }

        public static DateTime Tomorrow(this FromDayResult result)
        {
            return DateTime.Now.AddDays(result.Result.NumberOfDays);
        }

        public static DateTime Tomorrow(this FromYearResult result)
        {
            return DateTime.Now.AddYears(result.Result.NumberOfYears);
        }
    }

    
    public class FromMinuteResult
    {
        public FromMinuteResult(MinuteResult result)
        {
            Result = result;
        }

        internal MinuteResult Result { get; private set; }

    }

    public class FromHourResult
    {
        public FromHourResult(HourResult result)
        {
            Result = result;
        }

        internal HourResult Result { get; private set; }

    }
    public class FromDayResult
    {
        public FromDayResult(DayResult result)
        {
            Result = result;
        }

        internal DayResult Result { get; private set; }

    }
    public class FromYearResult
    {
        public FromYearResult(YearResult result)
        {
            Result = result;
        }

        internal YearResult Result { get; private set; }

    }

    public class MinuteResult
    {
        public MinuteResult(int numberOfMinutes)
        {
            NumberOfMinutes = numberOfMinutes;
        }

        internal int NumberOfMinutes { get; private set; }
    }

    public class HourResult
    {
        public HourResult(int numberOfHours)
        {
            NumberOfHours = numberOfHours;
        }

        internal int NumberOfHours { get; private set; }
    }

    public class DayResult
    {
        public DayResult(int numberOfDays)
        {
            NumberOfDays = numberOfDays;
        }

        internal int NumberOfDays { get; private set; }
    }

    public class YearResult
    {
        public YearResult(int numberOfYears)
        {
            NumberOfYears = numberOfYears;
        }

        internal int NumberOfYears { get; private set; }
    }
}
