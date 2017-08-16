using System;
using System.IO;
using System.Collections.Generic;
using Xamarin.Forms;
using DemoApp.Utils.MediaPicker;
using System.Diagnostics;
using System.Threading.Tasks;
using DemoApp.Models;

namespace DemoApp.Views
{
    public partial class SliderPage : ContentPage
    {
        User user;
        public SliderPage()
        {
            InitializeComponent();
            user = Utils.Utility.getUserDetails();
            lblFirstName.Text = user.FirstName;
            lblEmail.Text = user.Email;
            imgViewProfile.Source = user.ProfileImage;
        }

        async void didTapLogout(object sender, EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("=====NavigationStack===== " + Navigation.NavigationStack.Count);
            //int i = 1;
            //while (Navigation.NavigationStack.Count > 0 && Navigation.NavigationStack.Count >= i)
            //{
            //	System.Diagnostics.Debug.WriteLine("=======: " + Navigation.NavigationStack[i - 1]);
            //	i++;
            //}

            //System.Diagnostics.Debug.WriteLine("=====ModalStack===== " + Navigation.ModalStack.Count);
            //int j = 1;

            //while (Navigation.ModalStack.Count > 1 && Navigation.ModalStack.Count >= i)
            //{
            //	System.Diagnostics.Debug.WriteLine("=======: " + Navigation.ModalStack[i - 1]);
            //	j++;
            //}

            //Navigation.RemovePage(this);

            await Utils.Utility.clearAllApplicationProperty();
            Application.Current.MainPage = new NavigationPage(new StartPage());
        }

        private async void didTapProfilePic(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet("Change profile picture", "Cancel", null, "Take Photo", "Choose from Library");
            Debug.WriteLine("Action: " + action);
            if (action == "Take Photo")
            {
                var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });
                if (photo != null)
                {
                    User user = new User();
                    using (MemoryStream ms = new MemoryStream())
                    {
                        photo.GetStream().CopyTo(ms);
                        user.ProfilePicData = ms.ToArray();
                    }
                    user = await App.loginManager.updateProfilePicAPICall(user);
                    if (user.UserID != 0)
                    {
                        imgViewProfile.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
                    }
                }
            }
            else if (action == "Choose from Library")
            {
                await Task.Delay(50);
                Stream stream = await DependencyService.Get<PicturePicker>().GetImageStreamAsync();
                if (stream != null)
                {
                    //User user = new User();
                    MemoryStream ms = new MemoryStream();
                    stream.CopyTo(ms);
					ms = new MemoryStream(ms.ToArray());
					user.ProfilePicData = ms.ToArray();
                    user = await App.loginManager.updateProfilePicAPICall(user);
                    if (user.UserID != 0)
                    {
                        imgViewProfile.Source = ImageSource.FromStream(() => ms); ;
                    }
                }
            }
        }
    }
}
