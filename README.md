# FssJpModAid
Fallout Shelter 日本語化MOD作成支援ツール

## FsbFontChanger

    日本語用フォント座標ファイルを作成する。
      usage: FsbFontChanger.exe -i <original font file path> -o <japanized font file path> -x <xml font path> [-r]
    OPTIONS:
      -i, --in=VALUE             オリジナル版のフォント座標ファイルのパスを指定する。
      -o, --out=VALUE            日本語化されたフォント座標ファイルのパスを指定する。
      -x, --xml=VALUE            BMFontで作成された座標ファイル（XML）のパス名を指定する。
      -r                         出力用言語ファイルが既に存在する場合はを上書きする。
      -h, --help                 ヘルプ

    Example:
      オリジナルのフォント座標ファイル(-i)とBMFontで作成されたXMLファイル(-x)から日本語用フォント座標ファイル(-o)を作成する 。
        FsbFontChanger.exe -i original\resources_00001.114 -o new\resources_00001.114 -x futura_0.xml
    終了コード:
     0  正常終了
     1  異常終了


## FssJpModMaker

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


## FssSheetMaker

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
