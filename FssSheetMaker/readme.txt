Fallout Shelter 日本語化支援キット（翻訳シート作成）

このツールは、UnityEXで export した言語ファイルから翻訳シートを作成するコマンドラインツールです。

使い方：
言語ファイルから翻訳シートを作成する。
  usage: FssSheetMaker.exe -i <lang file path> -s <trans sheet path> [-r]
OPTIONS:
  -i, --in=VALUE             オリジナル版の言語ファイルのパスを指定する。
  -s, --sheet=VALUE          CSV形式の翻訳シートのパス名。
  -r                         翻訳シートが既に存在する場合はを上書きする。
  -h, --help                 ヘルプ
Example:
  オリジナルの言語ファイル(-i)から翻訳シート(-s)を作成する。
    FssSheetMaker.exe -i data\en\resources_00001.-6 -s data\csv\FssTransSheet.csv
終了コード:
 0  正常終了
 1  異常終了