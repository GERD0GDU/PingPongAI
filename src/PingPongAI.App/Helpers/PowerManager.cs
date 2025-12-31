using System.Runtime.InteropServices;

namespace PingPongAI.App.Helpers
{
    public sealed class PowerManager : IDisposable
    {
        [DllImport("kernel32.dll")]
        private static extern uint SetThreadExecutionState(uint esFlags);

        private const uint ES_CONTINUOUS = 0x80000000;
        private const uint ES_DISPLAY_REQUIRED = 0x00000002;
        private const uint ES_SYSTEM_REQUIRED = 0x00000001;

        private bool _disposed = false;
        private readonly object _lock = new();

        public PowerManager()
        {
            // Prevent display turning off and sleep/hibernation
            _ = SetThreadExecutionState(ES_CONTINUOUS | ES_DISPLAY_REQUIRED | ES_SYSTEM_REQUIRED);

            // Global crash handler
            App.Current.DispatcherUnhandledException += OnUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
        }

        private void OnUnhandledException(object sender, EventArgs e)
        {
            Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // Prevent finalizer from running again
        }

        private void Dispose(bool disposing)
        {
            lock (_lock)
            {
                if (_disposed) return;
                _disposed = true;

                // Restore normal power management
                _ = SetThreadExecutionState(ES_CONTINUOUS);

                if (disposing)
                {
                    // Only clean up managed resources
                    App.Current.DispatcherUnhandledException -= OnUnhandledException;
                    AppDomain.CurrentDomain.UnhandledException -= OnUnhandledException;
                }
            }
        }

        ~PowerManager()
        {
            Dispose(false);
        }
    }
}
