﻿using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.PropertyEditors.ValueConverters;

namespace UmbracoContentApi.Core.Converters
{
    public class ImageCropperConverter : IConverter
    {
        public string EditorAlias => "Umbraco.ImageCropper";

        public object Convert(object value, Dictionary<string, object> options = null)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value), $"A value for {EditorAlias} is required.");
            }

            var ctn = (ImageCropperValue)value;

            List<ImageCropperValue.ImageCropperCrop> crops = ctn.Crops?.ToList();
            var cropUrls = new Dictionary<string, string>();

            // ReSharper disable once InvertIf
            if (crops?.Any() != null)
            {
                foreach (ImageCropperValue.ImageCropperCrop crop in crops)
                {
                    cropUrls.Add(crop.Alias, ctn.Src + ctn.GetCropUrl(crop.Alias));
                }
            }

            return new
            {
                ctn.Src,
                Crops = cropUrls
            };
        }
    }
}