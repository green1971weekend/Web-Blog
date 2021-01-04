using System.Collections.Generic;

namespace Blog.Helper
{
    /// <summary>
    /// Handles with a certain page amount computation.
    /// </summary>
    public static class PageHelper
    {
        /// <summary>
        /// Page list rendering.
        /// </summary>
        /// <param name="pageNumber">Current page number.</param>
        /// <param name="pageCount">Computed common amount of pages.</param>
        /// <returns></returns>
        public static IEnumerable<int> PageNumbers(int pageNumber, int pageCount)
        {
            //List<int> pages = new List<int>();
            if (pageCount <= 5)
            {
                for (int i = 1; i <= pageCount; i++)
                {
                    yield return i;
                }
            }
            else
            {
                var midPoint = pageNumber < 3 ? 3
                        : pageNumber > pageCount - 2 ? pageCount - 2
                        : pageNumber;

                var lowerBound = midPoint - 2;
                var upperBound = midPoint + 2;

                //Smart pagination based on three dots addition to begin and end of pagination list.
                if (lowerBound != 1)
                {
                    //pages.Insert(0, 1);

                    // Yield return will emit a value from PageNumbers iterator and it will carry on doing its thing. 
                    //It doesn't exit the function it will carry on executing.
                    yield return 1;

                    if (lowerBound - 1 > 1)
                    {
                        //pages.Insert(1, -1);
                        yield return -1;
                    }
                }

                for (int i = midPoint - 2; i <= upperBound; i++)
                {
                    //pages.Add(i);
                    yield return i;
                }

                if (upperBound != pageCount)
                {
                    //pages.Insert(pages.Count, pageCount);

                    if (pageCount - upperBound > 1)
                    {
                        //pages.Insert(pages.Count - 1, -1);
                        yield return -1;
                    }

                    yield return pageCount;
                }
            }
        }
    }
}
