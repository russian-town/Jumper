using System;
using System.Threading.Tasks;

namespace Sourse.StartScene
{
    public interface ILoadingOperation
    {
        public Task Load(Action<float> onProgress);
    }
}
