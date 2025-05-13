using System;
using System.Collections.Generic;

namespace Source.Model
{
    public enum Status
    {
        Critical,
        Poor,
        Fair,
        Good,
        Prime,
        Perfect
    }

    public static class StatusExtensions
    {
        public static Status StatusForPercentage(int percentage)
        {
            // Exploits how C# enums are treated as ints
            return (Status) (percentage / 20);
        }

        public static string LocalizedName(this Status status)
        {
            return status switch
            {
                Status.Critical => Strings.Status_Critical, 
                Status.Poor => Strings.Status_Poor, 
                Status.Fair => Strings.Status_Fair,
                Status.Good => Strings.Status_Good,
                Status.Prime => Strings.Status_Prime, 
                Status.Perfect => Strings.Status_Perfect,
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
            };
        }
    }
}