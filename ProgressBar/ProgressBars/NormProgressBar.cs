namespace ProgressBar.ProgressBars
{
    #region Namespaces
    using ProgressBar.Models;
    using System;
    #endregion

    public class NormProgressBar
    {
        public NormProgressBar(NormProgressBarStyle style = NormProgressBarStyle.ColorBlock)
        {
            this.style = style;
            this.Left = 0;
            this.Top = Console.CursorTop + 1;
            this.Width = 50;

            // For color block style
            this.Background = ConsoleColor.DarkCyan;
            this.BlockColor = ConsoleColor.Green;

            // For char style
            this.Foreground = ConsoleColor.White;
            this.ProgressBarStartChar = '|';
            this.FilledProgressBodyChar = '=';
            this.FilledProgressHeaderChar = '>';
            this.ReplacedProgressBackgroundChar = '-';
            this.ProgressBarEndChar = '|';

            this.Done = false;

            this.cursorPos = new CursorPosition() { CursorLeft = 0, CursorTop = Console.CursorTop };
        }

        /// <summary>
        /// The left position of the progress bar.
        /// </summary>
        public int Left { get; set; }

        /// <summary>
        /// The top position of the progress bar.
        /// </summary>
        public int Top { get; set; }

        /// <summary>
        /// The width of the progress bar.
        /// </summary>
        public int Width { get; set; }

        #region Only for ColorBlock sub-style.
        /// <summary>
        /// The background of the progress bar.
        /// </summary>
        public ConsoleColor Background { get; set; }

        /// <summary>
        /// The color of filled blocks in the progress bar.  
        /// </summary>
        public ConsoleColor BlockColor { get; set; }
        #endregion

        #region For CharacterFill and CharacterReplace sub-styles.
        /// <summary>
        /// The foreground of the progress bar.
        /// </summary>
        public ConsoleColor Foreground { get; set; }

        /// <summary>
        /// The start character to shape the progress bar.
        /// </summary>
        public char ProgressBarStartChar { get; set; }

        /// <summary>
        /// Filled body-character in the progress bar.
        /// </summary>
        public char FilledProgressBodyChar { get; set; }

        /// <summary>
        /// Filled header-character in the progress bar.
        /// </summary>
        public char FilledProgressHeaderChar { get; set; }

        /// <summary>
        /// Replaced background-char in the progress bar.
        /// </summary>
        public char ReplacedProgressBackgroundChar { get; set; }

        /// <summary>
        /// The end character to shape the progress bar.
        /// </summary>
        public char ProgressBarEndChar { get; set; }
        #endregion

        /// <summary>
        /// The lengths of the previous messages.
        /// </summary>
        public int[] MessageLengths { get; set; }

        /// <summary>
        /// The indicator for whether the task has been finished.
        /// </summary>
        public bool Done { get; set; }

        public void Initialize()
        {
            if (this.style == NormProgressBarStyle.ColorBlock)
            {
                Console.SetCursorPosition(this.Left, this.Top);
                Console.BackgroundColor = this.Background;
                Console.Write(new string(' ', this.Width));
                Console.ResetColor();
            }
            else if (this.style == NormProgressBarStyle.CharacterFill) 
            {
                Console.SetCursorPosition(this.Left, this.Top);
                Console.ForegroundColor = this.Foreground;
                Console.Write(this.ProgressBarStartChar + new string(' ', this.Width - 2) + this.ProgressBarEndChar);
                Console.ResetColor();
            }
            else // NormProgressBarStyle.CharacterReplace
            {
                Console.SetCursorPosition(this.Left, this.Top);
                Console.ForegroundColor = this.Foreground;
                Console.Write(this.ProgressBarStartChar + new string(this.ReplacedProgressBackgroundChar, this.Width - 2) + this.ProgressBarEndChar);
                Console.ResetColor();
            }
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            if (this.Done)
            {
                Console.SetCursorPosition(0, this.Top + 1);
            }
        }

        public void SetInfo(double value, string message, ConsoleColor fontColor = ConsoleColor.White, ConsoleColor scaleColor = ConsoleColor.White)
        {
            if (this.MessageLengths != null && this.MessageLengths.Length > 0)
            {
                this.ClearProgressMessage();
            }

            bool flag = message.Contains("\n") || this.MessageLengths != null && this.MessageLengths.Length > 1;
            if (flag)
            {
                this.ClearProgressBar();
            }

            var msgs = message.Split('\n');
            this.MessageLengths = new int[msgs.Length];
            for (int i = 0; i < this.MessageLengths.Length; i++)
            {
                this.MessageLengths[i] = msgs[i].Length;
            }

            Console.ForegroundColor = fontColor;
            Console.SetCursorPosition(this.cursorPos.CursorLeft, this.cursorPos.CursorTop);
            Console.WriteLine(message);

            if (flag)
            {
                this.Top = Console.CursorTop;

                // Re-initialize
                this.Initialize();
            }

            if (this.style == NormProgressBarStyle.ColorBlock)
            {
                Console.BackgroundColor = this.BlockColor;
                Console.SetCursorPosition(0, this.Top);
                var blocks = new string(' ', (int)Math.Round(value / 100.0 * this.Width));
                if (blocks.Length == this.Width)
                {
                    this.Done = true;
                }
                Console.Write(blocks);
                Console.ResetColor();

                Console.ForegroundColor = scaleColor;
                Console.SetCursorPosition(this.Width + SCALE_PADDING, this.Top);
                var scale = string.Format("{0}%", (int)((double)blocks.Length / (double)this.Width * 100));
                scale = new string(' ', SCALE_MAXLEN - scale.Length) + scale;
                Console.Write(scale);
                Console.ResetColor();
            }
            else/* if (this.style == NormProgressBarStyle.CharacterFill)*/
            {
                Console.ForegroundColor = this.Foreground;
                Console.SetCursorPosition(1, this.Top);
                int count = (int)Math.Round(value / 100.0 * (this.Width - 2));
                string chars = new string(this.FilledProgressHeaderChar, 1);
                if (count > 1)
                {
                    chars = new string(this.FilledProgressBodyChar, count - 1) + this.FilledProgressHeaderChar;
                }

                if (count + 2 == this.Width)
                {
                    this.Done = true;
                }
                Console.Write(chars);
                Console.ResetColor();

                Console.ForegroundColor = scaleColor;
                Console.SetCursorPosition(this.Width + SCALE_PADDING, this.Top);
                var scale = string.Format("{0}%", (int)((double)(chars.Length + 2) / (double)this.Width * 100));
                scale = new string(' ', SCALE_MAXLEN - scale.Length) + scale;
                Console.Write(scale);
                Console.ResetColor();
            }
        }

        private const int SCALE_PADDING = 5;
        private const int SCALE_MAXLEN = 4;

        private CursorPosition cursorPos;
        private NormProgressBarStyle style;

        private void ClearProgressMessage()
        {
            for (int i = 0; i < this.MessageLengths.Length; i++)
            {
                Console.SetCursorPosition(this.Left, this.Top - (i + 1));
                Console.Write(new string(' ', this.MessageLengths[this.MessageLengths.Length - i - 1]));
            }
        }

        private void ClearProgressBar()
        {
            Console.SetCursorPosition(this.Left, this.Top);
            Console.Write(new string(' ', this.Width + SCALE_PADDING + SCALE_MAXLEN));
        }
    }

    public enum NormProgressBarStyle
    {
        CharacterFill,
        CharacterReplace,
        ColorBlock
    }
}
