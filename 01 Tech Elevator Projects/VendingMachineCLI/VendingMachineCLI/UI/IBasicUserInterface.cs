using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachineCLI.UI
{
    public interface IBasicUserInterface
    {
        void Output(string message);

        void PauseOutput();

        Object PromptForSelection(Object[] options);
    }
}
