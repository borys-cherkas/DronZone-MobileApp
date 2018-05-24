using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using DronZone_UWP.Business.Services;
using DronZone_UWP.Models.AreaFilters;
using DronZone_UWP.Presentation.Views.Area;
using DronZone_UWP.Presentation.Views.AreaFilter;
using DronZone_UWP.Utils;
using ReactiveUI;

namespace DronZone_UWP.Presentation.ViewModels.AreaFilters
{
    public class FilterListViewModel : ViewModelBase
    {
        private readonly IAreaFilterService _areaFilterService;
        private readonly MenuNavigationHelper _menuNavigationHelper;

        private readonly string _areaId;

        private AreaFilterDetailedModel _selectedFilter;
        private ReactiveList<AreaFilterDetailedModel> _areaFilterList;

        public FilterListViewModel(IAreaFilterService areaFilterService, MenuNavigationHelper menuNavigationHelper)
        {
            _areaFilterService = areaFilterService;
            _menuNavigationHelper = menuNavigationHelper;

            _areaId = MenuContentViewModel.Param as string;

            GoToAddFilterPageCommand = ReactiveCommand.CreateFromTask(async () => GoToAddFilterPageExecuted());
            GoBackToAreaDetailsCommand = ReactiveCommand.CreateFromTask(async () => GoBackToAreaDetailsExecuted());
            AreaFilterList = new ReactiveList<AreaFilterDetailedModel>();


            this.ObservableForProperty(x => x.SelectedFilter)
                .Where(x => x.Value != null)
                .Subscribe(args =>
                {
                    GoToFilterDetails();
                });

            Init();
        }

        public ReactiveCommand GoToAddFilterPageCommand { get; set; }

        public ReactiveCommand GoBackToAreaDetailsCommand { get; set; }

        public AreaFilterDetailedModel SelectedFilter
        {
            get => _selectedFilter;
            set => this.RaiseAndSetIfChanged(ref _selectedFilter, value);
        }

        public ReactiveList<AreaFilterDetailedModel> AreaFilterList
        {
            get => _areaFilterList;
            set => this.RaiseAndSetIfChanged(ref _areaFilterList, value);
        }

        private async void Init()
        {
            await LoadAreaFiltersAsync();
        }

        private void GoToFilterDetails()
        {
            _menuNavigationHelper.NavigateTo(typeof(FilterDetailsPage), SelectedFilter?.Id);
        }

        private void GoToAddFilterPageExecuted()
        {
            _menuNavigationHelper.NavigateTo(typeof(AddFilterPage), _areaId);
        }

        private void GoBackToAreaDetailsExecuted()
        {
            _menuNavigationHelper.NavigateTo(typeof(AreaDetailsPage), _areaId);
        }

        private async Task LoadAreaFiltersAsync()
        {
            OnIsInProgressChanges(true);

            try
            {
                var areaId = MenuContentViewModel.Param as string;
                var areaFilters = await _areaFilterService.GetFiltersByAreaAsync(areaId);

                AreaFilterList.Clear();

                if (areaFilters != null)
                {
                    AreaFilterList.AddRange(areaFilters);
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