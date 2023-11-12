using System;

namespace Code.Services
{
    public class ProgressService : IProgressService
    {
        public ProgressService(SaveLoadService saveLoadService, AdsService adsService)
        {
            var data = saveLoadService.Load<GameData>(saveLoadService.SAVE_FILE_NAME);
            if (data != null)
                LastRecord = data.maxRecord;
            _saveLoadService = saveLoadService;
            _adsService = adsService;
        }

        public int CurrentRecord { get; private set; }
        public int LastRecord { get; private set; }
        public Action<int> OnChangeRecord { get; set; }

        private SaveLoadService _saveLoadService;
        private AdsService _adsService;
        public void SetRecord(int value)
        {
            if (value < 0)
                return;
            OnChangeRecord?.Invoke(value);
            CurrentRecord = value;
            
            if(CurrentRecord > LastRecord)
                _adsService.ShowInterstitialAd();
        }
        
        public void SetLastRecord(int value)
        {
            if (value < 0)
                return;
            LastRecord = value;
        }

        public void SaveProgress()
        {
            if(CurrentRecord < LastRecord)
                return;
            var data = new GameData() { maxRecord = CurrentRecord };
            _saveLoadService.Save(_saveLoadService.SAVE_FILE_NAME, data);
        }
    }
}