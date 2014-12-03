using System;

using WatiN.Core.DialogHandlers;
using WatiN.Core.Exceptions;
using WatiN.Core.Interfaces;
using WatiN.Core.Native.Windows;
using WatiN.Core.UtilityClasses;

namespace Coypu.Drivers.Watin
{
    public class DialogHandler : IDialogHandler, IDisposable
    {
        private readonly AlertDialogHandler alertHandler;
        private readonly ConfirmDialogHandler confirmHandler;

        public DialogHandler()
        {
            alertHandler = new AlertDialogHandler();
            confirmHandler = new ConfirmDialogHandler();
        }

        public bool HandleDialog(Window window)
        {
            return alertHandler.HandleDialog(window) || confirmHandler.HandleDialog(window);
        }

        public bool CanHandleDialog(Window window, IntPtr mainWindowHwnd)
        {
            return alertHandler.CanHandleDialog(window, mainWindowHwnd) || confirmHandler.CanHandleDialog(window, mainWindowHwnd);
        }

        private void WaitUntilNoLongerExists(int waitDurationInSeconds = 10)
        {
            new TryFuncUntilTimeOut(TimeSpan.FromSeconds(waitDurationInSeconds)).Try(NotExists);

            if (Exists())
                throw new WatiNException(string.Format("Dialog still available after {0} seconds.", waitDurationInSeconds));
        }

        public bool Exists()
        {
            return alertHandler.Exists() || confirmHandler.Exists();
        }

        private bool NotExists()
        {
            return !alertHandler.Exists() && !confirmHandler.Exists();
        }

        public string Message
        {
            get
            {
                if (alertHandler.Exists())
                    return alertHandler.Message;
                if (confirmHandler.Exists())
                    return confirmHandler.Message;
                throw new WatiNException("Dialog not available");
            }
        }

        public void Dispose()
        {
            if (Exists())
                ClickOk();
        }

        public void ClickOk()
        {
            if (alertHandler.Exists())
                alertHandler.OKButton.Click();
            else if (confirmHandler.Exists())
                confirmHandler.OKButton.Click();
            else
                throw new MissingDialogException("No dialog was present to accept");
            
            WaitUntilNoLongerExists();
        }

        public void ClickCancel()
        {
            if (confirmHandler.Exists())
                confirmHandler.CancelButton.Click();
            else
                throw new MissingDialogException("No dialog was present to confirm");

            WaitUntilNoLongerExists();
        }
    }
}