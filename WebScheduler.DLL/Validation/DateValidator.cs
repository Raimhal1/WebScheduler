using System;
namespace WebScheduler.BLL.Validation
{
    public static class DateValidator
    {
        public static bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }

    }
}
