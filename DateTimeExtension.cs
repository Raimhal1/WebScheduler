using System;

public static class Status
{
    public static int getStatus(this DateTime start, this DateTime end)
    {
        var now = DateTime.UtcNow;
        if (now < startDate)
            return 0;
        else if (startDate <= now && now < endDate)
            return 1;
        return 2;
    }
}
