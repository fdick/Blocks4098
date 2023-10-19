using System;

namespace Code.Services
{
    public class ProgressService : IProgressService
    {
        public int CurrentRecord { get; private set; }
        public Action<int> OnChangeRecord { get; set; }

        public void SetRecord(int value)
        {
            if (value < 0)
                return;
            OnChangeRecord?.Invoke(value);
            CurrentRecord = value;
        }
    }
}