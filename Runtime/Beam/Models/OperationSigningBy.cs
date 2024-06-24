namespace Beam.Models
{
    public enum OperationSigningBy
    {
        /// <summary>
        /// Will use Session if there is one, otherwise fallback to opening Browser
        /// </summary>
        Auto,
        
        /// <summary>
        /// Will always open Browser to sign an operation
        /// </summary>
        Browser,
        
        /// <summary>
        /// Will always try to use local Session, fail if there is no valid Sessions
        /// </summary>
        Session
    }
}