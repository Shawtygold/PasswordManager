using NLog;
using System.Data.Common;

namespace PasswordBox.MVVM.Model
{
    internal static class ExceptionsLogger
    {
        private readonly static Logger _logger = LogManager.GetCurrentClassLogger();

        public static void Handle(DbException e)
        {
            _logger.Error(e, "Database exception");
        }
        public static void Handle(NullReferenceException e)
        {
            _logger.Error(e, "Null reference exception");
        }
        public static void Handle(Exception e)
        {
            _logger.Error(e, "Default exception handler");
        }
    }
}
