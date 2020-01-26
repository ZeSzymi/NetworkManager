﻿using Windows.ApplicationModel.Resources;

namespace NetworkManager.Helpers
{
    internal static class ResourceExtensions
    {
        private static ResourceLoader _resLoader = new ResourceLoader();

        public static string GetLocalized(this string resourceKey) => _resLoader.GetString(resourceKey);
    }
}
