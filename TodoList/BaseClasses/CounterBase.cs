using Microsoft.AspNetCore.Components;

namespace TodoList.BaseClasses
{
    public class CounterBase : ComponentBase 
    {
        protected int currentCount = 0;

        protected void IncrementCount()
        {
            currentCount++;
        }
    }
}
