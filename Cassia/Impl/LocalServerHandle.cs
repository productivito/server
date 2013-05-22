using System;

namespace Cassia.Impl
{
    /// <summary>
    /// Connection to the local terminal server.
    /// </summary>
    public class LocalServerHandle : ITerminalServerHandle
    {
        public IntPtr Handle
        {
            get { return NativeMethods.LocalServerHandle; }
        }

        public string ServerName
        {
            get { return null; }
        }

        public bool IsOpen
        {
            get { return true; }
        }

        public void Open() {}

        public void Close() {}

        public void Dispose() {}
    }
}