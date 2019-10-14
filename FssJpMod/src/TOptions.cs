// ******************************************************************************
// Copyright (c) 2015-2019 synctam
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
// of the Software, and to permit persons to whom the Software is furnished to do
// so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

namespace MonoOptions
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Mono.Options;

    /// <summary>
    /// コマンドライン オプション
    /// </summary>
    public class TOptions
    {
        //// ******************************************************************************
        //// Property fields
        //// ******************************************************************************
        private TArgs args;
        private bool isError = false;
        private StringWriter errorMessage = new StringWriter();
        private OptionSet optionSet;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="arges">コマンドライン引数</param>
        public TOptions(string[] arges)
        {
            this.args = new TArgs();
            this.Settings(arges);
            if (this.IsError)
            {
                this.ShowErrorMessage();
                this.ShowUsage();
            }
            else
            {
                this.CheckOption();
                if (this.IsError)
                {
                    this.ShowErrorMessage();
                    this.ShowUsage();
                }
                else
                {
                    // skip
                }
            }
        }

        //// ******************************************************************************
        //// Property
        //// ******************************************************************************

        /// <summary>
        /// コマンドライン オプション
        /// </summary>
        public TArgs Arges { get { return this.args; } }

        /// <summary>
        /// コマンドライン オプションのエラー有無
        /// </summary>
        public bool IsError { get { return this.isError; } }

        /// <summary>
        /// エラーメッセージ
        /// </summary>
        public string ErrorMessage { get { return this.errorMessage.ToString(); } }

        /// <summary>
        /// Uasgeを表示する
        /// </summary>
        public void ShowUsage()
        {
            TextWriter writer = Console.Error;
            this.ShowUsage(writer);
        }

        /// <summary>
        /// Uasgeを表示する
        /// </summary>
        /// <param name="textWriter">出力先</param>
        public void ShowUsage(TextWriter textWriter)
        {
            StringWriter msg = new StringWriter();

            string exeName = Path.GetFileName(Environment.GetCommandLineArgs()[0]);
            msg.WriteLine(string.Empty);
            msg.WriteLine($@"使い方：");
            msg.WriteLine($@"日本語化MODを作成する。");
            msg.WriteLine(
                $@"  usage: {exeName} -i <original lang file path> -o <japanized lang file path>" +
                $@" -s <Trans Sheet path> [-m] [-r]");
            msg.WriteLine($@"OPTIONS:");
            this.optionSet.WriteOptionDescriptions(msg);
            msg.WriteLine($@"");
            msg.WriteLine($@"注意事項:");
            msg.WriteLine("機械翻訳を使用する場合、原文に変数（{0}や{1}）が含まれている項目は機械翻訳を使用せず、原文を使用します。これはゲームの不具合を防止するためです。なお、日本語訳が存在する場合は問題ありません。");
            msg.WriteLine($@"");
            msg.WriteLine($@"Example:");
            msg.WriteLine($@"  翻訳シート(-s)とオリジナルの言語ファイル(-i)から日本語化MOD(-o)を作成する。");
            msg.WriteLine(
                $@"    {exeName} -i original\resources_00002.-5 -o new\resources_00002.-5" +
                $@" -s CrsTransSheet.csv");
            msg.WriteLine($@"終了コード:");
            msg.WriteLine($@" 0  正常終了");
            msg.WriteLine($@" 1  異常終了");
            msg.WriteLine();

            if (textWriter == null)
            {
                textWriter = Console.Error;
            }

            textWriter.Write(msg.ToString());
        }

        /// <summary>
        /// エラーメッセージ表示
        /// </summary>
        public void ShowErrorMessage()
        {
            TextWriter writer = Console.Error;
            this.ShowErrorMessage(writer);
        }

        /// <summary>
        /// エラーメッセージ表示
        /// </summary>
        /// <param name="textWriter">出力先</param>
        public void ShowErrorMessage(TextWriter textWriter)
        {
            if (textWriter == null)
            {
                textWriter = Console.Error;
            }

            textWriter.Write(this.ErrorMessage);
        }

        /// <summary>
        /// オプション文字の設定
        /// </summary>
        /// <param name="args">args</param>
        private void Settings(string[] args)
        {
            this.optionSet = new OptionSet()
            {
                { "i|in="      , this.args.FileNameInputText   , v => this.args.FileNameInput   = v},
                { "o|out="     , this.args.FileNameOutputText  , v => this.args.FileNameOutput  = v},
                { "s|sheet="   , this.args.FileNameSheetText   , v => this.args.FileNameSheet   = v},
                { "m"          , this.args.UseMachineTransText , v => this.args.UseMachineTrans = v != null},
                { "r"          , this.args.UseReplaceText      , v => this.args.UseReplace      = v != null},
                { "h|help"     , "ヘルプ"                      , v => this.args.Help            = v != null},
            };

            List<string> extra;
            try
            {
                extra = this.optionSet.Parse(args);
                if (extra.Count > 0)
                {
                    // 指定されたオプション以外のオプションが指定されていた場合、
                    // extra に格納される。
                    // 不明なオプションが指定された。
                    this.SetErrorMessage($"{Environment.NewLine}エラー：不明なオプションが指定されました。");
                    extra.ForEach(t => this.SetErrorMessage(t));
                    this.isError = true;
                }
            }
            catch (OptionException e)
            {
                ////パースに失敗した場合OptionExceptionを発生させる
                this.SetErrorMessage(e.Message);
                this.isError = true;
            }
        }

        /// <summary>
        /// オプションのチェック
        /// </summary>
        private void CheckOption()
        {
            //// -h
            if (this.Arges.Help)
            {
                this.SetErrorMessage();
                this.isError = false;
                return;
            }

            if (this.IsErrorSheetSystemFile())
            {
                return;
            }

            if (this.IsErrorInputFile())
            {
                return;
            }

            if (this.IsErrorOutputFile())
            {
                return;
            }

            this.isError = false;
            return;
        }

        /// <summary>
        /// 翻訳シート(システム)の有無を確認
        /// </summary>
        /// <returns>翻訳シート(システム)の存在有無</returns>
        private bool IsErrorSheetSystemFile()
        {
            if (string.IsNullOrWhiteSpace(this.Arges.FileNameSheet))
            {
                this.SetErrorMessage($@"{Environment.NewLine}エラー：(-s)言語ファイル(system)のパスを指定してください。");
                this.isError = true;

                return true;
            }
            else
            {
                if (!File.Exists(this.Arges.FileNameSheet))
                {
                    this.SetErrorMessage($@"{Environment.NewLine}エラー：(-s)言語ファイル(system)が見つかりません。{Environment.NewLine}({Path.GetFullPath(this.Arges.FileNameSheet)})");
                    this.isError = true;

                    return true;
                }
            }

            return false;
        }

        private bool IsErrorInputFile()
        {
            if (string.IsNullOrWhiteSpace(this.Arges.FileNameInput))
            {
                this.SetErrorMessage($@"{Environment.NewLine}エラー：(-i)オリジナル版の言語ファイルのパスを指定してください。");
                this.isError = true;

                return true;
            }
            else
            {
                if (!File.Exists(this.Arges.FileNameInput))
                {
                    this.SetErrorMessage($@"{Environment.NewLine}エラー：(-i)オリジナル版の言語ファイルがありません。{Environment.NewLine}({Path.GetFullPath(this.Arges.FileNameInput)})");
                    this.isError = true;

                    return true;
                }
            }

            return false;
        }

        private bool IsErrorOutputFile()
        {
            if (string.IsNullOrWhiteSpace(this.Arges.FileNameOutput))
            {
                this.SetErrorMessage($@"{Environment.NewLine}エラー：(-o)日本語化された言語ファイルのパスを指定してください。");
                this.isError = true;

                return true;
            }

            if (File.Exists(this.Arges.FileNameOutput) && !this.args.UseReplace)
            {
                this.SetErrorMessage(
                    $@"{Environment.NewLine}エラー：(-o)日本語化された言語ファイルが既に存在します。{Environment.NewLine}" +
                    $@"({Path.GetFullPath(this.Arges.FileNameInput)}){Environment.NewLine}" +
                    $@"上書きする場合は '-r' オプションを指定してください。");
                this.isError = true;

                return true;
            }

            return false;
        }

        private void SetErrorMessage(string errorMessage = null)
        {
            if (errorMessage != null)
            {
                this.errorMessage.WriteLine(errorMessage);
            }
        }

        /// <summary>
        /// オプション項目
        /// </summary>
        public class TArgs
        {
            public string FileNameInput { get; internal set; }

            public string FileNameInputText { get; internal set; } =
                "オリジナル版の言語ファイルのパスを指定する。";

            public string FileNameOutput { get; internal set; }

            public string FileNameOutputText { get; internal set; } =
                "日本語化された言語ファイルのパスを指定する。";

            public string FileNameSheet { get; set; }

            public string FileNameSheetText { get; set; } =
                "CSV形式の翻訳シートのパス名。";

            public bool UseMachineTrans { get; internal set; }

            public string UseMachineTransText { get; internal set; } =
                $"有志翻訳がない場合は機械翻訳を使用する。{Environment.NewLine}注意事項：機械翻訳を使用した場合は、ゲームがフリーズする可能性があります。";

            public bool UseReplace { get; internal set; }

            public string UseReplaceText { get; internal set; } = $"出力用言語ファイルが既に存在する場合はを上書きする。";

            public bool Help { get; set; }
        }
    }
}
