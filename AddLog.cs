using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tovuti_api
{
    public class AddLog
    {
        public void LogError(string error)
        {
            Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Debug()
                        .WriteTo.File(AppDomain.CurrentDomain.BaseDirectory + @"\logfile.log", rollingInterval: RollingInterval.Day, shared: true)
                        .CreateLogger();
            Log.Error(error);

        }
        public void LogInfo(string info)
        {
            Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Debug()
                        .WriteTo.File(AppDomain.CurrentDomain.BaseDirectory + @"\logfile.log", rollingInterval: RollingInterval.Day, shared: true)
                        .CreateLogger();
            Log.Information(info);

        }
        public void LogWarning(string warning)
        {
            Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Debug()
                        .WriteTo.File(AppDomain.CurrentDomain.BaseDirectory + @"\logfile.log", rollingInterval: RollingInterval.Day, shared: true)
                        .CreateLogger();
            Log.Warning(warning);

        }
    }
}