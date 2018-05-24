using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reactive.Linq;
using Windows.Devices.Geolocation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Maps;
using Autofac;
using DronZone_UWP.Presentation.ViewModels.Area;
using ReactiveUI;
using Color = Windows.UI.Color;

namespace DronZone_UWP.Presentation.Views.Area
{
    public sealed partial class AreaDetailsPage : IViewFor<AreaDetailsViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel),
                typeof(AreaDetailsViewModel),
                typeof(AreaDetailsPage),
                new PropertyMetadata(default(AreaDetailsViewModel)));

        public AreaDetailsPage()
        {
            InitializeComponent();
            ViewModel = App.Container.Resolve<AreaDetailsViewModel>();

            this.WhenActivated(CreateBindings);
        }

        private void CreateBindings(Action<IDisposable> d)
        {
            d(this.OneWayBind(ViewModel, vm => vm.IsBusy, v => v.Preloader.IsLoading));

            d(this.OneWayBind(ViewModel, vm => vm.Area.Name, v => v.AreaNameTextBlock.Text));
            d(this.BindCommand(ViewModel, vm => vm.GoToFiltersCommand, v => v.GoToFiltersButton));
            d(this.BindCommand(ViewModel, vm => vm.GoBackToAreaListCommand, v => v.GoBackToAreaListButton));

            d(ViewModel.ObservableForProperty(x => x.Area)
                .Where(x => x != null)
                .Subscribe(UpdateMapArea));
        }

        private void AreaMapControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            AreaMapControl.ZoomLevel = 14;
            AreaMapControl.Style = MapStyle.Road;
            AreaMapControl.MapProjection = MapProjection.WebMercator;
            AreaMapControl.StyleSheet = MapStyleSheet.RoadLight();
        }
        
        private void UpdateMapArea(object _ = null)
        {
            AreaMapControl.MapElements.Clear();

            AreaMapControl.Center = ViewModel.MapCenter;

            var map = ViewModel.Area.MapRectangle;
            var leftTop = new BasicGeoposition() { Latitude = map.South, Longitude = map.West};
            var leftBottom = new BasicGeoposition() { Latitude = map.South, Longitude = map.East };
            var rightTop = new BasicGeoposition() { Latitude = map.North, Longitude = map.West };
            var rightBottom = new BasicGeoposition() { Latitude = map.North, Longitude = map.East};

            MapPolygon mapPolygon = new MapPolygon
            {
                Path = new Geopath(new List<BasicGeoposition>()
                {
                    leftTop,
                    rightTop,
                    rightBottom,
                    leftBottom
                }),
                ZIndex = 1,
                FillColor = new Color() { A = 100, R = 255, G = 255, B = 153 },
                StrokeColor = Colors.Blue,
                StrokeThickness = 3,
                StrokeDashed = false
            };

            AreaMapControl.MapElements.Add(mapPolygon);
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (AreaDetailsViewModel)value;
        }

        public AreaDetailsViewModel ViewModel
        {
            get => (AreaDetailsViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
    }
}
