using System;
using System.Collections.ObjectModel;
using Windows.Devices.WiFi;
using Windows.UI.Xaml.Data;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Toolkit.Uwp.UI.Controls;

namespace NetworkManager.Converters
{
    public class NetworkToOrbitItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            ObservableCollection<WiFiAvailableNetwork> AvailableNetworks =
                (ObservableCollection<WiFiAvailableNetwork>) value;
            ObservableCollection<OrbitViewDataItem> Orbits = new ObservableCollection<OrbitViewDataItem>();

            AvailableNetworks.ForEach(network =>
            {
                Orbits.Add(new OrbitViewDataItem()
                {
                    Diameter = 1,
                    Distance = ConvertSignalBarsToOrbitDistance(network.SignalBars),
                    Label = network.Ssid,
                    Item = network
                });
            });

            return Orbits;
        }

        /// <summary>
        /// This method is not implementer because we're using one-way data source binding
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        private double ConvertSignalBarsToOrbitDistance(byte signalBars) =>
            signalBars switch
            {
                4 => 0.20,
                3 => 0.40,
                2 => 0.60,
                _ => 0.80
            };
    }
}
