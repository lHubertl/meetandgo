﻿using MeetAndGoMobile.Infrastructure.DependencyServices;
using System;
using System.IO;
using System.Threading.Tasks;
using Foundation;
using UIKit;

namespace MeetAndGoMobile.iOS.DependencyServices
{
    public class PicturePickerService : IPicturePicker
    {
        private TaskCompletionSource<(Stream stream, string name, string format)> _taskCompletionSource;
        private UIImagePickerController _imagePicker;

        public Task<(Stream stream, string name, string format)> PickImageAsync()
        {
            // Create and define UIImagePickerController
            _imagePicker = new UIImagePickerController
            {
                SourceType = UIImagePickerControllerSourceType.PhotoLibrary,
                MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary)
            };

            // Set event handlers
            _imagePicker.FinishedPickingMedia += OnImagePickerFinishedPickingMedia;
            _imagePicker.Canceled += OnImagePickerCancelled;

            // Present UIImagePickerController;
            UIWindow window = UIApplication.SharedApplication.KeyWindow;
            var viewController = window.RootViewController;
            viewController.PresentModalViewController(_imagePicker, true);

            // Return Task object
            _taskCompletionSource = new TaskCompletionSource<(Stream stream, string name, string format)>();
            return _taskCompletionSource.Task;
        }

        void OnImagePickerFinishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs args)
        {
            UIImage image = args.EditedImage ?? args.OriginalImage;

            if (image != null)
            {
                // Convert UIImage to .NET Stream object
                NSData data = image.AsJPEG(1);
                Stream stream = data.AsStream();

                UnregisterEventHandlers();

                // Set the Stream as the completion of the Task
                //TODO: CHECK AND FIX NAME AND TYPE
                _taskCompletionSource.SetResult((stream, "Image", args.MediaType));
            }
            else
            {
                UnregisterEventHandlers();
                _taskCompletionSource.SetResult((null, null, null));
            }
            _imagePicker.DismissModalViewController(true);
        }

        void OnImagePickerCancelled(object sender, EventArgs args)
        {
            UnregisterEventHandlers();
            _taskCompletionSource.SetResult((null, null, null));
            _imagePicker.DismissModalViewController(true);
        }

        void UnregisterEventHandlers()
        {
            _imagePicker.FinishedPickingMedia -= OnImagePickerFinishedPickingMedia;
            _imagePicker.Canceled -= OnImagePickerCancelled;
        }
    }
}