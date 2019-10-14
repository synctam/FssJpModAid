Fallout Shelter 日本語化支援キット（日本語化MOD作成）

このツールは、UnityEXで export した言語ファイルと翻訳シートから日本語化された言語ファイルを作成するコマンドラインツールです。

使い方：
日本語化MODを作成する。
  usage: FssJpModMaker.exe -i <original lang file path> -o <japanized lang file path> -s <Trans Sheet path> [-m] [-r]
OPTIONS:
  -i, --in=VALUE             オリジナル版の言語ファイルのパスを指定する。
  -o, --out=VALUE            日本語化された言語ファイルのパスを指定する。
  -s, --sheet=VALUE          CSV形式の翻訳シートのパス名。
  -m                         有志翻訳がない場合は機械翻訳を使用する。
                               注意事項：機械翻訳を使用した場合は、ゲームがフリーズする可能性があります。
  -r                         出力用言語ファイルが既に存在する場合はを上書きする。
  -h, --help                 ヘルプ

注意事項:
機械翻訳を使用する場合、原文に変数（{0}や{1}）が含まれている項目は機械翻訳を使用せず、原文を使用します。これはゲームの不具合を防止するためです。なお、日本語訳が存在する場合は問題ありません。

Example:
  翻訳シート(-s)とオリジナルの言語ファイル(-i)から日本語化MOD(-o)を作成する。
    FssJpModMaker.exe -i original\resources_00002.-5 -o new\resources_00002.-5 -s CrsTransSheet.csv
終了コード:
 0  正常終了
 1  異常終了