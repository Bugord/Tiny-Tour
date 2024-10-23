using System.Threading.Tasks;

namespace Core.Navigation
{
    public abstract class BaseModalPopup<T> : BasePopup
    {
        protected readonly TaskCompletionSource<T> Tcs = new TaskCompletionSource<T>();

        public Task Task => Tcs.Task;

        protected void SetResult(T data)
        {
            Tcs.SetResult(data);
        }
    }
}