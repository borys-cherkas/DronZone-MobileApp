using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using DronZone_UWP.Models.KindOfSport;
using DronZone_UWP.Presentation.Views.Fields;
using DronZone_UWP.Utils;
using ReactiveUI;
using Sportorent_UWP.Business.Services;

namespace DronZone_UWP.Presentation.ViewModels.KindOfSport
{
    public class KindOfSportListViewModel : ViewModelBase
    {
        private readonly MenuNavigationHelper _menuNavigationHelper;

        private ReactiveList<KindOfSportListItemModel> _kindOfSportList;
        private KindOfSportListItemModel _selectedKindOfSport;

        public KindOfSportListViewModel(MenuNavigationHelper menuNavigationHelper)
        {
            _menuNavigationHelper = menuNavigationHelper;

            Init();
        }

        public ReactiveList<KindOfSportListItemModel> KindOfSportList
        {
            get => _kindOfSportList;
            set => this.RaiseAndSetIfChanged(ref _kindOfSportList, value);
        }

        public KindOfSportListItemModel SelectedKindOfSport
        {
            get => _selectedKindOfSport;
            set => this.RaiseAndSetIfChanged(ref _selectedKindOfSport, value);
        }

        private static IDisposable _disposable;
        private async void Init()
        {
            KindOfSportList = new ReactiveList<KindOfSportListItemModel>();

            _disposable?.Dispose();
            _disposable = this.ObservableForProperty(x => x.SelectedKindOfSport)
                .Where(x => x.Value != null)
                .Subscribe(x =>
                {
                    _disposable?.Dispose();
                    GoToFieldsList();
                });

            await LoadKindOfSportListAsync();
        }

        private void GoToFieldsList()
        {
            _menuNavigationHelper.NavigateTo(typeof(FieldsListPage), SelectedKindOfSport);
        }

        private async Task LoadKindOfSportListAsync()
        {
            OnIsInProgressChanges(true);

            try
            {
                //var listWrapper = await _kindsOfSportService.GetAvailableKindsOfSportAsync();

                //KindOfSportList.Clear();
                //KindOfSportList.AddRange(listWrapper.KindOfSportList);
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
