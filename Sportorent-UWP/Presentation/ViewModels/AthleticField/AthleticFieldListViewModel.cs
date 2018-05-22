using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using DronZone_UWP.Business.Services;
using DronZone_UWP.Models.AthleticField;
using DronZone_UWP.Models.KindOfSport;
using DronZone_UWP.Presentation.Views.Fields;
using DronZone_UWP.Utils;
using ReactiveUI;
using Sportorent_UWP.Business.Services;

namespace DronZone_UWP.Presentation.ViewModels.AthleticField
{
    public class AthleticFieldListViewModel : ViewModelBase
    {
        private readonly IDroneService _athleticFieldsService;
        private readonly MenuNavigationHelper _menuNavigationHelper;

        private ReactiveList<AthleticFieldListItemModel> _athleticFieldList;
        private AthleticFieldListItemModel _selectedAthleticField;
        private string _sportName;

        public AthleticFieldListViewModel(
            IDroneService athleticFieldsService,
            MenuNavigationHelper menuNavigationHelper)
        {
            _athleticFieldsService = athleticFieldsService;
            _menuNavigationHelper = menuNavigationHelper;

            Init();
        }

        public string SportName
        {
            get => _sportName;
            set => this.RaiseAndSetIfChanged(ref _sportName, value);
        }

        public ReactiveList<AthleticFieldListItemModel> AthleticFieldList
        {
            get => _athleticFieldList;
            set => this.RaiseAndSetIfChanged(ref _athleticFieldList, value);
        }

        public AthleticFieldListItemModel SelectedAthleticField
        {
            get => _selectedAthleticField;
            set => this.RaiseAndSetIfChanged(ref _selectedAthleticField, value);
        }

        private async void Init()
        {
            AthleticFieldList = new ReactiveList<AthleticFieldListItemModel>();

            this.ObservableForProperty(x => x.SelectedAthleticField)
                .Where(x => x.Value != null)
                .Subscribe(x =>
                {
                    GoToFieldDetails();
                });

            await LoadAthleticFieldListAsync();
        }

        private void GoToFieldDetails()
        {
            _menuNavigationHelper.NavigateTo(typeof(AthleticFieldDetailsPage), SelectedAthleticField);
        }

        private async Task LoadAthleticFieldListAsync()
        {
            OnIsInProgressChanges(true);

            try
            {
                var kindOfSport = MenuContentViewModel.Param as KindOfSportListItemModel;
                //var listWrapper = await _athleticFieldsService.GetAthleticFieldsBySportKind(kindOfSport.Id);

                //SportName = listWrapper.KindOfSportName;
                //AthleticFieldList.Clear();
                //AthleticFieldList.AddRange(listWrapper.AthleticFieldList);
            }
            catch (Exception ex)
            {
                await ShowErrorAsync(ex.Message);
            }
            finally
            {
                OnIsInProgressChanges(false);
            }
        }
    }
}
