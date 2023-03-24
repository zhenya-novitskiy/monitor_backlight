using System;
using System.Collections.ObjectModel;
using Lightless.Components;

namespace Lightless.Infrastructure
{
    public class LedCollection<LedInstance> : ObservableCollection<LedInstance>
    {
        public event EventHandler CollectionUpdated;

        public void Update(Action updateAction)
        {
            updateAction();
            CollectionUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}
