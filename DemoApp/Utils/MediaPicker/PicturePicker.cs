using System;
using System.IO;
using System.Threading.Tasks;

namespace DemoApp.Utils.MediaPicker
{
    public interface PicturePicker
    {
        Task<Stream> GetImageStreamAsync();
    }
}
