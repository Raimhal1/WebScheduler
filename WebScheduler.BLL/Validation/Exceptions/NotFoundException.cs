using System;


namespace WebScheduler.BLL.Validation.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            : base($"{name} ({key}) not found.") 
        {

        }
    }
}
