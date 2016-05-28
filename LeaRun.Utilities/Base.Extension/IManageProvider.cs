using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeaRun.Utilities
{
    /// <summary>
    /// 管理提供者-接口
    /// </summary>
    public interface IManageProvider
    {
        /// <summary>
        /// 写入登录信息
        /// </summary>
        /// <param name="user">成员信息</param>
        void AddCurrent(IManageUser user);
        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        IManageUser Current();
        /// <summary>
        /// 删除当前用户
        /// </summary>
        void EmptyCurrent();
        /// <summary>
        /// 是否过期
        /// </summary>
        /// <returns></returns>
        bool IsOverdue();
    }
}
