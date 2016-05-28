using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeaRun.Utilities
{
    /// <summary>
    /// Log4Net日志类
    /// 版本：2.0
    /// <author>
    ///		<name>shecixiong</name>
    ///		<date>2014.03.03</date>
    /// </author>
    /// </summary>
    public class LogHelper
    {
        private ILog logger;

        public LogHelper(ILog log)
        {
            this.logger = log;
        }
        public void Debug(object message)
        {
            this.logger.Debug(message);
        }
        public void Debug(object message, Exception e)
        {
            this.logger.Debug(message, e);
        }
        public void Error(object message)
        {
            this.logger.Error(message);
        }
        public void Error(object message, Exception e)
        {
            this.logger.Error(message, e);
        }
    }
}
