using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using DronZone_UWP.Business.Services;
using DronZone_UWP.Models.Area;
using DronZone_UWP.Presentation.Views.Area;
using DronZone_UWP.Utils;
using ReactiveUI;

namespace DronZone_UWP.Presentation.ViewModels.Area
{
    public class UserAreasListViewModel : ViewModelBase
    {
        private readonly IAreaService _areaService;
        private readonly MenuNavigationHelper _menuNavigationHelper;

        private ReactiveList<AreaDetailedModel> _areaList;
        private AreaDetailedModel _selectedArea;

        public UserAreasListViewModel(IAreaService areaService, MenuNavigationHelper menuNavigationHelper)
        {
            _areaService = areaService;
            _menuNavigationHelper = menuNavigationHelper;

            AreaList = new ReactiveList<AreaDetailedModel>();

            this.ObservableForProperty(x => x.SelectedArea)
                .Where(x => x.Value != null)
                .Subscribe(args =>
                {
                    GoToAreaDetails();
                });

            Init();
        }

        public ReactiveList<AreaDetailedModel> AreaList
        {
            get => _areaList;
            set => this.RaiseAndSetIfChanged(ref _areaList, value);
        }

        public AreaDetailedModel SelectedArea
        {
            get => _selectedArea;
            set => this.RaiseAndSetIfChanged(ref _selectedArea, value);
        }
        
        private async void Init()
        {
            await LoadAreasAsync();
        }

        private void GoToAreaDetails()
        {
            _menuNavigationHelper.NavigateTo(typeof(AreaDetailsPage), SelectedArea?.Id);
        }

        private async Task LoadAreasAsync()
        {
            OnIsInProgressChanges(true);

            try
            {
                var areas = await _areaService.GetCurrentUserAreasAsync();

                AreaList.Clear();

                if (areas != null)
                {
                    AreaList.AddRange(areas);
                }
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
