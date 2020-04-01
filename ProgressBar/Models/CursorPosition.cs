namespace ProgressBar.Models
{
    #region Namespaces
    using System;
    #endregion

    public class CursorPosition
    {
        public int CursorLeft { get; set; }
        public int CursorTop { get; set; }
        public override string ToString()
        {
            return String.Format(@"CursorLeft: {0}, CursorTop: {1}", this.CursorLeft, this.CursorTop);
        }
    }
}
