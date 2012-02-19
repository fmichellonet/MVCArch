using Afterthought;

namespace MvcArch.Core.Log
{
    public class LogAmender<T> : Amendment<T, T>
    {
        /// <summary>
        /// The construcotr
        /// </summary>
        public LogAmender()
        {
            // Properties
            Properties
                .BeforeGet(Logger<T>.LogGetPropertyBefore)
                .AfterGet(Logger<T>.LogGetPropertyAfter)
                .BeforeSet(Logger<T>.LogSetPropertyBefore)
                .AfterSet(Logger<T>.LogSetPropertyAfter);

            // Methods
            Methods
                .Before(Logger<T>.LogMethodBefore)
                .After(Logger<T>.LogMethodAfter);
        }
    }
}