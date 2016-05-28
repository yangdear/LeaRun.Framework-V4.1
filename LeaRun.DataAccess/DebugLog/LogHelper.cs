using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeaRun.DataAccess
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
        public void Error(object message)
        {
            this.logger.Error(message);
            DbResultMsg.ReturnMsg = message.ToString();
        }
        public void Error(object message, Exception e)
        {
            this.logger.Error(message, e);
            DbResultMsg.ReturnMsg = message.ToString();
        }
    }
}
