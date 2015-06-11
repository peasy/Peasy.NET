using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Extensions
{
    public static MinuteResult Minutes(this int minutes)
    {
      return new MinuteResult(minutes); 
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
    
    public static DateTime FromNow(this DayResult result)
    {
      return DateTime.Now.AddDays(result.NumberOfDays);
    }
    
    public static DateTime FromNow(this YearResult result)
    {
      return DateTime.Now.AddYears(result.NumberOfYears);
    }    
}


public class MinuteResult
{
    public MinuteResult(int numberOfMinutes)
    {
        NumberOfMinutes = numberOfMinutes;
    }

    public int NumberOfMinutes { get; private set; }
}

public class DayResult
{
  public DayResult(int numberOfDays)
  {
     NumberOfDays = numberOfDays;
  }

  public int NumberOfDays { get; private set; }
}

public class YearResult
{
  public YearResult(int numberOfYears)
  {
     NumberOfYears = numberOfYears;
  }

  public int NumberOfYears { get; private set; }
}
