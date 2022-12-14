using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.ObjectPool;

namespace Api.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalcuateAge(this DateOnly dob)
        {
            var today =DateOnly.FromDateTime(DateTime.UtcNow);
            var age =today.Year - dob.Year;
            if(dob> today.AddYears(-age)) age--;
            return age;
        }
        
    }
}