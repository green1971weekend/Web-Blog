namespace Blog.ViewModels
{
    /// <summary>
    /// View model captures on data from front-end razor pages.
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Login username credentials.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Password credentials.
        /// </summary>
        public string Password { get; set; }
    }
}
