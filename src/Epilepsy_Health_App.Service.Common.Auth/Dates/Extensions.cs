using System;

namespace Epilepsy_Health_App.Service.Common.Auth.Core.Dates
{
    internal static class Extensions
    {
        public static long ToTimestamp(this DateTime dateTime) => new DateTimeOffset(dateTime).ToUnixTimeSeconds();
    }
}