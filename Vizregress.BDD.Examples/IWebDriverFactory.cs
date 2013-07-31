namespace Vizregress.BDD.Examples
{
    /// <summary>
    /// A factory interface for creating <see cref="IInformationalWebDriver"/>
    /// </summary>
    public interface IWebDriverFactory
    {
        /// <summary>
        /// The informational webdriver
        /// </summary>
        /// <returns></returns>
        IInformationalWebDriver Create();       
    }
}
